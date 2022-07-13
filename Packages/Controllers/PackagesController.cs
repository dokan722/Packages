using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Packages.Data;
using Packages.Models;
using Packages.Other;

namespace Packages.Controllers
{
    public class PackagesController : Controller
    {
        private readonly PackagesContext _context;

        public PackagesController(PackagesContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: Packages
        public async Task<IActionResult> Index(PackageState? filter, int? pageNumber)
        {
            ViewData["Filter"] = filter;
            
            var packages = from p in _context.Packages select p;

            if (filter != null)
            {
                packages = packages.Where(s => s.State == filter);
            }

            int pageSize = 5;
            return _context.Packages != null ?
                        View(await PaginatedList<Package>.CreateAsync(packages.AsNoTracking(), pageNumber ?? 1, pageSize)) :
                        Problem("Entity set 'PackagesContext.Packages'  is null.");
        }

        // GET: Packages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Packages == null)
            {
                return NotFound();
            }

            var package = await _context.Packages
                .Include(p => p.Parcels)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.PackageID == id);
            if (package == null)
            {
                return NotFound();
            }

            return View(package);
        }

        public async Task<IActionResult> CreateEdit(int? id)
        {
            Package? package;
            if (id == null || _context.Packages == null)
            {
                package = new Package
                {
                    PackageName = "Nowa paczka",
                    CreateDate = DateTime.Today,
                    State = PackageState.Open,
                    CityDestination = "Kraków",
                    Parcels = new List<Parcel>()
                };
            }
            else
            {
                package = await _context.Packages
                    .Include(p => p.Parcels)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.PackageID == id);
            }
            return View(package);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEdit(int id, Package package)
        {
            if (id != package.PackageID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var allParcels = _context.Parcels;
                    var parcels = allParcels.Where(p => p.PackageID == id);
                    _context.Parcels.RemoveRange(parcels);
                    if (package.State == PackageState.Closed)
                    {
                        package.CloseDate = DateTime.Today;
                    }
                    _context.Update(package);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PackageExists(package.PackageID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(package);
        }

        // GET: Packages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Packages == null)
            {
                return NotFound();
            }

            var package = await _context.Packages
                .FirstOrDefaultAsync(m => m.PackageID == id);
            if (package == null)
            {
                return NotFound();
            }

            return View(package);
        }

        // POST: Packages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Packages == null)
            {
                return Problem("Entity set 'PackagesContext.Packages'  is null.");
            }
            var package = await _context.Packages.FindAsync(id);
            if (package != null)
            {
                _context.Packages.Remove(package);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> NewParcelView(int packageID)
        {
            var parcel = new Parcel
            {
                ParcelName = "Nowa przesyłka",
                CreateDate = DateTime.Today,
                DeliveryAddress = "Kraków",
                PackageID = packageID,
                Weight = 1
            };
            return PartialView("_Parcel", parcel);
        }

        private bool PackageExists(int id)
        {
            return (_context.Packages?.Any(e => e.PackageID == id)).GetValueOrDefault();
        }

        /*[HttpPost]
        public async Task<IActionResult> AddParcel(Package package)
        { ;
            if (package.Parcels == null)
                package.Parcels = new List<Parcel>();
            package.Parcels.Add(new Parcel
            {
                ParcelName = "Nowa przesyłka",
                CreateDate = DateTime.Today,
                DeliveryAddress = "Kraków",
                PackageID = package.PackageID,
                Weight = 1
            });
            return View("CreateEdit", package);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveParcel(Package package, int id)
        {
            package.Parcels.RemoveAt(id);
            ModelState.Clear();
            return View("CreateEdit", package);
        }*/

        [HttpPost]
        public async Task<IActionResult> OpenPackage(int id)
        {
            var package =  await _context.Packages.FirstOrDefaultAsync(m => m.PackageID == id);
            package.State = PackageState.Open;
            package.CloseDate = null;
            _context.Update(package);
            _context.SaveChanges();
            return RedirectToAction("CreateEdit", new { id = id});
        }
    }
}
