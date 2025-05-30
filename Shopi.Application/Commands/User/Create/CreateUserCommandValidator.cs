using FluentValidation;
using Shopi.Resources;

namespace Shopi.Application.Commands.User.Create;

public class CreateUserCommandValidator: AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.name)
            .NotNull().NotEmpty()
            .WithMessage(string.Format(Messages.Required, nameof(DomainModel.Models.UserEntity.Name)))

            .MaximumLength(100)
            .WithMessage(string.Format(Messages.MaxLength, nameof(DomainModel.Models.UserEntity.Name), 100));
    }
}
