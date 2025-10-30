using Microsoft.AspNetCore.Mvc;
using Notes.Application.Notes.Commands.Queries.GetNoteDetails;
using Notes.Application.Notes.Commands.Queries.GetNoteList;

namespace Notes.WebApi.Controllers
{
    public class NoteController : BaseController
    {
        public async Task<ActionResult<NoteListVm>> GetAll()
        {
            var query = new GetNoteListQuery
            {
                UserId = UserId
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        public async Task<ActionResult<NoteDetailsVm>> Get(Guid id)
        {
            var query = new GetNoteDetailsQuery
            {
                UserId = UserId,
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
    }
}
