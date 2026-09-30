using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace EmployeeLifecycleManagement.Utilities
{
    public static class JsonReader
    {
        public static List<T> ReadJson<T>(string fileName)
        {
            string path = Path.Combine(
                AppContext.BaseDirectory,
                "TestData",
                fileName
            );
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(
                    $"Test data file not found: {path}"
                );
            }

            string json = File.ReadAllText(path);

            return JsonSerializer.Deserialize<List<T>>(json)
                   ?? new List<T>();
        }

    }
}
