namespace OnlineLearning.Models.Order
{
    public class OrderInfoModel
    {

        public string OrderId { get; set; }
        public long UserId { get; set; }
        public long CourseId { get; set; }

        public string FullName { get; set; }

        public string OrderInfo { get; set; }
        public decimal Amount { get; set; }
    }
}
