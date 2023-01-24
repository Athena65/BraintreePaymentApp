using Braintree;
using BraintreePaymentApp.Models;
using BraintreePaymentApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BraintreePaymentApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBraintreeService _braintreeService;

        public HomeController(IBraintreeService braintreeService)
        {
            _braintreeService = braintreeService;
        }

        public IActionResult Index()
        {
            var gateway= _braintreeService.GetGateway();
            var clientToken = gateway.ClientToken.Generate();
            ViewBag.ClientToken = clientToken;

            var data = new PhonePurchaseVM
            {
                Id = Guid.NewGuid(),
                Producer="Samsung",
                Receiver="Ahmet Kara",
                Model="J7 Core",
                Nonce="",
                Price=500
            };
            return View(data);
        }
        [HttpPost]
        public object Create(PhonePurchaseVM phone)
        {
            string paymentStatus=string.Empty;
            var gateway = _braintreeService.GetGateway();
            var request = new TransactionRequest
            {
                Amount = Convert.ToDecimal("500"),
                PaymentMethodNonce = phone.Nonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };
            Result<Transaction> result=gateway.Transaction.Sale(request);
            if (result.IsSuccess())
                paymentStatus = "Payment Successfull!";
            else
            {
                foreach (ValidationError error in result.Errors.DeepAll())
                {
                    paymentStatus = $"Error: {(int)error.Code} {error.Message}";

                }
            }
            var data = new PhonePurchaseVM
            {
                Id = Guid.NewGuid(),
                Producer = "Samsung",
                Receiver = "Ahmet Kara",
                Model = "J7 Core",
                Nonce = "",
                Price = 500
            };
            return paymentStatus;
               
        }
    }

       
}