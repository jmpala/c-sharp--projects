using LatinoNet.DTOs;
using LatinoNet.Entities.Interfaces;
using LatinoNet.Entities.POCOs;
using LatinoNet.UseCasesPorts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LatinoNet.UseCases.CreateProduct
{
    public class CreateProductInteractor : ICreateProductInputPort
    {
        readonly IProductRepository Repository;
        readonly IUnitOfWork UnitOfWork;
        readonly ICreateProductOutputPort OutputPort;

        public CreateProductInteractor(IProductRepository repository, IUnitOfWork unitOfWork, ICreateProductOutputPort outputPort)
        {
            Repository = repository;
            UnitOfWork = unitOfWork;
            OutputPort = outputPort;
        }

        public async Task Handle(CreateProductDTO product)
        {
            Product NewProduct = new Product
            {
                Name = product.ProductName
            };
            Repository.CreateProduct(NewProduct);
            await UnitOfWork.SaveChanges();
            await OutputPort.Handle(
                new ProductDTO
                {
                    Id = NewProduct.Id,
                    Name = NewProduct.Name
                }
            );
        }
    }
}
