using DependencyInjection.IRepositories;
using DependencyInjection.Models;

namespace DependencyInjection.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        public async Task<List<HomeModel>> GetAllHomeModels()
        {
            var x = Task.Run(() => new List<HomeModel>
            {
                new HomeModel { HomeId = 1, HomeName = "My House", HomeDate = DateTime.Now, HomeValue = 100000000.00M, IsBeautiful=true }
            });

            return await x;
        }
    }
}
