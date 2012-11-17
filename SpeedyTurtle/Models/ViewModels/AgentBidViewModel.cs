using System;

namespace SpeedyTurtle.Models.ViewModels
{
    public class AgentBidViewModel
    {
        public int TaskId { get; set; }
        public int AgentId { get; set; }
        public string TaskDescription { get; set; }
        public DateTime Submitted { get; set; }
        public string Comments { get; set; }
        public decimal Amount { get; set; }
    }
}