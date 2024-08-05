using DependencyInjection.Models;

namespace DependencyInjection.IServices
{
    public interface IHomeServices
    {
        Task<List<HomeModel>> GetAllHomeModels();
    }
}
