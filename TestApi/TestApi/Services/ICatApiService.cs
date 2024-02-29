using TestApi.Models;

namespace TestApi.Services
{
    public interface ICatApiService
    {
        Task<List<CatImage>> GetCatImages(int numberToGet);
    }
}
