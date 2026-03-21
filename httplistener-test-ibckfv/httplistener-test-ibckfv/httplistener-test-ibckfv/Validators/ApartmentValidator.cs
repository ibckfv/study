using FluentValidation;
using httplistener_test_ibckfv.Dto;

namespace httplistener_test_ibckfv.Validators;

public class ApartmentValidator : AbstractValidator<ApartmentCreateDto>
{
    public ApartmentValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500);

        RuleFor(x => x.PricePerDay)
            .NotEmpty().WithMessage("PricePerDay is required.")
            .Must(BeNumeric).WithMessage("PricePerDay must be a number.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.");
    }

    private bool BeNumeric(string value)
        => decimal.TryParse(value, out _);
}