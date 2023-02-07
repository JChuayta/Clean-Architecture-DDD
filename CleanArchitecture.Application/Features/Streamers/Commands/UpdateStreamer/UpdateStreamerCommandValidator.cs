using FluentValidation;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer
{
    public class UpdateStreamerCommandValidator : AbstractValidator<UpdateStreamerCommand>
    {
        public UpdateStreamerCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("{Nombre} no permite valores nulos");
            RuleFor(x => x.Url)
                .NotNull().WithMessage("{Url} no permite valores nulos");
        }
    }
}
