using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListaZakupowWaszkiewicz.Models;
using System.Text.Json;

namespace ListaZakupowWaszkiewicz.Services
{
    public class DataService
    {
        private readonly string filePath =
            Path.Combine(FileSystem.AppDataDirectory, "shoppingdata.json");

        public async Task SaveAsync(List<Category> categories)
        {
            string json = JsonSerializer.Serialize(categories);
            await File.WriteAllTextAsync(filePath, json);
        }

        public async Task<List<Category>> LoadAsync()
        {
            if (!File.Exists(filePath))
                return new List<Category>();

            string json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<List<Category>>(json);
        }
    }
}
