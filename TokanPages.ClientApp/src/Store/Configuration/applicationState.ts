import { 
    IUserActivate,
    ISubscriberAdd,
    IArticleSelection,
    IArticleListing,
    IContentAccount,
    IContentActivateAccount,
    IContentArticleFeatures,
    IContentClients,
    IContentContactForm,
    IContentCookiesPrompt,
    IContentFeatured,
    IContentFeatures,
    IContentFooter,
    IContentHeader,
    IContentNavigation,
    IContentNewsletter,
    IContentResetPassword,
    IContentTestimonials,
    IContentUnsubscribe,
    IContentUpdatePassword,
    IContentUpdateSubscriber,
    IContentUserSignin,
    IContentUserSignout,
    IContentUserSignup,
    IContentWrongPagePrompt,
    IApplicationDialog,
    IApplicationError,
    IUserReAuthenticate,
    IUserRemove,
    ISubscriberRemove,
    IUserPasswordReset,
    IApplicationEmail,
    IUserSignin,
    IUserSignup,
    IUserDataStore,
    IArticleUpdate,
    ISubscriberUpdate,
    IUserUpdate,
    IUserPasswordUpdate,
    IApplicationLanguage,
    IContentPolicy,
    IContentTerms,
    IContentStory
} from "../States";

export interface IApplicationState 
{
    applicationError: IApplicationError;
    applicationEmail: IApplicationEmail;
    applicationDialog: IApplicationDialog;
    applicationLanguage: IApplicationLanguage;
    articleListing: IArticleListing;
    articleSelection: IArticleSelection;
    articleUpdate: IArticleUpdate;
    contentAccount: IContentAccount;
    contentActivateAccount: IContentActivateAccount;
    contentArticleFeatures: IContentArticleFeatures;
    contentClients: IContentClients;
    contentContactForm: IContentContactForm;
    contentCookiesPrompt: IContentCookiesPrompt;
    contentFeatured: IContentFeatured;
    contentFeatures: IContentFeatures;
    contentFooter: IContentFooter;
    contentHeader: IContentHeader;
    contentNavigation: IContentNavigation;
    contentNewsletter: IContentNewsletter;
    contentPolicy: IContentPolicy;
    contentResetPassword: IContentResetPassword;
    contentStory: IContentStory;
    contentTerms: IContentTerms;
    contentTestimonials: IContentTestimonials;
    contentUnsubscribe: IContentUnsubscribe;
    contentUpdatePassword: IContentUpdatePassword;
    contentUpdateSubscriber: IContentUpdateSubscriber;
    contentUserSignin: IContentUserSignin;
    contentUserSignout: IContentUserSignout;
    contentUserSignup: IContentUserSignup;
    contentWrongPagePrompt: IContentWrongPagePrompt;
    subscriberAdd: ISubscriberAdd;
    subscriberRemove: ISubscriberRemove;
    subscriberUpdate: ISubscriberUpdate;
    userActivate: IUserActivate;
    userDataStore: IUserDataStore;
    userPasswordReset: IUserPasswordReset;
    userPasswordUpdate: IUserPasswordUpdate;
    userReAuthenticate: IUserReAuthenticate;
    userRemove: IUserRemove;
    userSignin: IUserSignin;
    userSignup: IUserSignup;
    userUpdate: IUserUpdate;
}
