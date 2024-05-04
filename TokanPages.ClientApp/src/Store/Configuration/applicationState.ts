import {
    UserActivateState,
    NewsletterAddState,
    ArticleSelectionState,
    ArticleListingState,
    ContentAccountState,
    ContentActivateAccountState,
    ContentArticleFeaturesState,
    ContentClientsState,
    ContentContactFormState,
    ContentCookiesPromptState,
    ContentFeaturedState,
    ContentTechnologiesState,
    ContentFooterState,
    ContentHeaderState,
    ContentNavigationState,
    ContentNewsletterState,
    ContentResetPasswordState,
    ContentTestimonialsState,
    ContentNewsletterRemoveState,
    ContentUpdatePasswordState,
    ContentNewsletterUpdateState,
    ContentUserSigninState,
    ContentUserSignoutState,
    ContentUserSignupState,
    ContentWrongPagePromptState,
    ApplicationDialogState,
    ApplicationErrorState,
    UserReAuthenticateState,
    UserRemoveState,
    NewsletterRemoveState,
    UserPasswordResetState,
    ApplicationEmailState,
    UserSigninState,
    UserSignupState,
    UserDataStoreState,
    UserUpdateState,
    NewsletterUpdateState,
    ArticleUpdateState,
    UserPasswordUpdateState,
    ApplicationLanguageState,
    ContentPolicyState,
    ContentTermsState,
    ContentStoryState,
    UserMediaUploadState,
    UserSignoutState,
    UserNotificationState,
    ContentTemplatesState,
    UserEmailVerificationState,
    ContentShowcaseState,
    ContentArticleState,
} from "../States";

export interface ApplicationState {
    applicationError: ApplicationErrorState;
    applicationEmail: ApplicationEmailState;
    applicationDialog: ApplicationDialogState;
    applicationLanguage: ApplicationLanguageState;
    articleListing: ArticleListingState;
    articleSelection: ArticleSelectionState;
    articleUpdate: ArticleUpdateState;
    contentTemplates: ContentTemplatesState;
    contentAccount: ContentAccountState;
    contentActivateAccount: ContentActivateAccountState;
    contentArticle: ContentArticleState;
    contentArticleFeatures: ContentArticleFeaturesState;
    contentClients: ContentClientsState;
    contentContactForm: ContentContactFormState;
    contentCookiesPrompt: ContentCookiesPromptState;
    contentFeatured: ContentFeaturedState;
    contentTechnologies: ContentTechnologiesState;
    contentFooter: ContentFooterState;
    contentHeader: ContentHeaderState;
    contentNavigation: ContentNavigationState;
    contentNewsletter: ContentNewsletterState;
    contentPolicy: ContentPolicyState;
    contentResetPassword: ContentResetPasswordState;
    contentStory: ContentStoryState;
    contentShowcase: ContentShowcaseState;
    contentTerms: ContentTermsState;
    contentTestimonials: ContentTestimonialsState;
    contentNewsletterRemove: ContentNewsletterRemoveState;
    contentUpdatePassword: ContentUpdatePasswordState;
    contentNewsletterUpdate: ContentNewsletterUpdateState;
    contentUserSignin: ContentUserSigninState;
    contentUserSignout: ContentUserSignoutState;
    contentUserSignup: ContentUserSignupState;
    contentWrongPagePrompt: ContentWrongPagePromptState;
    newsletterAdd: NewsletterAddState;
    newsletterRemove: NewsletterRemoveState;
    newsletterUpdate: NewsletterUpdateState;
    userActivate: UserActivateState;
    userEmailVerification: UserEmailVerificationState;
    userDataStore: UserDataStoreState;
    userMediaUpload: UserMediaUploadState;
    userNotification: UserNotificationState;
    userPasswordReset: UserPasswordResetState;
    userPasswordUpdate: UserPasswordUpdateState;
    userReAuthenticate: UserReAuthenticateState;
    userRemove: UserRemoveState;
    userSignin: UserSigninState;
    userSignout: UserSignoutState;
    userSignup: UserSignupState;
    userUpdate: UserUpdateState;
}
