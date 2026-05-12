using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace PaymentsSystem.Application.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("El ID del cliente es obligatorio.");

            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("La moneda es obligatoria.")
                .Length(3).WithMessage("La moneda debe tener exactamente 3 caracteres (USD, HNL, EUR).")
                .Matches("^[A-Z]{3}$").WithMessage("La moneda debe estar en mayúsculas (USD, HNL, EUR).");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("La orden debe tener al menos un item.")
                .Must(items => items.Count <= 50)
                .WithMessage("Una orden no puede tener más de 50 items.");

            RuleForEach(x => x.Items).ChildRules(item =>
            {
                item.RuleFor(i => i.ProductName)
                    .NotEmpty().WithMessage("El nombre del producto es obligatorio.")
                    .MaximumLength(200).WithMessage("El nombre no puede superar 200 caracteres.");

                item.RuleFor(i => i.Quantity)
                    .GreaterThan(0).WithMessage("La cantidad debe ser mayor a cero.");

                item.RuleFor(i => i.UnitPrice)
                    .GreaterThan(0).WithMessage("El precio debe ser mayor a cero.");
            });
        }
    }
}
