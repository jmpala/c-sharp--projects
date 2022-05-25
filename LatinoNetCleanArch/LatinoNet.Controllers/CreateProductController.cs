using LatinoNet.DTOs;
using LatinoNet.Presenters;
using LatinoNet.UseCasesPorts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LatinoNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateProductController
    {
        readonly ICreateProductInputPort InputPort;
        readonly ICreateProductOutputPort OutputPort;

        public CreateProductController(ICreateProductInputPort inputPort, ICreateProductOutputPort outputPort)
        {
            InputPort = inputPort;
            OutputPort = outputPort;
        }

        [HttpPost]
        public async Task<ProductDTO> CreateProduct(CreateProductDTO product)
        {
            await InputPort.Handle(product);
            return ((IPresenter<ProductDTO>)OutputPort).Content;
        }
    }
}
