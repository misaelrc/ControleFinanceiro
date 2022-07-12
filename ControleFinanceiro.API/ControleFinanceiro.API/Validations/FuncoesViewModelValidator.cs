using ControleFinanceiro.API.ViewModels;
using ControleFinanceiro.BLL.Models;
using FluentValidation;

namespace ControleFinanceiro.API.Validations
{
    public class FuncoesViewModelValidator : AbstractValidator<FuncoesViewModel>
    {
        public FuncoesViewModelValidator()
        {
            RuleFor(f => f.Name)
                .NotNull().WithMessage("Preencha a função")
                .NotEmpty().WithMessage("Preencha a função")
                .MinimumLength(1).WithMessage("Use mais caracteres")
                .MaximumLength(30).WithMessage("Use apenas 30 caracteres");

            RuleFor(f => f.Descricao)
                .NotNull().WithMessage("Preencha a descrição")
                .NotEmpty().WithMessage("Preencha a descrição")
                .MinimumLength(1).WithMessage("Use mais caracteres")
                .MaximumLength(30).WithMessage("Use apenas 50 caracteres");
        }
    }
}
