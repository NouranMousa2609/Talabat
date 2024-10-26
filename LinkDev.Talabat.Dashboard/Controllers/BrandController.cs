using LinkDev.Talabat.Core.Domain.Contracts.Presistence;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.Dashboard.Controllers
{
    public class BrandController(IUnitOfWork _unitOfWork) : Controller
    {
        public async Task <IActionResult> Index()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand,int>().GetAllAsync();
            return View(brands);
        }
        public async Task<IActionResult> Create (ProductBrand brand)
        {
            try
            {
                await _unitOfWork.GetRepository<ProductBrand, int>().AddAysnc(brand);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                ModelState.AddModelError("Name", "Please Add New Name ");
                return View("Index",await _unitOfWork.GetRepository<ProductBrand,int>().GetAllAsync());
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var brand =await _unitOfWork.GetRepository<ProductBrand,int>().GetAsync(id);
            _unitOfWork.GetRepository<ProductBrand,int>().Delete(brand);
            await _unitOfWork.CompleteAsync();
            return RedirectToAction("Index");
        }

    }
}
