using FluentValidation;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequestDto>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Summary).NotEmpty().MaximumLength(300);
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be a positive value.");
    }    
}    
