using DotnetSeleniumTest;
using Newtonsoft.Json;

namespace selenium_test.Services
{
    public class FileService
    {
        // Function to save the product links to a file
        public void SaveProductLinks(List<string> links, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var link in links)
                {
                    writer.WriteLine(link);
                }
            }
        }

        //Function to save links to a json file
        public void SaveLinksToJsonFile(List<string> links, string filePath)
        {
            // Convert the list of links to JSON
            string json = JsonConvert.SerializeObject(links, Formatting.Indented);
            // Writing the JSON to a file
            File.WriteAllText(filePath, json);
        }

        public void SaveReviewsToJsonFile(List<ItemReviewModel> reviews, string filePath)
        {
            // Convert the audit list to JSON
            string json = JsonConvert.SerializeObject(reviews, Formatting.Indented);
            // Writing the JSON to a file
            File.WriteAllText(filePath, json);
        }
    }
}
