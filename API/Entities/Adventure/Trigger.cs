namespace API.Entities
{
    public class Trigger
    {
        public int Id { get; set; }

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
    }
}
