using SCA.WebApp.Models;

namespace SCA.WebApp.Services.Contracts
{
    public interface IAssetTypeService
    {
        Task<IEnumerable<AssetTypeViewModel>> GetAll();
        Task<AssetTypeViewModel> GetById(int id);

        Task<AssetTypeViewModel> CreateAssetType(AssetTypeViewModel assetTypeVM);
        Task<AssetTypeViewModel> UpdateAssetType(AssetTypeViewModel assetTypeVM);
        Task<bool> DeleteAssetType(int id);
    }
}
