using Newtonsoft.Json;
namespace DAL
{
    /// <summary>
    /// A class that represents the configuration's data from the json file.
    /// </summary>
    public class ConfigData
    {
        /// <summary>
        /// The maximum allowed quantity of a box in the storage.
        /// </summary>
        public int MaxQuantity { get; set; }
        /// <summary>
        /// The minimum quantity of a box that alerts the user.
        /// </summary>
        public int MinQuantityToAlert { get; set; }
        /// <summary>
        /// The percentage of distancing from the wanted amount of boxes.
        /// </summary>
        public double PercentageRange { get; set; }
        /// <summary>
        /// The maximum days that a box can be in the storage without being bought.
        /// </summary>
        public int MaxDaysUntilExpiration { get; set; }
    }

    /// <summary>
    /// A class that convert the configuration file(json) to a <see cref="ConfigData"/> variable.
    /// </summary>
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
