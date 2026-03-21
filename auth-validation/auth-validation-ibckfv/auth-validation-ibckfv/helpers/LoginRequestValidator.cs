using auth_validation_ibckfv.DTO;
using FluentValidation;

namespace auth_validation_ibckfv.helpers;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.UsernameOrEmail)
            .NotEmpty().WithMessage("Имя пользователя или Email обязательно.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Пароль обязателен.");
    }
}