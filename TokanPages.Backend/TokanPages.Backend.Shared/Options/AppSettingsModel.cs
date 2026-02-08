using Microsoft.Extensions.Configuration;

namespace TokanPages.Backend.Shared.Options;

public class AppSettingsModel
{
    public const string SectionName = "AppSettings";

    [ConfigurationKeyName("RequestLimiter_Window")]
    public int RequestLimiterWindow { get; init; } = 3;

    [ConfigurationKeyName("RequestLimiter_Permit")]
    public int RequestLimiterPermit { get; init; } = 1;

    [ConfigurationKeyName("RequestLimiter_Segments")]
    public int RequestLimiterSegments { get; init; } = 2;

    [ConfigurationKeyName("SonarQube_Server")]
    public string SonarQubeServer {get; init;} = string.Empty;

    [ConfigurationKeyName("SonarQube_Token")]
    public string SonarQubeToken { get; init; } = string.Empty;

    [ConfigurationKeyName("UserNote_MaxSize")]
    public int UserNoteMaxSize { get; init; }

    [ConfigurationKeyName("UserNote_MaxCount")]
    public int UserNoteMaxCount {get; init;}

    [ConfigurationKeyName("CacheMediaFiles")]
    public string CacheMediaFiles { get; init; } = string.Empty;

    [ConfigurationKeyName("CacheNonMediaFiles")]
    public string CacheNonMediaFiles { get; init; } = string.Empty;

    [ConfigurationKeyName("CacheConfiguration")]
    public string CacheConfiguration { get; init; } = string.Empty;

    [ConfigurationKeyName("BatchInvoicing_Cron")]
    public string BatchInvoicingCron { get; init; } = string.Empty;
    
    [ConfigurationKeyName("CachingService_Cron")]
    public string CachingServiceCron { get; init; } = string.Empty;
    
    [ConfigurationKeyName("CachingService_GetUrl")]
    public string CachingServiceGetUrl { get; init; } = string.Empty;

    [ConfigurationKeyName("CachingService_PostUrl")]
    public string CachingServicePostUrl { get; set; } = string.Empty;

    [ConfigurationKeyName("CachingService_Files")]
    public string CachingServiceFiles { get; init; } = string.Empty;
    
    [ConfigurationKeyName("CachingService_Paths")]
    public string CachingServicePaths  { get; init; } = string.Empty;
    
    [ConfigurationKeyName("CachingService_PdfSource")]
    public string CachingServicePdfSource { get; init; } = string.Empty;
    
    [ConfigurationKeyName("Email_PrivateKey")]
    public string EmailPrivateKey { get; init; } = string.Empty;
    
    [ConfigurationKeyName("Email_HealthUrl")]
    public string EmailHealthUrl { get; init; } = string.Empty;
    
    [ConfigurationKeyName("Email_BaseUrl")]
    public string EmailBaseUrl { get; init; } = string.Empty;
    
    [ConfigurationKeyName("Email_Address_Admin")]
    public string EmailAddressAdmin  { get; init; } = string.Empty;
    
    [ConfigurationKeyName("Email_Address_Contact")]
    public string EmailAddressContact { get; init; } = string.Empty;

    [ConfigurationKeyName("Email_Address_Support")]
    public string EmailAddressSupport { get; init; } = string.Empty;

    [ConfigurationKeyName("Db_DatabaseContext")]
    public string DbDatabaseContext { get; init; } = string.Empty;

    [ConfigurationKeyName("Db_DatabaseContext_Migrator")]
    public string DbDatabaseContextMigrator { get; init; } = string.Empty;

    [ConfigurationKeyName("AZ_Bus_ConnectionString")]
    public string AzBusConnectionString { get; init; } = string.Empty;

    [ConfigurationKeyName("AZ_Storage_BaseUrl")]
    public string AzStorageBaseUrl { get; init; } = string.Empty;

    [ConfigurationKeyName("AZ_Storage_ContainerName")]
    public string AzStorageContainerName { get; init; } = string.Empty;

    [ConfigurationKeyName("AZ_Storage_ConnectionString")]
    public string AzStorageConnectionString { get; init; } = string.Empty;

    [ConfigurationKeyName("AZ_Storage_MaxFileSizeUserMedia")]
    public int AzStorageMaxFileSizeUserMedia { get; init; } = 104857600;

    [ConfigurationKeyName("AZ_Storage_MaxFileSizeSingleAsset")]
    public int AzStorageMaxFileSizeSingleAsset { get; init; } = 262144000;

    [ConfigurationKeyName("AZ_Redis_InstanceName")]
    public string AzRedisInstanceName { get; init; } = string.Empty;
    
    [ConfigurationKeyName("AZ_Redis_ConnectionString")]
    public string AzRedisConnectionString { get; init; } = string.Empty;

    [ConfigurationKeyName("AZ_Redis_ExpirationMinute")]
    public int AzRedisExpirationMinute { get; init; } = 15;

    [ConfigurationKeyName("AZ_Redis_ExpirationSecond")]
    public int AzRedisExpirationSecond { get; init; } = 30;

    [ConfigurationKeyName("AZ_Hub_HubName")]
    public string AzHubHubName { get; init; } = string.Empty;

    [ConfigurationKeyName("AZ_Hub_ServiceEndpoint")]
    public string AzHubServiceEndpoint { get; init; } = string.Empty;
    
    [ConfigurationKeyName("AZ_Hub_ConnectionString")]
    public string AzHubConnectionString { get; init; } = string.Empty;
    
    [ConfigurationKeyName("Paths_NewsletterUpdate")]
    public string PathsNewsletterUpdate {get; init;} = string.Empty;
    
    [ConfigurationKeyName("Paths_NewsletterRemove")]
    public string PathsNewsletterRemove { get; init; } = string.Empty;
    
    [ConfigurationKeyName("Paths_UpdatePassword")]
    public string PathsUpdatePassword { get; init; } = string.Empty;
    
    [ConfigurationKeyName("Paths_Activation")]
    public string PathsActivation { get; init; } = string.Empty;
    
    [ConfigurationKeyName("Paths_DevelopmentOrigin")]
    public string PathsDevelopmentOrigin { get; init; } = string.Empty;
    
    [ConfigurationKeyName("Paths_DeploymentOrigin")]
    public string PathsDeploymentOrigin { get; init; } = string.Empty;
    
    [ConfigurationKeyName("Paths_Templates_Newsletter")]
    public string PathsTemplatesNewsletter { get; init; } = string.Empty;

    [ConfigurationKeyName("Paths_Templates_ContactForm")]
    public string PathsTemplatesContactForm { get; set; } = string.Empty;

    [ConfigurationKeyName("Paths_Templates_ResetPassword")]
    public string PathsTemplatesResetPassword { get; init; } = string.Empty;

    [ConfigurationKeyName("Paths_Templates_RegisterForm")]
    public string PathsTemplatesRegisterForm { get; init; } = string.Empty;

    [ConfigurationKeyName("Paths_Templates_VerifyEmail")]
    public string PathsTemplatesVerifyEmail { get; init; } = string.Empty;

    [ConfigurationKeyName("Ids_Issuer")]
    public string IdsIssuer { get; init; } = string.Empty;

    [ConfigurationKeyName("Ids_Audience")]
    public string IdsAudience { get; init; } = string.Empty;

    [ConfigurationKeyName("Ids_WebSecret")]
    public string IdsWebSecret { get; init; } = string.Empty;

    [ConfigurationKeyName("Ids_RequireHttps")]
    public bool IdsRequireHttps { get; init; }

    [ConfigurationKeyName("Ids_WebToken_Maturity")]
    public int IdsWebTokenMaturity { get; init; } = 90;

    [ConfigurationKeyName("Ids_RefreshToken_Maturity")]
    public int IdsRefreshTokenMaturity { get; init; } = 120;

    [ConfigurationKeyName("Limit_Reset_Maturity")]
    public int LimitResetMaturity { get; init; } = 30;

    [ConfigurationKeyName("Limit_Activation_Maturity")]
    public int LimitActivationMaturity { get; init; } = 30;

    [ConfigurationKeyName("Limit_Likes_Anonymous")]
    public int LimitLikesAnonymous { get; init; } = 25;

    [ConfigurationKeyName("Limit_Likes_User")]
    public int LimitLikesUser { get; init; } = 50;
    
    [ConfigurationKeyName("Pmt_MerchantPosId")]
    public string PmtMerchantPosId { get; init; } = string.Empty;
    
    [ConfigurationKeyName("Pmt_ClientId")]
    public string PmtClientId { get; init; } = string.Empty;
    
    [ConfigurationKeyName("Pmt_ClientSecret")]
    public string PmtClientSecret { get; init; } = string.Empty;
    
    [ConfigurationKeyName("Pmt_BaseUrl")]
    public string PmtBaseUrl { get; init; } = string.Empty;
    
    [ConfigurationKeyName("Pmt_Address_Authorize")]
    public string PmtAddressAuthorize { get; init; } = string.Empty;
    
    [ConfigurationKeyName("Pmt_Address_Tokens")]
    public string PmtAddressTokens { get; init; } = string.Empty;
    
    [ConfigurationKeyName("Pmt_Address_Orders")]
    public string PmtAddressOrders { get; init; } = string.Empty;
    
    [ConfigurationKeyName("Pmt_Address_Payouts")]
    public string PmtAddressPayouts { get; init; } = string.Empty;
    
    [ConfigurationKeyName("Pmt_Address_PayMethods")]
    public string PmtAddressPayMethods { get; init; } = string.Empty;

    [ConfigurationKeyName("Pmt_Address_Reports")]
    public string PmtAddressReports { get; init; } = string.Empty;
}