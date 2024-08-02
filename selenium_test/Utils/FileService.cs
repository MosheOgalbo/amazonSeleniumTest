using DotnetSeleniumTest;
using Newtonsoft.Json;

namespace AmazonAutomation.Services
{
    public class FileService
    {
        // פונקציה לשמירת קישורי המוצרים לקובץ
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

        //פונקציה לשמירה קישורי לקובץ json
        public void SaveLinksToJsonFile(List<string> links, string filePath)
        {
            // המרת רשימת הקישורים ל-JSON
            string json = JsonConvert.SerializeObject(links, Formatting.Indented);

            // כתיבת ה-JSON לקובץ
            File.WriteAllText(filePath, json);
        }

        public  void  SaveReviewsToJsonFile(List<ItemReviewModel> reviews, string filePath)
        {
            // המרת רשימת הביקורות ל-JSON
            string json = JsonConvert.SerializeObject(reviews, Formatting.Indented);

            // כתיבת ה-JSON לקובץ
            File.WriteAllText(filePath, json);
        }
    }
}
