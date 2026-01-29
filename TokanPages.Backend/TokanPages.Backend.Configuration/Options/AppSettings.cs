using Microsoft.Extensions.Configuration;

namespace TokanPages.Backend.Configuration.Options;

public class AppSettings
{
    public const string SectionName = "AppSettings";

    [ConfigurationKeyName("RequestLimiter_Window")]
    public int RequestLimiterWindow { get; set; } = 3;

    [ConfigurationKeyName("RequestLimiter_Permit")]
    public int RequestLimiterPermit { get; set; } = 1;

    [ConfigurationKeyName("RequestLimiter_Segments")]
    public int RequestLimiterSegments { get; set; } = 2;

    [ConfigurationKeyName("SonarQube_Server")]
    public string SonarQubeServer {get; set;} = string.Empty;

    [ConfigurationKeyName("SonarQube_Token")]
    public string SonarQubeToken { get; set; } = string.Empty;

    [ConfigurationKeyName("UserNote_MaxSize")]
    public int UserNoteMaxSize { get; set; }

    [ConfigurationKeyName("UserNote_MaxCount")]
    public int UserNoteMaxCount {get; set;}

    [ConfigurationKeyName("CacheMediaFiles")]
    public string CacheMediaFiles { get; set; } = string.Empty;

    [ConfigurationKeyName("CacheNonMediaFiles")]
    public string CacheNonMediaFiles { get; set; } = string.Empty;

    [ConfigurationKeyName("CacheConfiguration")]
    public string CacheConfiguration { get; set; } = string.Empty;

    [ConfigurationKeyName("BatchInvoicing_Cron")]
    public string BatchInvoicingCron { get; set; } = string.Empty;
    
    [ConfigurationKeyName("CachingService_Cron")]
    public string CachingServiceCron { get; set; } = string.Empty;
    
    [ConfigurationKeyName("CachingService_GetUrl")]
    public string CachingServiceGetUrl { get; set; } = string.Empty;

    [ConfigurationKeyName("CachingService_PostUrl")]
    public string CachingServicePostUrl { get; set; } = string.Empty;

    [ConfigurationKeyName("CachingService_Files")]
    public string CachingServiceFiles { get; set; } = string.Empty;
    
    [ConfigurationKeyName("CachingService_Paths")]
    public string CachingServicePaths  { get; set; } = string.Empty;
    
    [ConfigurationKeyName("CachingService_PdfSource")]
    public string CachingServicePdfSource { get; set; } = string.Empty;
    
    [ConfigurationKeyName("Email_PrivateKey")]
    public string EmailPrivateKey { get; set; } = string.Empty;
    
    [ConfigurationKeyName("Email_HealthUrl")]
    public string EmailHealthUrl { get; set; } = string.Empty;
    
    [ConfigurationKeyName("Email_BaseUrl")]
    public string EmailBaseUrl { get; set; } = string.Empty;
    
    [ConfigurationKeyName("Email_Address_Admin")]
    public string EmailAddressAdmin  { get; set; } = string.Empty;
    
    [ConfigurationKeyName("Email_Address_Contact")]
    public string EmailAddressContact { get; set; } = string.Empty;

    [ConfigurationKeyName("Email_Address_Support")]
    public string EmailAddressSupport { get; set; } = string.Empty;

    [ConfigurationKeyName("Db_DatabaseContext")]
    public string DbDatabaseContext { get; set; } = string.Empty;

    [ConfigurationKeyName("Db_DatabaseContext_Migrator")]
    public string DbDatabaseContextMigrator { get; set; } = string.Empty;

    [ConfigurationKeyName("AZ_Bus_ConnectionString")]
    public string AzBusConnectionString { get; set; } = string.Empty;

    [ConfigurationKeyName("AZ_Storage_BaseUrl")]
    public string AzStorageBaseUrl { get; set; } = string.Empty;

    [ConfigurationKeyName("AZ_Storage_ContainerName")]
    public string AzStorageContainerName { get; set; } = string.Empty;

    [ConfigurationKeyName("AZ_Storage_ConnectionString")]
    public string AzStorageConnectionString { get; set; } = string.Empty;

    [ConfigurationKeyName("AZ_Storage_MaxFileSizeUserMedia")]
    public int AzStorageMaxFileSizeUserMedia { get; set; } = 104857600;

    [ConfigurationKeyName("AZ_Storage_MaxFileSizeSingleAsset")]
    public int AzStorageMaxFileSizeSingleAsset { get; set; } = 262144000;

    [ConfigurationKeyName("AZ_Redis_InstanceName")]
    public string AzRedisInstanceName { get; set; } = string.Empty;
    
    [ConfigurationKeyName("AZ_Redis_ConnectionString")]
    public string AzRedisConnectionString { get; set; } = string.Empty;

    [ConfigurationKeyName("AZ_Redis_ExpirationMinute")]
    public int AzRedisExpirationMinute { get; set; } = 15;

    [ConfigurationKeyName("AZ_Redis_DatabaseId")]
    public int AzRedisDatabaseId { get; set; } = 30;

    [ConfigurationKeyName("AZ_Hub_HubName")]
    public string AzHubHubName { get; set; } = string.Empty;

    [ConfigurationKeyName("AZ_Hub_ServiceEndpoint")]
    public string AzHubServiceEndpoint { get; set; } = string.Empty;
    
    [ConfigurationKeyName("AZ_Hub_ConnectionString")]
    public string AzHubConnectionString { get; set; } = string.Empty;
    
    [ConfigurationKeyName("Paths_NewsletterUpdate")]
    public string PathsNewsletterUpdate {get; set;} = string.Empty;
    
    [ConfigurationKeyName("Paths_NewsletterRemove")]
    public string PathsNewsletterRemove { get; set; } = string.Empty;
    
    [ConfigurationKeyName("Paths_UpdatePassword")]
    public string PathsUpdatePassword { get; set; } = string.Empty;
    
    [ConfigurationKeyName("Paths_Activation")]
    public string PathsActivation { get; set; } = string.Empty;
    
    [ConfigurationKeyName("Paths_DevelopmentOrigin")]
    public string PathsDevelopmentOrigin { get; set; } = string.Empty;
    
    [ConfigurationKeyName("Paths_DeploymentOrigin")]
    public string PathsDeploymentOrigin { get; set; } = string.Empty;
    
    [ConfigurationKeyName("Paths_Templates_Newsletter")]
    public string PathsTemplatesNewsletter { get; set; } = string.Empty;

    [ConfigurationKeyName("Paths_Templates_ContactForm")]
    public string PathsTemplatesContactForm { get; set; } = string.Empty;

    [ConfigurationKeyName("Paths_Templates_ResetPassword")]
    public string PathsTemplatesResetPassword { get; set; } = string.Empty;

    [ConfigurationKeyName("Paths_Templates_RegisterForm")]
    public string PathsTemplatesRegisterForm { get; set; } = string.Empty;

    [ConfigurationKeyName("Paths_Templates_VerifyEmail")]
    public string PathsTemplatesVerifyEmail { get; set; } = string.Empty;

    [ConfigurationKeyName("Ids_Issuer")]
    public string IdsIssuer { get; set; } = string.Empty;

    [ConfigurationKeyName("Ids_Audience")]
    public string IdsAudience { get; set; } = string.Empty;

    [ConfigurationKeyName("Ids_WebSecret")]
    public string IdsWebSecret { get; set; } = string.Empty;

    [ConfigurationKeyName("Ids_RequireHttps")]
    public bool IdsRequireHttps { get; set; }

    [ConfigurationKeyName("Ids_WebToken_Maturity")]
    public int IdsWebTokenMaturity { get; set; } = 90;

    [ConfigurationKeyName("Ids_RefreshToken_Maturity")]
    public int IdsRefreshTokenMaturity { get; set; } = 120;

    [ConfigurationKeyName("Limit_Reset_Maturity")]
    public int LimitResetMaturity { get; set; } = 30;

    [ConfigurationKeyName("Limit_Activation_Maturity")]
    public int LimitActivationMaturity { get; set; } = 30;

    [ConfigurationKeyName("Limit_Likes_Anonymous")]
    public int LimitLikesAnonymous { get; set; } = 25;

    [ConfigurationKeyName("Limit_Likes_User")]
    public int LimitLikesUser { get; set; } = 50;
    
    [ConfigurationKeyName("Pmt_MerchantPosId")]
    public string PmtMerchantPosId { get; set; } = string.Empty;
    
    [ConfigurationKeyName("Pmt_ClientId")]
    public string PmtClientId { get; set; } = string.Empty;
    
    [ConfigurationKeyName("Pmt_ClientSecret")]
    public string PmtClientSecret { get; set; } = string.Empty;
    
    [ConfigurationKeyName("Pmt_BaseUrl")]
    public string PmtBaseUrl { get; set; } = string.Empty;
    
    [ConfigurationKeyName("Pmt_Address_Authorize")]
    public string PmtAddressAuthorize { get; set; } = string.Empty;
    
    [ConfigurationKeyName("Pmt_Address_Tokens")]
    public string PmtAddressTokens { get; set; } = string.Empty;
    
    [ConfigurationKeyName("Pmt_Address_Orders")]
    public string PmtAddressOrders { get; set; } = string.Empty;
    
    [ConfigurationKeyName("Pmt_Address_Payouts")]
    public string PmtAddressPayouts { get; set; } = string.Empty;
    
    [ConfigurationKeyName("Pmt_Address_PayMethods")]
    public string PmtAddressPayMethods { get; set; } = string.Empty;

    [ConfigurationKeyName("Pmt_Address_Reports")]
    public string PmtAddressReports { get; set; } = string.Empty;
}