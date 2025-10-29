namespace Notes.Application.Notes.Commands.Queries.GetNoteList
{
    public class NoteListVm
    {
        public required IList<NoteLookupDto> Notes { get; set; }
    }
}
