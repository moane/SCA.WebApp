using SCA.WebApp.Controllers;
using SCA.WebApp.Models;
using SCA.WebApp.Services.Contracts;
using System.Text;
using System.Text.Json;

namespace SCA.WebApp.Services
{
    public class AssetService : IAssetService
    {
        private readonly IHttpClientFactory _clientFactory;  
        private readonly JsonSerializerOptions _options;
        private readonly ILogger<HomeController> _logger;

        private AssetViewModel AssetVM;
        private IEnumerable<AssetViewModel> AssetsVM;

        private const string apiEndpoint = "/Prod/api/asset/";
        private const string clientName = "ScaRegisterApi";

       
        public AssetService(IHttpClientFactory clientFactory, ILogger<HomeController> logger)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _logger = logger;
            AssetsVM = new List<AssetViewModel>();
            AssetVM = new AssetViewModel();
        }

        public async Task<IEnumerable<AssetViewModel>> GetAll()
        {            

            var client = _clientFactory.CreateClient(clientName);

            using (var response = await client.GetAsync(apiEndpoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    if (apiResponse != null)
                    {
                        AssetsVM = await JsonSerializer
                            .DeserializeAsync<IEnumerable<AssetViewModel>>(apiResponse, _options);
                    }
                }
                else
                {
                    _logger.LogError(1, "Erro ao obter ativos", response);
                    return null;
                }
            }

            return AssetsVM;

        }

        public async Task<AssetViewModel> GetById(int id)
        {
            var client = _clientFactory.CreateClient(clientName);

            using (var response = await client.GetAsync(apiEndpoint+id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    if (apiResponse != null)
                    {
                        AssetVM = await JsonSerializer
                            .DeserializeAsync<AssetViewModel>(apiResponse, _options);
                    }
                }
                else
                {
                    _logger.LogError(2, "Erro ao obter ativo por id", response);
                    return null;
                }
            }

            return AssetVM;
        }

        public async Task<AssetViewModel> CreateAsset(AssetViewModel AssetVM)
        {
            var client = _clientFactory.CreateClient(clientName);
            StringContent content = new StringContent(JsonSerializer.Serialize(AssetVM), Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(apiEndpoint,content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    if (apiResponse != null)
                    {
                        AssetVM = await JsonSerializer
                            .DeserializeAsync<AssetViewModel>(apiResponse, _options);
                    }
                }
                else
                {
                    _logger.LogError(3, "Erro ao criar ativo", response);
                    return null;
                }
            }

            return AssetVM;
        }


        public async Task<AssetViewModel> UpdateAsset(AssetViewModel AssetVM)
        {
            var client = _clientFactory.CreateClient(clientName);
            // StringContent content = new StringContent(JsonSerializer.Serialize(AssetVM), Encoding.UTF8, "application/json");

            AssetViewModel AssetUpdated = new AssetViewModel();

            using (var response = await client.PutAsJsonAsync(apiEndpoint, AssetVM))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    if (apiResponse != null)
                    {
                        AssetUpdated = await JsonSerializer
                            .DeserializeAsync<AssetViewModel>(apiResponse, _options);
                    }
                }
                else
                {
                    _logger.LogError(1, "Erro ao atualizar ativo", response);
                    return null;
                }
            }

            return AssetUpdated;
        }
        public async Task<bool> DeleteAsset(int id)
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
                    _logger.LogError(1, "Erro ao deletar ativo", response);                    
                }
            }

            return false;
        }
    }
}
