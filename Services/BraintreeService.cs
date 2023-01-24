using Braintree;

namespace BraintreePaymentApp.Services
{
    public class BraintreeService : IBraintreeService
    {
        private readonly IConfiguration _config;

        public BraintreeService(IConfiguration config)
        {
            _config = config;
        }
        public IBraintreeGateway CreateGateway()
        {
            var newGateway = new BraintreeGateway()
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = _config.GetValue<string>("BraintreeGate:MerchantId"),
                PublicKey = _config.GetValue<string>("BraintreeGate:PublicKey"),
                PrivateKey = _config.GetValue<string>("BraintreeGate:PrivateKey")
            };
            return newGateway;
        }

        public IBraintreeGateway GetGateway()
        {
            return CreateGateway(); 
        }
    }
}
