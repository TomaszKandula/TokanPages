using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.HostedServices.Services.Abstractions;
using TokanPages.HostedServices.Services.Base;
using TokanPages.Services.SpaCachingService;

namespace TokanPages.HostedServices.Services.CronJobs;

/// <summary>
/// CRON job implementation.
/// </summary>
[ExcludeFromCodeCoverage]
public class CachingProcessingJob : CronJob
{
    private readonly ICachingService _cachingService;

    private readonly ILoggerService _loggerService;

    private readonly string _cronExpression;

    /// <summary>
    /// CRON job implementation.
    /// </summary>
    /// <param name="config"></param>
    /// <param name="cachingService"></param>
    /// <param name="loggerService"></param>
    public CachingProcessingJob(ICachingProcessingConfig config, 
        ICachingService cachingService, ILoggerService loggerService)
        : base(config.CronExpression, config.TimeZoneInfo)
    {
        _cachingService = cachingService;
        _loggerService = loggerService;
        _cronExpression = config.CronExpression;
    }

    /// <summary>
    /// Execute caching of the SPA.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task DoWork(CancellationToken cancellationToken)
    {
        _loggerService.LogInformation($"{DateTime.Now:T} {nameof(CachingProcessingJob)} is working...");

        await _cachingService.RenderStaticPage("/", "MainPage");
        await _cachingService.RenderStaticPage("/about/info", "InfoPage");
        await _cachingService.RenderStaticPage("/about/story", "StoryPage");
        await _cachingService.RenderStaticPage("/articles", "ArticlesPage");
        await _cachingService.RenderStaticPage("/showcase", "ShowcasePage");
        await _cachingService.RenderStaticPage("/document", "PdfViewerPage");
        await _cachingService.RenderStaticPage("/business", "BusinessPage");
        await _cachingService.RenderStaticPage("/leisure/bicycle", "BicyclePage");
        await _cachingService.RenderStaticPage("/leisure/electronics", "ElectronicsPage");
        await _cachingService.RenderStaticPage("/leisure/football", "FootballPage");
        await _cachingService.RenderStaticPage("/leisure/guitar", "GuitarPage");
        await _cachingService.RenderStaticPage("/leisure/photography", "PhotographyPage");
        await _cachingService.RenderStaticPage("/terms", "TermsPage");
        await _cachingService.RenderStaticPage("/policy", "PolicyPage");
        await _cachingService.RenderStaticPage("/contact", "ContactPage");
        await _cachingService.RenderStaticPage("/signin", "SigninPage");
        await _cachingService.RenderStaticPage("/signup", "SignupPage");
        await _cachingService.RenderStaticPage("/signout", "SignoutPage");
        await _cachingService.RenderStaticPage("/account", "AccountPage");
        await _cachingService.RenderStaticPage("/accountactivation", "ActivationPage");
        await _cachingService.RenderStaticPage("/updatepassword", "PasswordUpdatePage");
        await _cachingService.RenderStaticPage("/resetpassword", "PasswordResetPage");
        await _cachingService.RenderStaticPage("/update-newsletter", "NewsletterUpdatePage");
        await _cachingService.RenderStaticPage("/remove-newsletter", "NewsletterRemovePage");
    }

    /// <summary>
    /// Start CRON job.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _loggerService.LogInformation($"{nameof(CachingProcessingJob)} started. CRON expression is '{_cronExpression}'.");
        return base.StartAsync(cancellationToken);
    }

    /// <summary>
    /// Stop CRON job.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _loggerService.LogInformation($"{nameof(CachingProcessingJob)} is stopped.");
        return base.StopAsync(cancellationToken);
    }
}