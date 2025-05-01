using System.Text.Json;

namespace RenspandWebsite.Service
{
    public class JsonFileService<T>
    {
        public IWebHostEnvironment WebHostEnvironment { get; }

        public JsonFileService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "Data", typeof(T).Name + "s.json"); }
        }

        /// <summary>
        /// Saves a list of objects to a JSON file.
        /// </summary>
        /// <param name="objs"></param>
        public void SaveJsonObjects(List<T> objs)
        {
            using (FileStream jsonFileWriter = File.Create(JsonFileName))
            {
                Utf8JsonWriter jsonWriter = new Utf8JsonWriter(jsonFileWriter, new JsonWriterOptions()
                {
                    SkipValidation = false,
                    Indented = true
                });
                JsonSerializer.Serialize<T[]>(jsonWriter, objs.ToArray());
            }
        }

        /// <summary>
        /// Reads the JSON file and deserializes it into a list of objects.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetJsonObjects()
        {
            using (StreamReader jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<T[]>(jsonFileReader.ReadToEnd());
            }
        }
    }
}
