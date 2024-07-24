using System.Collections.Generic;
using System.IO;

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
    }
}
