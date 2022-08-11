using Newtonsoft.Json;
namespace DAL
{
    /// <summary>
    /// A class that represents the configuration's data from the json file.
    /// </summary>
    public class ConfigData
    {
        public int MaxQuantity { get; set; }
        public int MinQuantityToAlert { get; set; }

        public double PercentageRange { get; set; }
    }

    public class Configuration
    {
        public static ConfigData Data { get; set; }

        static Configuration()
        {
            var currentDirectory = Environment.CurrentDirectory; //get the current directory path
            var fileName = "Configuration.json";
            var configurationPath = Path.Combine(currentDirectory, fileName); //combine the current directory with the json file name
            var readText = File.ReadAllText(configurationPath); //reads the text from them json file
            Data = JsonConvert.DeserializeObject<ConfigData>(readText); //deserialize the text from the json file into the data property correspondingly
        }
    }
}
