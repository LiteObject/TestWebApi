﻿namespace TestWebAPI.Library.HealthChecks
{
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using System.Net.NetworkInformation;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The ping health check.
    /// </summary>
    public class PingHealthCheck : IHealthCheck
    {
        /// <summary>
        /// The host.
        /// </summary>
        private readonly string host;

        /// <summary>
        /// The timeout.
        /// </summary>
        private readonly int timeout;

        /// <summary>
        /// Initializes a new instance of the <see cref="PingHealthCheck"/> class.
        /// </summary>
        /// <param name="host">
        /// The host.
        /// </param>
        /// <param name="timeout">
        /// The timeout.
        /// </param>
        public PingHealthCheck(string host, int timeout)
        {
            this.host = host;
            this.timeout = timeout;
        }

        /// <summary>
        /// The check health async.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using var ping = new Ping();
                var reply = await ping.SendPingAsync(this.host, this.timeout);

                if (reply.Status != IPStatus.Success)
                {
                    return HealthCheckResult.Unhealthy();
                }

                if (reply.RoundtripTime > this.timeout)
                {
                    return HealthCheckResult.Degraded();
                }

                return HealthCheckResult.Healthy();
            }
            catch
            {
                return HealthCheckResult.Unhealthy();
            }
        }
    }
}