using GLPIService.Application.Common.Interfaces;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;

namespace GLPIService.Infrastructure.Services.Glpi {
    public class GlpiHealthCheck : IHealthCheck {

        private readonly IGlpiService _glpiService;
        private readonly Stopwatch _timer;

        public GlpiHealthCheck(IGlpiService service) {
            _glpiService = service;
            _timer = new Stopwatch();
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default) {
            try {
                _timer.Start();
                await _glpiService.Login();
                _timer.Stop();

                var elapsedMilliseconds = _timer.ElapsedMilliseconds;

                if (elapsedMilliseconds < 1000) {
                    return HealthCheckResult.Healthy("Communication with Glpi without problems");
                }

                return HealthCheckResult.Degraded("Communication with Glpi with delay");
            } catch (Exception) {
                return new HealthCheckResult(
                context.Registration.FailureStatus, "No communication with Glpi.");
            }
        }
    }
}
