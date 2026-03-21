using FluentValidation;
using httplistener_test_ibckfv.Dto;

namespace httplistener_test_ibckfv.Validators;

public class BookingValidator : AbstractValidator<BookingCreateDto>
{
    public BookingValidator()
    {
        RuleFor(x => x.ApartmentId)
            .GreaterThan(0);

        RuleFor(x => x.DateStart)
            .NotEmpty().WithMessage("Start date is required.");

        RuleFor(x => x.DateEnd)
            .NotEmpty().WithMessage("End date is required.")
            .GreaterThanOrEqualTo(x => x.DateStart)
            .WithMessage("End date must be after start date.");
    }
}