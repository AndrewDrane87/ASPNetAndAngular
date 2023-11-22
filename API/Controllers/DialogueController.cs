using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DialogueController : BaseApiController
    {
        private readonly UnitOfWork uow;

        public DialogueController(UnitOfWork uow)
        {
            this.uow = uow;
        }

        [HttpPost("create-main")]
        public async Task<ActionResult<DialogueNodeDto>> CreateMain(DialogueNodeDto dialogue, [FromQuery] int npcId)
        {
            var result = await uow.DialogRepository.CreateMainDialogue(dialogue, npcId);
            if (result != null)
                return Ok(result);

            return BadRequest("failed to create main dialogue");
        }

        [HttpPost("create-dialogue")]
        public async Task<ActionResult<DialogueNodeDto>> CreateChildDialogue(DialogueNodeDto dialogue, [FromQuery] int fromDialogueId)
        {
            var d = await uow.DialogRepository.CreateDialogue(dialogue, fromDialogueId);
            if (d == null) return BadRequest("Could not find response to tie dialogue to");

            return Ok(d);
        }

        [HttpPost("create-response")]
        public async Task<ActionResult<DialogueNodeDto>> CreateResponse(DialogueNode response, [FromQuery] int dialogueId)
        {
            var result = await uow.DialogRepository.CreateResponse(response, dialogueId);
            if (result != null && await uow.Complete())
                return Ok(result);

            return BadRequest("Failed to create response");
        }

        [HttpGet("get-dialogue")]
        public async Task<ActionResult<DialogueNodeDto>> GetDialogue([FromQuery] int dialogueId)
        {
            var dialogue = await uow.DialogRepository.GetDialogue(dialogueId);

            if (dialogue == null) return BadRequest("Could not find dialogue");

            return Ok(dialogue);
        }

        [HttpGet("get-dialogue-from-response")]
        public async Task<ActionResult<DialogueNodeDto>> GetDialogueFromResponse([FromQuery] int responseId)
        {
            var dialogue = await uow.DialogRepository.GetDialogueFromResponse(responseId);

            if (dialogue == null) return NoContent();

            return Ok(dialogue);
        }

        /*
        [HttpGet("get-previous-dialogue")]
        public async Task<ActionResult<DialogueDto>> GetPreviousDialogue([FromQuery] int dialogueId)
        {
            var d = await uow.DialogRepository.GetPreviousDialogue(dialogueId);
            if (d == null) return BadRequest("Could not find parent response/dialogue");

            return d;

        }
        */

        [HttpDelete("delete-response")]
        public async Task<ActionResult<DialogueNodeDto>> DeleteResponse([FromQuery] int responseId)
        {
            var dialogDto = await uow.DialogRepository.DeleteResponse(responseId);

            if (dialogDto == null) return BadRequest("failed to delete response");

            if (await uow.Complete())
                return Ok(dialogDto);

            return BadRequest("Could not delete response");

        }
    }
}
