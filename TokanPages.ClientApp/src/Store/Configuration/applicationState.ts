import { 
    IActivateAccount,
    IAddSubscriber,
    IArticle,
    IArticles,
    IGetAccountContent,
    IGetActivateAccountContent,
    IGetArticleFeaturesContent,
    IGetClientsContent,
    IGetContactFormContent,
    IGetCookiesPromptContent,
    IGetFeaturedContent,
    IGetFeaturesContent,
    IGetFooterContent,
    IGetHeaderContent,
    IGetNavigationContent,
    IGetNewsletterContent,
    IGetResetPasswordContent,
    IGetTestimonialsContent,
    IGetUnsubscribeContent,
    IGetUpdatePasswordContent,
    IGetUpdateSubscriberContent,
    IGetUserSigninContent,
    IGetUserSignoutContent,
    IGetUserSignupContent,
    IGetWrongPagePromptContent,
    IRaiseDialog,
    IRaiseError,
    IReAuthenticateUser,
    IRemoveAccount,
    IRemoveSubscriber,
    IResetUserPassword,
    ISendMessage,
    ISigninUser,
    ISignupUser,
    IStoreUserData,
    IUpdateArticle,
    IUpdateSubscriber,
    IUpdateUser,
    IUpdateUserPassword,
    IUserLanguage,
    IGetPolicyContent,
    IGetTermsContent,
    IGetStoryContent
} from "../States";

export interface IApplicationState 
{
    applicationDialog: IRaiseDialog;
    applicationError: IRaiseError;
    applicationLanguage: IUserLanguage;
    applicationMessage: ISendMessage;
    articleListing: IArticles;
    articleSelection: IArticle;
    articleUpdated: IUpdateArticle;
    contentAccount: IGetAccountContent;
    contentActivateAccount: IGetActivateAccountContent;
    contentArticleFeatures: IGetArticleFeaturesContent;
    contentClients: IGetClientsContent;
    contentContactForm: IGetContactFormContent;
    contentCookiesPrompt: IGetCookiesPromptContent;
    contentFeatured: IGetFeaturedContent;
    contentFeatures: IGetFeaturesContent;
    contentFooter: IGetFooterContent;
    contentHeader: IGetHeaderContent;
    contentNavigation: IGetNavigationContent;
    contentNewsletter: IGetNewsletterContent;
    contentPolicy: IGetPolicyContent;
    contentResetPassword: IGetResetPasswordContent;
    contentStory: IGetStoryContent;
    contentTerms: IGetTermsContent;
    contentTestimonials: IGetTestimonialsContent;
    contentUnsubscribe: IGetUnsubscribeContent;
    contentUpdatePassword: IGetUpdatePasswordContent;
    contentUpdateSubscriber: IGetUpdateSubscriberContent;
    contentUserSignin: IGetUserSigninContent;
    contentUserSignout: IGetUserSignoutContent;
    contentUserSignup: IGetUserSignupContent;
    contentWrongPagePrompt: IGetWrongPagePromptContent;
    subscriberAdd: IAddSubscriber;
    subscriberRemove: IRemoveSubscriber;
    subscriberUpdate: IUpdateSubscriber;
    userActivate: IActivateAccount;
    userDataStore: IStoreUserData;
    userPasswordReset: IResetUserPassword;
    userPasswordUpdate: IUpdateUserPassword;
    userReAuthenticate: IReAuthenticateUser;
    userRemove: IRemoveAccount;
    userSignin: ISigninUser;
    userSignup: ISignupUser;
    userUpdate: IUpdateUser;
}

export interface AppThunkAction<TAction> 
{
    (dispatch: (action: TAction) => void, getState: () => IApplicationState): void;
}
