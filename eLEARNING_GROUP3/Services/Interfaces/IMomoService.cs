using OnlineLearning.Models.Momo;
using OnlineLearning.Models.Order;

namespace OnlineLearning.Services.Interfaces
{
    public interface IMomoService
    {
        Task<MomoCreatePaymentResponeModel> CreatePaymentAsync(OrderInfoModel model);
        Task<MomoExecuteResponseModel> PaymentExecuteAsync(IQueryCollection collection, long courseId);
    }
}
