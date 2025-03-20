using CsvHelper;
using System.Globalization;

namespace DemoApp.Business.Helpers
{
    public class CsvHelperService : ICsvHelperService
    {
        public IEnumerable<T> ReadCSV<T>(Stream file)
        {
            var reader = new StreamReader(file);
            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecords<T>();
            return records;
        }
    }
}
