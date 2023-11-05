using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Dtos
{
    public class ActionTriggerSaveDto
    {
        public int Id { get; set; }
        [ForeignKey("ActionTrigger")]
        public int ActionTriggerId { get; set; }
        public bool Complete { get; set; }

        /// <summary>
        /// enter, exit
        /// </summary>
        public string EventType { get; set; }

        /// <summary>
        /// information,spawn,setvariable,challenge
        /// </summary>
        public string ActionType { get; set; }
        public string ActionData { get; set; }
        public string ResultData { get; set; }
        public static ActionTriggerSaveDto Create(ActionTriggerSave save)
        {
            ActionTriggerSaveDto dto = new ActionTriggerSaveDto()
            {
                Id = save.Id,
                ActionTriggerId = save.ActionTrigger.Id,
                Complete = save.Complete,
                EventType = save.ActionTrigger.EventType,
                ActionType = save.ActionTrigger.ActionType,
                ActionData = save.ActionTrigger.ActionData,
                ResultData = save.ActionTrigger.ResultData
            };

            return dto;
        }

        public static List<ActionTriggerSaveDto> CreateList(List<ActionTriggerSave> saves)
        {
            List<ActionTriggerSaveDto> list = new List<ActionTriggerSaveDto>();
            if (saves != null)
            {
                foreach (ActionTriggerSave save in saves)
                {
                    if (save.Complete) continue;

                    ActionTriggerSaveDto dto = new ActionTriggerSaveDto()
                    {
                        Id = save.Id,
                        ActionTriggerId = save.ActionTrigger.Id,
                        Complete = save.Complete,
                        EventType = save.ActionTrigger.EventType,
                        ActionType = save.ActionTrigger.ActionType,
                        ActionData = save.ActionTrigger.ActionData,
                        ResultData = save.ActionTrigger.ResultData
                    };
                    list.Add(dto);
                }
            }

            return list;
        }
    }

}
