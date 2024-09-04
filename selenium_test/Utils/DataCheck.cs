

namespace selenium_test.Services
{

    public class DataCheck
    {
        public static bool IsDataEmpty(List<string> dataList)
        {
            return (dataList.Count > 0) ? true : false;
        }
    }
}
