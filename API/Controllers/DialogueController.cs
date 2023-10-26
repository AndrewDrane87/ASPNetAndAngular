using API.DTOs.Adventure;
using API.Entities.Adventure;
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
        public async Task<ActionResult<Dialogue>> CreateMain(Dialogue dialogue, [FromQuery] int npcId)
        {
            var result = await uow.DialogRepository.CreateMainDialogue(dialogue,npcId);
            if (result != null && await uow.Complete())
                return Ok(result);

            return BadRequest("failed to create main dialogue");
        }

        [HttpPost("create-response")]
        public async Task<ActionResult<DialogDto>> CreateResponse(DialogueResponse response,[FromQuery] int dialogueId)
        {
            var result = await uow.DialogRepository.CreateResponse(response, dialogueId);
            if (result != null && await uow.Complete())
                return Ok(result);

            return BadRequest("Failed to create response");
        }

        [HttpGet("get-dialogue")]
        public async Task<ActionResult<DialogDto>> GetDialogue([FromQuery] int dialogueId)
        {
            var dialogue = await uow.DialogRepository.GetDialogue(dialogueId);

            if (dialogue == null) return BadRequest("Could not find dialogue");

            return Ok(dialogue);
        }
        
        [HttpGet("get-dialogue-from-response")]
        public async Task<ActionResult<DialogDto>> GetDialogueFromResponse([FromQuery] int responseId)
        {
            var dialogue = await uow.DialogRepository.GetDialogueFromResponse(responseId);

            if (dialogue == null) return BadRequest("Could not find response or dialogue");

            return Ok(dialogue);
        }
    }
}
