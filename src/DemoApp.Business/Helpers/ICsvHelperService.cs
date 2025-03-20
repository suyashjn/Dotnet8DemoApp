namespace DemoApp.Business.Helpers
{
    public interface ICsvHelperService
    {
        public IEnumerable<T> ReadCSV<T>(Stream file);
    }
}
