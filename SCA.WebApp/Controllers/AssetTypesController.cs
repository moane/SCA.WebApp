using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCA.WebApp.IdentityConfiguration;
using SCA.WebApp.Models;
using SCA.WebApp.Services.Contracts;
using System.Diagnostics;


namespace SCA.WebApp.Controllers
{
   
    [Authorize(Roles = Roles.Admin)]
    public class AssetTypesController : Controller
    {
        private readonly IAssetTypeService _assetTypeService;


        public AssetTypesController(IAssetTypeService assetTypeService)
        {
            _assetTypeService = assetTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetTypeViewModel>>> Index()
        {
            var result = await _assetTypeService.GetAll();

            if (result is null)
                return View("Error");

            return View(result);
            
        }
        [HttpGet]
        public ActionResult Create()
        {
            return  View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(AssetTypeViewModel assetTypeVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _assetTypeService.CreateAssetType(assetTypeVM);

                if (result is not null)
                    return RedirectToAction(nameof(Index));
                else
                    return View("Error");
            }           

            return View(assetTypeVM);
        }

        [HttpGet]
        public async Task<ActionResult<AssetTypeViewModel>> Update(int id)
        {

            var result = await _assetTypeService.GetById(id);

            if (result is null)
                return View("Error");

            return View(result);

        }
       

        [HttpPost]
        public async Task<ActionResult> Update(AssetTypeViewModel assetTypeVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _assetTypeService.UpdateAssetType(assetTypeVM);

                if (result is not null)
                    return RedirectToAction(nameof(Index));

                return View("Error");
            }

            return View(assetTypeVM);
        }

        [HttpGet]
        public async Task<ActionResult<AssetTypeViewModel>> Delete(int id)
        {

            var result = await _assetTypeService.GetById(id);

            if (result is null)
                return View("Error");

            return View(result);

        }
        [HttpPost(), ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var result = await _assetTypeService.DeleteAssetType(id);

            if (result)
                return RedirectToAction(nameof(Index));

            return View("Error");
        }
        
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

   
}
