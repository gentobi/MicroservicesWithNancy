using System.Threading.Tasks;
using FactorialService.Models;
using Microsoft.Extensions.Logging;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Validation;

namespace FactorialService {
    public class Module : NancyModule {
        private ILogger<Module> _logger;
        private ServiceClient<LogRequest, LogResponse> _logServiceClient;

        public Module(ILoggerFactory loggerFactory, ServiceClient<LogRequest, LogResponse> logServiceClient){
            _logger = loggerFactory.CreateLogger<Module> ();
            _logServiceClient = logServiceClient;

            Get ("/factorial/{number:int}",  p =>  GetFactorial (new FactorialRequest { Number = p.number }));

            Post ("/log", p => PostLogRequest ());
        }

        LogResponse PostLogRequest () {
            var request = this.Bind<LogRequest> ();
            var validationResult = this.Validate (request);

            if (!validationResult.IsValid) {
                _logger.LogInformation ($"Failed to log message '{request.Message}'");
                return new LogResponse { Success = false, Errors = validationResult.FormattedErrors };
            }

            _logger.LogInformation ($"Logging Message: '{request.Message}'");

            return new LogResponse { Success = true };
        }
        async Task<FactorialResponse> GetFactorial(FactorialRequest request){
            var validationResult = this.Validate (request);

            if (!validationResult.IsValid) {
                _logger.LogInformation ($"Failed to generate factorial for {request.Number}");
                return new FactorialResponse { Success = false, Errors = validationResult.FormattedErrors };
            }

            var result = new FactorialResponse { Success = true, Factorial = Factorial (request.Number) };
            await _logServiceClient.Post(new LogRequest { Message = $"Generated Factorial for {request.Number}: {result.Factorial}"});

            return result;
        }

        int Factorial (int i) {
            if (i <= 1)
                return 1;
            return i * Factorial (i - 1);
        }
    }
}