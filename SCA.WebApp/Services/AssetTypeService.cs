using SCA.WebApp.Models;
using SCA.WebApp.Services.Contracts;
using System.Text;
using System.Text.Json;

namespace SCA.WebApp.Services
{
    public class AssetTypeService : IAssetTypeService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly JsonSerializerOptions _options;
        private readonly ILogger<AssetTypeService> _logger;

        private const string apiEndpoint = "/Prod/api/assettype/";      
        private const string clientName = "ScaRegisterApi";
        
        private AssetTypeViewModel assetTypeVM;
        private IEnumerable<AssetTypeViewModel> assetTypesVM;

        public AssetTypeService(IHttpClientFactory clientFactory, ILogger<AssetTypeService> logger)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _logger = logger;
            assetTypesVM = new List<AssetTypeViewModel>();
            assetTypeVM = new AssetTypeViewModel();

        }

        public async Task<IEnumerable<AssetTypeViewModel>> GetAll()
        {
            var client = _clientFactory.CreateClient(clientName);

            using (var response = await client.GetAsync(apiEndpoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    if (apiResponse != null)
                    {
                        assetTypesVM = await JsonSerializer
                            .DeserializeAsync<IEnumerable<AssetTypeViewModel>>(apiResponse, _options);
                    }
                }
                else
                {
                    _logger.LogError(1, "Erro ao obter tipos de ativos", response);
                    return null;
                }
            }

            return assetTypesVM;

        }

        public async Task<AssetTypeViewModel> GetById(int id)
        {
            var client = _clientFactory.CreateClient(clientName);

            using (var response = await client.GetAsync(apiEndpoint+id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    if (apiResponse != null)
                    {
                        assetTypeVM = await JsonSerializer
                            .DeserializeAsync<AssetTypeViewModel>(apiResponse, _options);
                    }
                }
                else
                {
                    _logger.LogError(2, "Erro ao obter tipo de ativo por id", response);
                    return null;
                }
            }

            return assetTypeVM;
        }

        public async Task<AssetTypeViewModel> CreateAssetType(AssetTypeViewModel assetTypeVM)
        {
            var client = _clientFactory.CreateClient(clientName);
            StringContent content = new StringContent(JsonSerializer.Serialize(assetTypeVM), Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(apiEndpoint,content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    if (apiResponse != null)
                    {
                        assetTypeVM = await JsonSerializer
                            .DeserializeAsync<AssetTypeViewModel>(apiResponse, _options);
                    }
                }
                else
                {
                    _logger.LogError(3, "Erro ao criar tipo de ativo", response);
                    return null;
                }
            }

            return assetTypeVM;
        }


        public async Task<AssetTypeViewModel> UpdateAssetType(AssetTypeViewModel assetTypeVM)
        {
            var client = _clientFactory.CreateClient(clientName);
            // StringContent content = new StringContent(JsonSerializer.Serialize(assetTypeVM), Encoding.UTF8, "application/json");

            AssetTypeViewModel assetTypeUpdated = new AssetTypeViewModel();

            using (var response = await client.PutAsJsonAsync(apiEndpoint, assetTypeVM))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    if (apiResponse != null)
                    {
                        assetTypeUpdated = await JsonSerializer
                            .DeserializeAsync<AssetTypeViewModel>(apiResponse, _options);
                    }
                }
                else
                {
                    _logger.LogError(4, "Erro ao atualizar tipo de ativo", response);
                    return null;
                }
            }

            return assetTypeUpdated;
        }
        public async Task<bool> DeleteAssetType(int id)
        {
            var client = _clientFactory.CreateClient(clientName);

            using (var response = await client.DeleteAsync(apiEndpoint + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    _logger.LogError(5, "Erro ao deletar tipo de ativo", response);
                    
                }
            }

            return false;
        }
    }
}
