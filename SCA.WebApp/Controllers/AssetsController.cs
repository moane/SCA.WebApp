using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SCA.WebApp.Models;
using SCA.WebApp.Services.Contracts;
using System.Diagnostics;

namespace SCA.WebApp.Controllers
{
    [Authorize]
    public class AssetsController : Controller
    {
        private readonly IAssetService _assetService;
        private readonly IAssetTypeService _assetTypeService;
        public AssetsController(IAssetService Assetservice, IAssetTypeService AssetTypeService)
        {
            _assetService = Assetservice;
            _assetTypeService = AssetTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetViewModel>>> Index()
        {
            var result = await _assetService.GetAll();

            if (result is null)
                return View("Error");

            return View(result);
            
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var types = await _assetTypeService.GetAll();

            ViewBag.AssetTypeId = new SelectList(types, "Id", "Name");

            return  View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(AssetViewModel AssetVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _assetService.CreateAsset(AssetVM);

                if (result is not null)
                    return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.AssetTypeId = new SelectList(await
                 _assetTypeService.GetAll(), "Id", "Name");
            }

            return View(AssetVM);
        }

        [HttpGet]
        public async Task<ActionResult<AssetViewModel>> Update(int id)
        {
            ViewBag.AssetTypeId = new SelectList(await
                 _assetTypeService.GetAll(), "Id", "Name");

            var result = await _assetService.GetById(id);

            if (result is null)
                return View("Error");

            return View(result);

        }
       

        [HttpPost]
        public async Task<ActionResult> Update(AssetViewModel AssetVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _assetService.UpdateAsset(AssetVM);

                if (result is not null)
                    return RedirectToAction(nameof(Index));

                return View("Error");
            }

            return View(AssetVM);
        }

        [HttpGet]
        public async Task<ActionResult<AssetViewModel>> Delete(int id)
        {

            var result = await _assetService.GetById(id);

            if (result is null)
                return View("Error");

            return View(result);

        }
        [HttpPost(), ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var result = await _assetService.DeleteAsset(id);

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
