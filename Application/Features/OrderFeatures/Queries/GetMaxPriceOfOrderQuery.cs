using MediatR;

namespace Application.Features.OrderFeatures.Queries
{
    public class GetMaxPriceOfOrderQuery : IRequest<double>
    {
    }
}
