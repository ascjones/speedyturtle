using System;

namespace SpeedyTurtle.Models
{
    public enum BidStatus
    {
        Pending,
        Accepted,
        Rejected
    }

    public class Bid
    {
        public int Id { get; set; }
        public AgentDetail Agent { get; set; }
        public DateTime Submitted { get; set; }
        public string Comments { get; set; }
        public decimal Amount { get; set; }

        public BidStatus Status { get; set; }
    }

    public class AgentDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}