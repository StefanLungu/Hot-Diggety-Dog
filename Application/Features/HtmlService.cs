using Application.Interfaces;
using Domain.Entities;
using System.Text;

namespace Application.Features
{
    public class HtmlService : IHtmlService
    {
        public string ConvertProductsRequestToHtml(ProductsRequest productsRequest)
        {
            StringBuilder stringBuilder = new("<h2>Your delivery is on its way</h2>");

            stringBuilder.Append("</br>");
            stringBuilder.Append("<ul>");

            foreach (ProductRequest productRequest in productsRequest.ProductRequests)
            {
                stringBuilder.AppendFormat("<li> {0} - {1} </li>", productRequest.Product.Name, productRequest.Quantity);
            }

            stringBuilder.Append("</ul>");
            stringBuilder.Append("</br>");
            stringBuilder.Append("</br>");

            stringBuilder.Append("<p>Have a nice day,</p>");
            stringBuilder.Append("<p>Hot Diggety Dog Supplier's Team</p>");

            return stringBuilder.ToString();
        }
    }
}
