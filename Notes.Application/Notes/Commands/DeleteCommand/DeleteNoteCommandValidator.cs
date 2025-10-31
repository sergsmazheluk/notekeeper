using FluentValidation;

namespace Notes.Application.Notes.Commands.DeleteCommand
{
    public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
    {
        public DeleteNoteCommandValidator()
        {
            RuleFor(DeleteNoteCommand => DeleteNoteCommand.Id)
                .NotEqual(Guid.Empty);
            RuleFor(DeleteNoteCommand => DeleteNoteCommand.UserId)
                .NotEqual(Guid.Empty);
        }
    }
}
