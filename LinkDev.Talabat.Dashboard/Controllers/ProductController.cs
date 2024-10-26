using AutoMapper;
using LinkDev.Talabat.Core.Domain.Contracts.Presistence;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Dashboard.Helper;
using LinkDev.Talabat.Dashboard.Helpers;
using LinkDev.Talabat.Dashboard.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace LinkDev.Talabat.Dashboard.Controllers
{
    public class ProductController(IUnitOfWork _unitOfWork, IMapper _mapper) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync();
            var mappedProduct = _mapper.Map<IEnumerable<Product>,IEnumerable<ProductViewModel>>(products);
            return View(mappedProduct);
        }

        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                if (product.Image != null)
                {
                    product.PictureUrl = PictureSettings.UploadFile(product.Image, "products");

                }
                else
                {
                    product.PictureUrl = "images/products/glazed-donuts.png";
                }
                var mappedProduct = _mapper.Map<Product>(product);


                string userName = User.Identity?.Name ?? "Nouran_Mousa";
                mappedProduct.CreatedBy = userName;
                 mappedProduct.LastModifiedBy = userName;
                mappedProduct.NormalizedName = product.Name?.ToUpperInvariant();

                await _unitOfWork.GetRepository<Product, int>().AddAysnc(mappedProduct);

                await _unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
            return View(product);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetAsync(id);
            var mappedProduct = _mapper.Map<ProductViewModel>(product);
            return View(mappedProduct);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductViewModel product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                if (product.Image != null)
                    if (product.PictureUrl != null)
                    {
                        PictureSettings.DeleteFile(product.PictureUrl, "products");
                        product.PictureUrl = PictureSettings.UploadFile(product.Image, "products");

                    }
                    else
                        product.PictureUrl = PictureSettings.UploadFile(product.Image, "products");

                var mappedProduct = _mapper.Map<Product>(product);
                string userName = User.Identity?.Name ?? "Nouran_Mousa";
                mappedProduct.CreatedBy = userName;
                mappedProduct.LastModifiedBy = userName;
                mappedProduct.NormalizedName = product.Name?.ToUpperInvariant();

                _unitOfWork.GetRepository<Product, int>().Update(mappedProduct);
                var result = await _unitOfWork.CompleteAsync();
                if (result > 0)
                    return RedirectToAction("Index");
            }
            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetAsync(id);
            var mappedProduct = _mapper.Map<ProductViewModel>(product);
            return View(mappedProduct);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id)
                return NotFound();
            try
            {
                var product = await _unitOfWork.GetRepository<Product, int>().GetAsync(id);
                if (product.PictureUrl != null)
                {
                    PictureSettings.DeleteFile(product.PictureUrl, "products");

                }
                _unitOfWork.GetRepository<Product, int>().Delete(product);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction("Index");

            }
            catch (System.Exception)
            {
                return View(productViewModel);
            }
        }
    }
}
