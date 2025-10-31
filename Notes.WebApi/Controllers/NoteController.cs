using Microsoft.AspNetCore.Mvc;
using Notes.WebApi.Models;
using Notes.Application.Notes.Commands.CreateNote;
using AutoMapper;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Application.Notes.Commands.DeleteCommand;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Application.Notes.Queries.GetNoteList;

namespace Notes.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : BaseController
    {
        private readonly IMapper _mapper;

        public NoteController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<NoteListVm>> GetAll()
        {
            var query = new GetNoteListQuery
            {
                UserId = UserId
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpGet("{id}")]
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

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteDto createNote)
        {
            var command = _mapper.Map<CreateNoteCommand>(createNote);
            command.UserId = UserId;
            var noteId = await Mediator.Send(command);
            return Ok(noteId);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateNoteDto updateNote)
        {
            var command = _mapper.Map<UpdateNoteCommand>(updateNote);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteNoteCommand
            {
                UserId = UserId,
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
