using System;
using System.Collections.Generic;
using System.Linq;

namespace SpeedyTurtle.Models
{
    public class UserTask
    {
        public UserTask()
        {
            Bids = new List<Bid>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal MaximumOffer { get; set; }

        public DateTime Submitted { get; set; }

        public TaskStatus Status { get; set; }

        public List<Bid> Bids { get; set; }
        public Bid WinningBid { get; set; }

        public void AcceptWinningBid(int bidId)
        {
            var bid = Bids.SingleOrDefault(b => b.Id == bidId);
            WinningBid = bid;
            Status = TaskStatus.Completed;
        }

        public void SubmitBid(Bid bid)
        {
            if (Bids == null)
                Bids = new List<Bid>();

            var existingBid = Bids.SingleOrDefault(b => b.Agent.Id == bid.Agent.Id);

            if (existingBid != null)
                throw new Exception("Can't bid on the same task twice!");

            Bids.Add(bid);
        }
    }

    public enum TaskStatus
    {
        Open, InProgress, Completed
    }
}