using Packages.Models;

namespace Packages.Data
{
    public class DbInitializer
    {
        public static void Initialize(PackagesContext context)
        {
            context.Database.EnsureCreated();

            if (context.Packages.Any())
                return;

            var packages = new Package[]
            {
                new Package
                {
                    PackageName = "Pierwsza paczka", CreateDate = DateTime.Parse("2022-07-1"),
                    State = PackageState.Open, CloseDate = null, CityDestination = "Katowice"
                },
                new Package
                {
                    PackageName = "Druga paczka", CreateDate = DateTime.Parse("2022-07-2"), State = PackageState.Closed,
                    CloseDate = DateTime.Parse("2022-07-04"), CityDestination = "Wrocław"
                },
                new Package
                {
                    PackageName = "Trzecia paczka", CreateDate = DateTime.Parse("2022-07-1"), State = PackageState.Closed,
                    CloseDate = DateTime.Parse("2022-07-04"), CityDestination = "Tarnów"
                },
                new Package
                {
                    PackageName = "Czwarta paczka", CreateDate = DateTime.Parse("2022-07-4"), State = PackageState.Open,
                    CloseDate = null, CityDestination = "Sopot"
                },
                new Package
                {
                    PackageName = "Piąta paczka", CreateDate = DateTime.Parse("2022-07-6"), State = PackageState.Open,
                    CloseDate = null, CityDestination = "Łódź"
                },
                new Package
                {
                    PackageName = "Szósta paczka", CreateDate = DateTime.Parse("2022-07-7"), State = PackageState.Open,
                    CloseDate = null, CityDestination = "Warszawa"
                },
                new Package
                {
                    PackageName = "Siódma paczka", CreateDate = DateTime.Parse("2022-07-1"), State = PackageState.Closed,
                    CloseDate = DateTime.Parse("2022-07-06"), CityDestination = "Gdańsk"
                },
                new Package
                {
                    PackageName = "Ósma paczka", CreateDate = DateTime.Parse("2022-07-3"), State = PackageState.Open,
                    CloseDate = null, CityDestination = "Tarnobrzeg"
                },
                new Package
                {
                    PackageName = "Dziewiąta paczka", CreateDate = DateTime.Parse("2022-07-2"),
                    State = PackageState.Open, CloseDate = null, CityDestination = "Katowice"
                },
                new Package
                {
                    PackageName = "Dziesiąta paczka", CreateDate = DateTime.Parse("2022-07-5"),
                    State = PackageState.Open, CloseDate = null, CityDestination = "Kraków"
                },
                new Package
                {
                    PackageName = "Jedenasta paczka", CreateDate = DateTime.Parse("2022-07-1"), State = PackageState.Closed,
                    CloseDate = DateTime.Parse("2022-07-07"), CityDestination = "Białystok"
                },
                new Package
                {
                    PackageName = "Dwunasta paczka", CreateDate = DateTime.Parse("2022-07-4"),
                    State = PackageState.Open, CloseDate = null, CityDestination = "Rzeszów"
                }
            };

            foreach (Package p in packages)
            {
                context.Packages.Add(p);
            }
            context.SaveChanges();

            var parcels = new Parcel[]
            {
                new Parcel
                {
                    PackageID = packages.Single(p => p.PackageName == "Pierwsza paczka").PackageID,
                    ParcelName = "Pierwsza przesyłka w pierwszej paczce",
                    DeliveryAddress = "Jakaś ulica",
                    CreateDate = packages.Single(p => p.PackageName == "Pierwsza paczka").CreateDate,
                    Weight = 2.5
                },
                new Parcel
                {
                    PackageID = packages.Single(p => p.PackageName == "Pierwsza paczka").PackageID,
                    ParcelName = "Druga przesyłka w pierwszej paczce",
                    DeliveryAddress = "Inna ulica",
                    CreateDate = packages.Single(p => p.PackageName == "Pierwsza paczka").CreateDate,
                    Weight = 7.5
                }   
            };

            foreach (Parcel p in parcels)
            {
                context.Parcels.Add(p);
            }
            context.SaveChanges();
        }
    }
}
