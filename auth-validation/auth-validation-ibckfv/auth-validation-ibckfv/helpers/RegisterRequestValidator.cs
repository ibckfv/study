using auth_validation_ibckfv.DTO;
using FluentValidation;

namespace auth_validation_ibckfv.helpers;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(user => user.Username)
            .NotEmpty().WithMessage("Имя пользователя обязательно")
            .MinimumLength(3).WithMessage("Имя пользователя должно быть не менее 3 символов")
            .Matches(@"^\S+$").WithMessage("Имя пользователя не должно содержать пробелов");
        
        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email обязателен")
            .EmailAddress().WithMessage("Некорректный формат email");
        
        RuleFor(user => user.FirstName)
            .NotEmpty().WithMessage("Имя обязательно");
        
        RuleFor(user => user.LastName)
            .NotEmpty().WithMessage("Фамилия обязательна");
        
        RuleFor(user => user.Phone)
            .Matches(@"^\+?\d{7,15}$")
            .When(x => !string.IsNullOrWhiteSpace(x.Phone))
            .WithMessage("Неверный формат телефона");

        RuleFor(user => user.Dob)
            .NotEmpty().WithMessage("Дата рождения обязательна");
        
        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Пароль обязателен")
            .MinimumLength(8).WithMessage("Пароль должен быть не менее 8 символов")
            .Matches("[A-Z]").WithMessage("Пароль должен содержать хотя бы одну заглавную букву")
            .Matches("[a-z]").WithMessage("Пароль должен содержать хотя бы одну строчную букву")
            .Matches("[0-9]").WithMessage("Пароль должен содержать хотя бы одну цифру")
            .Matches(@"[!@#\$%\^&\*]").WithMessage("Пароль должен содержать хотя бы один спецсимвол");

        RuleFor(user => user.ConfirmPassword)
            .Equal(user => user.Password).WithMessage("Пароли не совпадают");
    }
}