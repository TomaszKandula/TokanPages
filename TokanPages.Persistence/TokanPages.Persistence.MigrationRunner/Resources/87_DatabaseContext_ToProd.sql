SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET XACT_ABORT ON
SET NOCOUNT ON

BEGIN TRY

    BEGIN TRANSACTION

    INSERT INTO [{{TARGET_TABLE}}].[dbo].[Roles]
    SELECT
        [Id],
        [Name],
        [Description],
        [CreatedAt],
        [CreatedBy],
        [ModifiedAt],
        [ModifiedBy]
    FROM [{{SOURCE_TABLE}}].[dbo].[Roles]

    INSERT INTO [{{TARGET_TABLE}}].[dbo].[Permissions]
    SELECT
        [Id],
        [Name],
        [CreatedAt],
        [CreatedBy],
        [ModifiedAt],
        [ModifiedBy]
    FROM [{{SOURCE_TABLE}}].[dbo].[Permissions]

    INSERT INTO [{{TARGET_TABLE}}].[dbo].[Users]
    SELECT
        [Id],
        [UserAlias],
        [IsActivated],
        [EmailAddress],
        [CryptedPassword],
        [ResetId],
        [ResetIdEnds],
        [ActivationId],
        [ActivationIdEnds],
        [CreatedAt],
        [CreatedBy],
        [ModifiedAt],
        [ModifiedBy],
        [IsDeleted]
    FROM [{{SOURCE_TABLE}}].[dbo].[Users]

    INSERT INTO [{{TARGET_TABLE}}].[dbo].[UserInfo]
    SELECT
        [Id],
        [UserId],
        [FirstName],
        [LastName],
        [UserAboutText],
        [UserImageName],
        [UserVideoName],
        [CreatedBy],
        [CreatedAt],
        [ModifiedBy],
        [ModifiedAt]
    FROM [{{SOURCE_TABLE}}].[dbo].[UserInfo]

    INSERT INTO [{{TARGET_TABLE}}].[dbo].[UserTokens]
    SELECT
        [Id],
        [UserId],
        [Token],
        [Expires],
        [Created],
        [CreatedByIp],
        [Command],
        [ReasonRevoked],
        [Revoked],
        [RevokedByIp]
    FROM [{{SOURCE_TABLE}}].[dbo].[UserTokens]

    INSERT INTO [{{TARGET_TABLE}}].[dbo].[UserRefreshTokens]
    SELECT
        [Id],
        [UserId],
        [Token],
        [Expires],
        [Created],
        [CreatedByIp],
        [Revoked],
        [RevokedByIp],
        [ReplacedByToken],
        [ReasonRevoked]
    FROM [{{SOURCE_TABLE}}].[dbo].[UserRefreshTokens]

    INSERT INTO [{{TARGET_TABLE}}].[dbo].[UserRoles]
    SELECT
        [Id],
        [UserId],
        [RoleId],
        [CreatedAt],
        [CreatedBy],
        [ModifiedAt],
        [ModifiedBy]
    FROM [{{SOURCE_TABLE}}].[dbo].[UserRoles]

    INSERT INTO [{{TARGET_TABLE}}].[dbo].[UserPermissions]
    SELECT
        [Id],
        [UserId],
        [PermissionId],
        [CreatedAt],
        [CreatedBy],
        [ModifiedAt],
        [ModifiedBy]
    FROM [{{SOURCE_TABLE}}].[dbo].[UserPermissions]

    INSERT INTO [{{TARGET_TABLE}}].[dbo].[UserPhotos]
    SELECT
        [Id],
        [UserId],
        [PhotoGearId],
        [PhotoCategoryId],
        [Keywords],
        [PhotoUrl],
        [DateTaken],
        [Title],
        [Description],
        [CreatedAt],
        [CreatedBy],
        [ModifiedAt],
        [ModifiedBy]
    FROM [{{SOURCE_TABLE}}].[dbo].[UserPhotos]

    INSERT INTO [{{TARGET_TABLE}}].[dbo].[PhotoGears]
    SELECT
        [Id],
        [BodyVendor],
        [BodyModel],
        [LensVendor],
        [LensName],
        [FocalLength],
        [ShutterSpeed],
        [Aperture],
        [FilmIso],
        [CreatedAt],
        [CreatedBy],
        [ModifiedAt],
        [ModifiedBy]
    FROM [{{SOURCE_TABLE}}].[dbo].[PhotoGears]

    INSERT INTO [{{TARGET_TABLE}}].[dbo].[PhotoCategories]
    SELECT
        [Id],
        [CategoryName],
        [CreatedAt],
        [CreatedBy],
        [ModifiedAt],
        [ModifiedBy]
    FROM [{{SOURCE_TABLE}}].[dbo].[PhotoCategories]

    INSERT INTO [{{TARGET_TABLE}}].[dbo].[Articles]
    SELECT
        [Id],
        [Title],
        [Description],
        [IsPublished],
        [ReadCount],
        [CreatedAt],
        [UpdatedAt],
        [UserId]
    FROM [{{SOURCE_TABLE}}].[dbo].[Articles]

    INSERT INTO [{{TARGET_TABLE}}].[dbo].[ArticleLikes]
    SELECT
        [Id],
        [ArticleId],
        [UserId],
        [IpAddress],
        [LikeCount],
        [CreatedAt],
        [CreatedBy],
        [ModifiedAt],
        [ModifiedBy]
    FROM [{{SOURCE_TABLE}}].[dbo].[ArticleLikes]

    INSERT INTO [{{TARGET_TABLE}}].[dbo].[ArticleCounts]
    SELECT
        [Id],
        [ArticleId],
        [UserId],
        [IpAddress],
        [ReadCount],
        [CreatedAt],
        [CreatedBy],
        [ModifiedAt],
        [ModifiedBy]
    FROM [{{SOURCE_TABLE}}].[dbo].[ArticleCounts]

    INSERT INTO [{{TARGET_TABLE}}].[dbo].[DefaultPermissions]
    SELECT
        [Id],
        [RoleId],
        [PermissionId],
        [CreatedAt],
        [CreatedBy],
        [ModifiedAt],
        [ModifiedBy]
    FROM [{{SOURCE_TABLE}}].[dbo].[DefaultPermissions]

    INSERT INTO [{{TARGET_TABLE}}].[dbo].[Subscribers]
    SELECT
        [Id],
        [Email],
        [IsActivated],
        [Count],
        [CreatedAt],
        [CreatedBy],
        [ModifiedAt],
        [ModifiedBy]
    FROM [{{SOURCE_TABLE}}].[dbo].[Subscribers]

    INSERT INTO [{{TARGET_TABLE}}].[dbo].[HttpRequests]
    SELECT
        [Id],
        [SourceAddress],
        [RequestedAt],
        [RequestedHandlerName]
    FROM [{{SOURCE_TABLE}}].[dbo].[HttpRequests]

    INSERT INTO [{{TARGET_TABLE}}].[dbo].[Albums]
    SELECT
        [Id],
        [UserId],
        [Title],
        [UserPhotoId],
        [CreatedAt],
        [CreatedBy],
        [ModifiedAt],
        [ModifiedBy]
    FROM [{{SOURCE_TABLE}}].[dbo].[Albums]

    OPTION (RECOMPILE)
    COMMIT TRANSACTION

END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
    DECLARE @ErrorMsg NVARCHAR(2048) = error_message()
	RAISERROR (@ErrorMsg, 16, 1)
END CATCH
