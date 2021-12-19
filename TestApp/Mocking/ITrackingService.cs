namespace TestApp.Mocking
{
    public interface ITrackingService
    {
        Location Get(string path);
        string GetPathAsGeoHash();
    }
}