namespace MarstonX.Infrastructure.Common.RetryPolicyHelper;

public static class RetryHelper
{
    /// <summary>
    /// Adds HttpClient Polly policies for circuit breaking and retrying.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the policies to.</param>
    /// <returns>The modified IServiceCollection.</returns>
    public static IServiceCollection AddHttpClientPolly(this IServiceCollection services)
    {
        // Circuit breaking using polly
        _ = services.AddHttpClient("errorApi", c => { c.BaseAddress = new Uri(string.Empty); })
            .AddTransientHttpErrorPolicy(policy =>
            {
                static void OnBreak(DelegateResult<HttpResponseMessage> ex, TimeSpan timeSpan)
                {
                    throw new HttpRequestException(Constant.CircuitError);
                }

                return policy.CircuitBreakerAsync(Constant.CircuitBreakerOpenCount, TimeSpan.FromSeconds(Constant.DurationOfBreak), OnBreak, () => { });
            });

        // Retry using polly
        services.AddHttpClient("errorApiClient", c => { c.BaseAddress = new Uri(string.Empty); })
               .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(Constant.RetryCount, _ => TimeSpan.FromSeconds(Constant.SleepDurationProvider)));

        return services;
    }
}
