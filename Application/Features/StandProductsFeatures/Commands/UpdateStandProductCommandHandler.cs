using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.StandProductsFeatures.Commands
{
    public class UpdateStandProductCommandHandler : IRequestHandler<UpdateStandProductCommand, Guid>
    {
        private readonly IStandProductRepository _standProductRepository;

        public UpdateStandProductCommandHandler(IStandProductRepository standProductRepository)
        {
            _standProductRepository = standProductRepository;
        }

        public async Task<Guid> Handle(UpdateStandProductCommand request, CancellationToken cancellationToken)
        {
            HotDogStandProduct standProduct = await _standProductRepository.GetStandProductByProductId(request.StandId, request.ProductId);

            if (standProduct == null)
            {
                standProduct = new HotDogStandProduct()
                {
                    StandId = request.StandId,
                    ProductId = request.ProductId,
                    Quantity = request.NewQuantity
                };
                await _standProductRepository.CreateAsync(standProduct);
            }
            else
            {
                standProduct.Quantity = request.NewQuantity;

                if (standProduct.Quantity == 0)
                {
                    await _standProductRepository.RemoveAsync(standProduct);
                }
                else
                {
                    await _standProductRepository.UpdateAsync(standProduct);
                }
            }

            return standProduct.Id;
        }
    }
}
