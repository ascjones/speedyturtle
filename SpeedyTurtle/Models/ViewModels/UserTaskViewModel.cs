namespace SpeedyTurtle.Models.ViewModels
{
    public class UserTaskViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal MaximumOffer { get; set; }
        public UserTaskBidViewModel[] Bids { get; set; }
    }

    public class UserTaskBidViewModel
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public decimal Amount { get; set; }
    }
}