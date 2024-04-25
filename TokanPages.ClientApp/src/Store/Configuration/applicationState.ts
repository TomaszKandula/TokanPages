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
    ContentUnsubscribeState,
    ContentUpdatePasswordState,
    ContentUpdateSubscriberState,
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
} from "../States";

export interface ApplicationState {
    applicationError: ApplicationErrorState;
    applicationEmail: ApplicationEmailState;
    applicationDialog: ApplicationDialogState;
    applicationLanguage: ApplicationLanguageState;
    articleListing: ArticleListingState;
    articleSelection: ArticleSelectionState;
    articleUpdate: ArticleUpdateState;
    contentAccount: ContentAccountState;
    contentActivateAccount: ContentActivateAccountState;
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
    contentTerms: ContentTermsState;
    contentTestimonials: ContentTestimonialsState;
    contentUnsubscribe: ContentUnsubscribeState;
    contentUpdatePassword: ContentUpdatePasswordState;
    contentUpdateSubscriber: ContentUpdateSubscriberState;
    contentUserSignin: ContentUserSigninState;
    contentUserSignout: ContentUserSignoutState;
    contentUserSignup: ContentUserSignupState;
    contentWrongPagePrompt: ContentWrongPagePromptState;
    newsletterAdd: NewsletterAddState;
    newsletterRemove: NewsletterRemoveState;
    newsletterUpdate: NewsletterUpdateState;
    userActivate: UserActivateState;
    userDataStore: UserDataStoreState;
    userMediaUpload: UserMediaUploadState;
    userPasswordReset: UserPasswordResetState;
    userPasswordUpdate: UserPasswordUpdateState;
    userReAuthenticate: UserReAuthenticateState;
    userRemove: UserRemoveState;
    userSignin: UserSigninState;
    userSignout: UserSignoutState;
    userSignup: UserSignupState;
    userUpdate: UserUpdateState;
}
