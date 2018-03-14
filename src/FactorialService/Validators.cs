using FluentValidation;
using FactorialService.Models;

namespace FactorialService
{
    public class FactorialRequestValidator : AbstractValidator<LogRequest>
    {
        public FactorialRequestValidator()
        {
            RuleFor(request => request.Message).NotEmpty();
        }
    }
}