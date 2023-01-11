using MyCoffeeApp.Models;
using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MyCoffeeApp.Services
{
    public static class CoffeeService
    {
        static SQLiteAsyncConnection db;

        static async Task Init()
        {
            if (db != null)
            {
                return;
            }

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");
            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<Coffee>();
        }

        public static async Task AddCoffee(string name,
            string roaster)
        {
            await Init();
            var image = "https://e7.pngegg.com/pngimages/156/456/png-clipart-brown-disposable-cup-filled-with-coffee-coffee-cup-paper-cup-tea-cup-fashion-tea.png";
            var coffee = new Coffee
            {
                Name = name,
                Roaster = roaster,
                Image = image
            };

            var id = await db.InsertAsync(coffee);
        }

        public static async Task RemoveCoffeee(int id)
        {
            await Init();
            await db.DeleteAsync<Coffee>(id);
        }

        public static async Task<IEnumerable<Coffee>> GetCoffee()
        {
            await Init();

            var coffee = await db.Table<Coffee>().ToListAsync();
            return coffee;
        }
    }
}
