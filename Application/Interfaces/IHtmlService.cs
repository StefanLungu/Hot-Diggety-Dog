using Domain.Entities;

namespace Application.Interfaces
{
    public interface IHtmlService
    {
        string ConvertProductsRequestToHtml(ProductsRequest productsRequest);
    }
}
