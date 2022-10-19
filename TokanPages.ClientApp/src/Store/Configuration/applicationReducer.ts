import { 
    UserActivate,
    SubscriberAdd,
    ContentAccount,
    ContentActivateAccount,
    ContentArticleFeatures,
    ContentClients,
    ContentContactForm,
    ContentCookiesPrompt,
    ContentFeatured,
    ContentFeatures,
    ContentFooter,
    ContentHeader,
    ContentNavigation,
    ContentNewsletter,
    ContentResetPassword,
    ContentTestimonials,
    ContentUnsubscribe,
    ContentUpdatePassword,
    ContentUpdateSubscriber,
    ContentUserSignin,
    ContentUserSignout,
    ContentUserSignup,
    ContentWrongPagePrompt,
    ArticleListing,
    ApplicationDialog,
    ApplicationError,
    UserReAuthenticate,
    UserRemove,
    SubscriberRemove,
    UserPasswordReset,
    ArticleSelection,
    ApplicationEmail,
    UserSignin,
    UserSignup,
    UserDataStore,
    ArticleUpdate,
    SubscriberUpdate,
    UserPasswordUpdate,
    UserUpdate,
    ApplicationLanguage,
    ContentPolicy,
    ContentTerms,
    ContentStory
} from "../Reducers";

export const ApplicationReducer = 
{
    applicationError: ApplicationError,
    applicationEmail: ApplicationEmail,
    applicationDialog: ApplicationDialog,
    applicationLanguage: ApplicationLanguage,
    articleListing: ArticleListing,
    articleSelection: ArticleSelection,
    articleUpdate: ArticleUpdate,
    contentAccount: ContentAccount,
    contentActivateAccount: ContentActivateAccount,
    contentArticleFeatures: ContentArticleFeatures,
    contentClients: ContentClients,
    contentContactForm: ContentContactForm,
    contentCookiesPrompt: ContentCookiesPrompt,
    contentFeatured: ContentFeatured,
    contentFeatures: ContentFeatures,
    contentFooter: ContentFooter,
    contentHeader: ContentHeader,
    contentNavigation: ContentNavigation,
    contentNewsletter: ContentNewsletter,
    contentPolicy: ContentPolicy,
    contentResetPassword: ContentResetPassword,
    contentStory: ContentStory,
    contentTerms: ContentTerms,
    contentTestimonials: ContentTestimonials,
    contentUnsubscribe: ContentUnsubscribe,
    contentUpdatePassword: ContentUpdatePassword,
    contentUpdateSubscriber: ContentUpdateSubscriber,
    contentUserSignin: ContentUserSignin,
    contentUserSignout: ContentUserSignout,
    contentUserSignup: ContentUserSignup,
    contentWrongPagePrompt: ContentWrongPagePrompt,
    subscriberAdd: SubscriberAdd,
    subscriberRemove: SubscriberRemove,
    subscriberUpdate: SubscriberUpdate,
    userActivate: UserActivate,
    userDataStore: UserDataStore,
    userPasswordReset: UserPasswordReset,
    userPasswordUpdate: UserPasswordUpdate,
    userReAuthenticate: UserReAuthenticate,
    userRemove: UserRemove,
    userSignin: UserSignin,
    userSignup: UserSignup,
    userUpdate: UserUpdate
};
