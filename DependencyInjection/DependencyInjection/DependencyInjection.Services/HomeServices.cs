using DependencyInjection.IServices;
using DependencyInjection.Models;
using DependencyInjection.IRepositories;
using DependencyInjection.Repositories;

namespace DependencyInjection.Services
{
    public class HomeServices : IHomeServices
    {
        private readonly IHomeRepository homeRepository;
        public HomeServices(IHomeRepository _homeRepository)
        {
            homeRepository = _homeRepository;
        }

        public async Task<List<HomeModel>> GetAllHomeModels()
        {
            return await homeRepository.GetAllHomeModels();
        }
    }
}
