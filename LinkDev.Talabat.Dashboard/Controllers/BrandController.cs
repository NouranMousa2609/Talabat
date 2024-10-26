using LinkDev.Talabat.Core.Domain.Contracts.Presistence;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Dashboard.Models;
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
		[HttpPost]
		public async Task<IActionResult> Create(BrandViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View("Index", await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync());
			}

			var brand = new ProductBrand
			{
				Id = 0,
				Name = model.Name,
				CreatedBy = "ahmed.nasr",
				LastModifiedBy = "ahmed.nasr"
			};

			await _unitOfWork.GetRepository<ProductBrand, int>().AddAysnc(brand);
			await _unitOfWork.CompleteAsync();

			return RedirectToAction("Index");
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
