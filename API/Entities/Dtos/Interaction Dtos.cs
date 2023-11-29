namespace API.Entities
{
    public class NewInteractionDto
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
    }

    public class InteractionSaveDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Complete { get; set; }
        public bool Passed { get; set; }
        public string DefaultText { get; set; }
        public string FailedText { get; set; }
        public string PassedText { get; set; }
        public int LocationId { get; set; }
        public List<TriggerSaveDto> Triggers { get; set; }

        public static InteractionSaveDto Convert(InteractionSave save)
        {
            return new InteractionSaveDto
            {
                Id = save.Id,
                Name = save.Interaction.Name,
                Complete = save.Complete,
                Passed = save.Passed,
                DefaultText = save.Interaction.DefaultText,
                FailedText = save.Interaction.FailedText,
                PassedText = save.Interaction.PassedText,
                LocationId = save.LocationSave.LocationId,
                Triggers = TriggerSaveDto.ConvertList(save.TriggerSaves)
            };
        }

        public static List<InteractionSaveDto> ConvertList(List<InteractionSave> saves)
        {
            List<InteractionSaveDto> list = new List<InteractionSaveDto>();
            foreach (InteractionSave save in saves)
                list.Add(Convert(save));

            return list;
        }
    }

}
