using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;
using Notes.Domain;
using Notes.Application.Common.Exceptions;
namespace Notes.Application.Notes.Queries.GetNoteDetails
{
    public class GetNoteDetailsQueryHandler : IRequestHandler<GetNoteDetailsQuery,
        NoteDetailsVm>
    {
        private readonly INotesDbContext dbContext;
        private readonly IMapper _mapper;

        public GetNoteDetailsQueryHandler(INotesDbContext notesDbContext, IMapper mapper)
        {
            dbContext = notesDbContext;
            _mapper = mapper;
        }


        public async Task<NoteDetailsVm> Handle(GetNoteDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await dbContext.Notes.FirstOrDefaultAsync(note =>
            note.Id == request.Id, cancellationToken);

            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }

            return _mapper.Map<NoteDetailsVm>(entity);
        }
    }
}
