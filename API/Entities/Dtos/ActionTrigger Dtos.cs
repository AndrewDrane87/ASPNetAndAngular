using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class TriggerSaveDto
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
        public static TriggerSaveDto Convert(TriggerSave save)
        {
            TriggerSaveDto dto = new TriggerSaveDto()
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

        public static List<TriggerSaveDto> ConvertList(List<TriggerSave> saves)
        {
            List<TriggerSaveDto> list = new List<TriggerSaveDto>();
            if (saves != null)
            {
                foreach (TriggerSave save in saves)
                {
                    if (save.Complete) continue;

                    TriggerSaveDto dto = new TriggerSaveDto()
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
