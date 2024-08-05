using DependencyInjection.Models;

namespace DependencyInjection.IRepositories
{
    public interface IHomeRepository
    {
        Task<List<HomeModel>> GetAllHomeModels();
    }
}
