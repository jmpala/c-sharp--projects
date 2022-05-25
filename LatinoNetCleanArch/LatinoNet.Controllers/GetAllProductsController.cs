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
    public class GetAllProductsController
    {
        readonly IGetAllProductsInputPort InputPort;
        readonly IGetAllProductsOutputPort OutputPort;

        public GetAllProductsController(IGetAllProductsInputPort inputPort, IGetAllProductsOutputPort outputPort)
        {
            InputPort = inputPort;
            OutputPort = outputPort;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            await InputPort.Handle();
            return ((IPresenter<IEnumerable<ProductDTO>>)OutputPort).Content;
        }
    }
}
