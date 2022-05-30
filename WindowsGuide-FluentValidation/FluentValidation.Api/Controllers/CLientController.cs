using FluentValidation.Demo.Model;
using Microsoft.AspNetCore.Mvc;

namespace FluentValidation.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CLientController
    {
        [HttpGet]
        public Task get()
        {
            return Task.FromResult(new ClientModel()
            {
                Id = 1,
                FirstName = "Juan"
            });
        }

        [HttpPost]
        public Task Post(ClientModel model)
        {
            return Task.FromResult(model);
        }
    }
}
