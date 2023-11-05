using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class NpcController : BaseApiController
    {
        private readonly UnitOfWork uow;

        public NpcController(UnitOfWork uow)
        {
            this.uow = uow;
        }

        [HttpPost("create")]
        public async Task<ActionResult<NPC>> Create(NewNpcDto npc)
        {
            NPC result = await uow.NpcRepository.CreateNpc(new NPC { Name = npc.Name, Caption = npc.Caption }, npc.LocationId);
            if (result == null) return BadRequest("Failed to create NPC");

            if (await uow.Complete())
                return Ok(result);

            return BadRequest("Failed to create NPC");
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete([FromQuery]int id)
        {
             await uow.NpcRepository.DeleteNpc(id);

            if (await uow.Complete())
                return Ok();

            return BadRequest("Failed to delete Npc");
        }

        [HttpGet("get")]
        public async Task<ActionResult<NpcDto>> Get([FromQuery]int id)
        {
            var npc = await uow.NpcRepository.Get(id);
            if (npc == null) return BadRequest("Could not find npc");
            return Ok(npc);
        }
    }
}
