using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OnlineLearning.Models.Momo;
using OnlineLearning.Models.Order;
using OnlineLearning.Services.Interfaces;
using System.Text;

namespace OnlineLearning.Controllers
{
    public class PaymentController : Controller
    {
        private IMomoService _momoService;

        public PaymentController(IMomoService momoService)
        {
            _momoService = momoService;
        }

        //Hàm chính thức khi có khóa học đc mua
        [HttpGet] 
        public async Task<IActionResult> CreatePaymentMomo(long paymentCourseId, decimal amount, string orderInfor, long paymentUserId, string paymentFullName)
        {
            var formattedAmount = Math.Floor(amount);
            // Tạo thông tin đơn hàng
            var orderInfo = new OrderInfoModel
            {
                OrderId = Guid.NewGuid().ToString(),
                Amount = formattedAmount,
                OrderInfo = $"Thanh toán khóa học: {orderInfor}",
                UserId = paymentUserId,
                CourseId = paymentCourseId,
                FullName = paymentFullName
            };

            // Gửi yêu cầu thanh toán qua MoMo
            var response = await _momoService.CreatePaymentAsync(orderInfo);

            if (response == null || response.ErrorCode != 0 || string.IsNullOrEmpty(response.PayUrl))
            {
                // Lấy thông báo lỗi từ MoMo (nếu có)
                string errorMessage = response?.Message ?? "Có lỗi xảy ra khi tạo thanh toán.";
                TempData["ErrorMessage"] = errorMessage;
                return RedirectToAction("payWithMomo", "Dashboard", new { Area = "Admin" });

            }

            // Lưu thông tin đơn hàng để cập nhật sau khi thanh toán thành công
            HttpContext.Session.SetString("OrderId", orderInfo.OrderId);
            HttpContext.Session.SetString("CourseId", paymentCourseId.ToString());
            HttpContext.Session.SetString("userIdBuyCourse", paymentUserId.ToString());

            // Chuyển hướng sang trang thanh toán của MoMo
            return Redirect(response.PayUrl);
        }




        //Hàm từ formm test
        [HttpPost]
        public async Task<IActionResult> CreatePaymentMomo(OrderInfoModel model)
        {
            var response = await _momoService.CreatePaymentAsync(model);
            // Kiểm tra phản hồi từ MoMo
            if (response == null || response.ErrorCode != 0 || string.IsNullOrEmpty(response.PayUrl))
            {
                // Lấy thông báo lỗi từ MoMo (nếu có)
                string errorMessage = response?.Message ?? "Có lỗi xảy ra khi tạo thanh toán.";
                TempData["ErrorMessage"] = errorMessage;
                return RedirectToAction("payWithMomo", "Dashboard", new { Area = "Admin" });
                // Trả về view với thông báo lỗi
            }
            return Redirect(response.PayUrl);

        }
        //[HttpGet]
        //public IActionResult PaymentCallBack()
        //{
        //    var response = _momoService.PaymentExecuteAsync(HttpContext.Request.Query);
        //    return View(response);
        //}


        public IActionResult Index()
        {
            return View();
        }
    }
}
