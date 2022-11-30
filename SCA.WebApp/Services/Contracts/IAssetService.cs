using SCA.WebApp.Models;

namespace SCA.WebApp.Services.Contracts
{
    public interface IAssetService
    {
        Task<IEnumerable<AssetViewModel>> GetAll();
        Task<AssetViewModel> GetById(int id);

        Task<AssetViewModel> CreateAsset(AssetViewModel AssetVM);
        Task<AssetViewModel> UpdateAsset(AssetViewModel AssetVM);
        Task<bool> DeleteAsset(int id);
    }
}
