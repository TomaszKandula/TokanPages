import {
    UserActivate,
    NewsletterAdd,
    ContentAccount,
    ContentActivateAccount,
    ContentArticleFeatures,
    ContentClients,
    ContentContactForm,
    ContentCookiesPrompt,
    ContentFeatured,
    ContentTechnologies,
    ContentFooter,
    ContentHeader,
    ContentNavigation,
    ContentNewsletter,
    ContentResetPassword,
    ContentTestimonials,
    ContentNewsletterRemove,
    ContentUpdatePassword,
    ContentNewsletterUpdate,
    ContentUserSignin,
    ContentUserSignout,
    ContentUserSignup,
    ContentWrongPagePrompt,
    ArticleListing,
    ApplicationDialog,
    ApplicationError,
    UserReAuthenticate,
    UserRemove,
    NewsletterRemove,
    UserPasswordReset,
    ArticleSelection,
    ApplicationEmail,
    UserSignin,
    UserSignup,
    UserDataStore,
    ArticleUpdate,
    NewsletterUpdate,
    UserPasswordUpdate,
    UserUpdate,
    ApplicationLanguage,
    ContentPolicy,
    ContentTerms,
    ContentStory,
    UserMediaUpload,
    UserSignout,
    UserNotification,
    ContentTemplates,
    UserEmailVerification,
    ContentShowcase,
    ContentArticle,
} from "../Reducers";

export const ApplicationReducer = {
    applicationError: ApplicationError,
    applicationEmail: ApplicationEmail,
    applicationDialog: ApplicationDialog,
    applicationLanguage: ApplicationLanguage,
    articleListing: ArticleListing,
    articleSelection: ArticleSelection,
    articleUpdate: ArticleUpdate,
    contentTemplates: ContentTemplates,
    contentAccount: ContentAccount,
    contentActivateAccount: ContentActivateAccount,
    contentArticle: ContentArticle,
    contentArticleFeatures: ContentArticleFeatures,
    contentClients: ContentClients,
    contentContactForm: ContentContactForm,
    contentCookiesPrompt: ContentCookiesPrompt,
    contentFeatured: ContentFeatured,
    contentTechnologies: ContentTechnologies,
    contentFooter: ContentFooter,
    contentHeader: ContentHeader,
    contentNavigation: ContentNavigation,
    contentNewsletter: ContentNewsletter,
    contentPolicy: ContentPolicy,
    contentResetPassword: ContentResetPassword,
    contentStory: ContentStory,
    contentShowcase: ContentShowcase,
    contentTerms: ContentTerms,
    contentTestimonials: ContentTestimonials,
    contentNewsletterRemove: ContentNewsletterRemove,
    contentUpdatePassword: ContentUpdatePassword,
    contentNewsletterUpdate: ContentNewsletterUpdate,
    contentUserSignin: ContentUserSignin,
    contentUserSignout: ContentUserSignout,
    contentUserSignup: ContentUserSignup,
    contentWrongPagePrompt: ContentWrongPagePrompt,
    newsletterAdd: NewsletterAdd,
    newsletterRemove: NewsletterRemove,
    newsletterUpdate: NewsletterUpdate,
    userActivate: UserActivate,
    userEmailVerification: UserEmailVerification,
    userDataStore: UserDataStore,
    userMediaUpload: UserMediaUpload,
    userNotification: UserNotification,
    userPasswordReset: UserPasswordReset,
    userPasswordUpdate: UserPasswordUpdate,
    userReAuthenticate: UserReAuthenticate,
    userRemove: UserRemove,
    userSignin: UserSignin,
    userSignout: UserSignout,
    userSignup: UserSignup,
    userUpdate: UserUpdate,
};
