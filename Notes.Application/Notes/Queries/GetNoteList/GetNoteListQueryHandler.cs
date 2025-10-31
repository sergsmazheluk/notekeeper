using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    public class GetNoteListQueryHandler : IRequestHandler<GetNoteListQuery, NoteListVm>
    {
        private readonly INotesDbContext dbContext;
        private readonly IMapper _mapper;

        public GetNoteListQueryHandler(INotesDbContext notesDbContext, IMapper mapper)
        {
            dbContext = notesDbContext;
            _mapper = mapper;
        }
        public async Task<NoteListVm> Handle(GetNoteListQuery request,
            CancellationToken cancellationToken)
        {
            var notesQuery = await dbContext.Notes
                .Where(note => note.UserId == request.UserId)
                .ProjectTo<NoteLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new NoteListVm { Notes = notesQuery };
        }
    }
}
