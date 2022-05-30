using FluentValidation.Demo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidation.Demo.Validations
{
    public class ClientValidator : AbstractValidator<ClientModel>
    {
        public ClientValidator()
        {
            RuleFor(cli => cli.Id).NotNull();
            RuleFor(cli => cli.FirstName).NotNull();
            RuleFor(cli => cli.LastName).NotNull();
            RuleFor(cli => cli.Email).NotNull();
            RuleFor(cli => cli.Birthdate).NotNull();
            RuleFor(cli => cli.Password).NotNull();
        }
    }
}
