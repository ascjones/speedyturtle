using System;

namespace SpeedyTurtle.Models
{
    public class UserTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal MaximumOffer { get; set; }

        public DateTime Submitted { get; set; }

        public TaskStatus Status { get; set; }

        public int? WinningBidId { get; set; }

        public void AcceptWinningBid(Bid bid)
        {
            WinningBidId = bid.Id;
            Status = TaskStatus.Completed;
        }
    }

    public enum TaskStatus
    {
        Open, InProgress, Completed
    }
}