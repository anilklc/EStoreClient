using EStore.Dto.Product;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security;
using System.Web;

namespace EStore.UI.ViewComponents.ProductViewComponents
{
    public class _ProductListComponentPartial : ViewComponent
    {
        private readonly IReadService<ResultProduct> _readService;
       
        public _ProductListComponentPartial(IReadService<ResultProduct> readService)
        {
            _readService = readService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int page,string categoryName,string brandName,float? minPrice,float? maxPrice)
        {

            var productList = await _readService.GetAllPagination($"Products/GetAllProductsByFilter?Page={page}");

            if (!string.IsNullOrEmpty(brandName))
            {
                string encodedbrandName = HttpUtility.UrlEncode(brandName);

                productList = await _readService.GetAllPagination($"Products/GetAllProductsByFilter?BrandName={encodedbrandName}");
            }
            else if (!string.IsNullOrEmpty(categoryName))
            {
                string encodedCategoryName = HttpUtility.UrlEncode(categoryName);
                productList = await _readService.GetAllPagination($"Products/GetAllProductsByFilter?CategoryName={encodedCategoryName}");
            }
            else if (maxPrice.HasValue && minPrice.HasValue)
            {
                productList = await _readService.GetAllPagination($"Products/GetAllProductsByFilter?MaxPrice={maxPrice}&MinPrice={minPrice}");
            }
           
            return View(productList);
        }
    }

}


