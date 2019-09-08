using AdvertApt.Models;
using System.Threading.Tasks;

namespace AdvertApi.Services
{
    public interface IAdvertStorageService
    {
        Task<string> Add(AdverModel model);
        Task Confirm(ConfirmAdvertModel model);
        
    }
}
