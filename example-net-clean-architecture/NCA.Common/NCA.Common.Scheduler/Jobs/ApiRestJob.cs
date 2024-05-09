using System.Diagnostics;
using NCA.Common.Application.Results;
using Quartz;
using ILogger = NCA.Common.Application.Infrastructure.Log.ILogger;

namespace NCA.Common.Scheduler.Jobs
{
    public class ApiRestJob : IJob
    {
        public const string Code = nameof(Code);
        public const string Endpoint = nameof(Endpoint);
        public const string StatusActive = "AC";
        public const string StatusInactive = "IN";

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;

        public ApiRestJob(IHttpClientFactory httpClientFactory, ILogger logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            if (context.CancellationToken.IsCancellationRequested)
                return;

            string? code = context.MergedJobDataMap.GetString(Code);
            string? endpoint = context.MergedJobDataMap.GetString(Endpoint);

            try
            {
                ArgumentNullException.ThrowIfNull(code);
                ArgumentNullException.ThrowIfNull(endpoint);

                var sw = Stopwatch.StartNew();

                var httpRequest = new HttpRequestMessage(HttpMethod.Get, endpoint);
                var httpClient = _httpClientFactory.CreateClient();
                var httpResponse = await httpClient.SendAsync(httpRequest);

                sw.Stop();

                switch ((int)httpResponse.StatusCode)
                {
                    case Result.SuccessCode:
                        _logger.LogInformation($"ApiRestJob - {code} - success execution in {sw.Elapsed.TotalMilliseconds} milliseconds.");
                        break;
                    case Result.ClientErrorCode:
                        var resultClientError = (await httpResponse.Content.ReadFromJsonAsync<ResultClientError>())!;

                        _logger.LogError($"ApiRestJob - {code} - client errror execution in {sw.Elapsed.TotalMilliseconds} milliseconds. Errors : {resultClientError.ErrorsText}");
                        break;
                    case Result.ServerErrorCode:
                        var resultServerError = (await httpResponse.Content.ReadFromJsonAsync<ResultServerError>())!;

                        _logger.LogError($"ApiRestJob - {code} - server errror execution in {sw.Elapsed.TotalMilliseconds} milliseconds. Detail : {resultServerError.DetailText}");
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"ApiRestJob - {code} - exception execution. Exception {ex}");
            }
        }
    }
}
