import { IArticle } from "./States/Articles/selectArticleState";
import { IArticles } from "./States/Articles/listArticlesState";
import { IUpdateArticle } from "./States/Articles/updateArticleState";
import { IAddSubscriber } from "./States/Subscribers/addSubscriberState";
import { IUpdateSubscriber } from "./States/Subscribers/updateSubscriberState";
import { IRemoveSubscriber } from "./States/Subscribers/removeSubscriberState";
import { IStoreUserData } from "./States/Users/storeUserDataState";
import { ISigninUser } from "./States/Users/signinUserState";
import { ISignupUser } from "./States/Users/signupUserState";
import { IResetUserPassword } from "./States/Users/resetUserPasswordState";
import { IUpdateUserPassword } from "./States/Users/updateUserPasswordState";
import { IActivateAccount } from "./States/Users/activateAccountState";
import { ISendMessage } from "./States/Mailer/sendMessageState";
import { IRaiseError } from "./States/raiseErrorState";
import { IRaiseDialog } from "./States/raiseDialogState";
import { IGetAccountContent } from "./States/Content/getAccountContentState";
import { IGetStaticContent } from "./States/Content/getStaticContentState";
import { IGetArticleFeatContent } from "./States/Content/getArticleFeatContentState";
import { IGetContactFormContent } from "./States/Content/getContactFormContentState";
import { IGetCookiesPromptContent } from "./States/Content/getCookiesPromptContentState";
import { IGetClientsContent } from "./States/Content/getClientsContentState";
import { IGetFeaturedContent } from "./States/Content/getFeaturedContentState";
import { IGetFeaturesContent } from "./States/Content/getFeaturesContentState";
import { IGetFooterContent } from "./States/Content/getFooterContentState";
import { IGetHeaderContent } from "./States/Content/getHeaderContentState";
import { IGetNavigationContent } from "./States/Content/getNavigationContentState";
import { IGetNewsletterContent } from "./States/Content/getNewsletterContentState";
import { IGetWrongPagePromptContent } from "./States/Content/getWrongPagePromptContentState";
import { IGetResetPasswordContent } from "./States/Content/getResetPasswordContentState";
import { IGetUpdatePasswordContent } from "./States/Content/getUpdatePasswordContentState";
import { IGetUserSigninContent } from "./States/Content/getUserSigninContentState";
import { IGetUserSignoutContent } from "./States/Content/getUserSignoutContentState";
import { IGetUserSignupContent } from "./States/Content/getUserSignupContentState";
import { IGetTestimonialsContent } from "./States/Content/getTestimonialsContentState";
import { IGetUnsubscribeContent } from "./States/Content/getUnsubscribeContentState";
import { IGetActivateAccountContent } from "./States/Content/getActivateAccountContentState";
import { IGetUpdateSubscriberContent } from "./States/Content/getUpdateSubscriberContentState";
import { IUpdateUser } from "./States/Users/updateUserState";

export interface IApplicationState 
{
    raiseError: IRaiseError;
    raiseDialog: IRaiseDialog;

    selectArticle: IArticle;
    listArticles: IArticles;
    storeUserData: IStoreUserData,
    
    updateArticle: IUpdateArticle,
    sendMessage: ISendMessage,
    addSubscriber: IAddSubscriber,
    updateSubscriber: IUpdateSubscriber,
    removeSubscriber: IRemoveSubscriber,
    signinUser: ISigninUser,
    signupUser: ISignupUser,
    resetUserPassword: IResetUserPassword,
    updateUserPassword: IUpdateUserPassword,
    activateAccount: IActivateAccount,
    updateUser: IUpdateUser,

    getAccountContent: IGetAccountContent,
    getStaticContent: IGetStaticContent,
    getArticleFeatContent: IGetArticleFeatContent,
    getContactFormContent: IGetContactFormContent,
    getCookiesPromptContent: IGetCookiesPromptContent,
    getClientsContent: IGetClientsContent,
    getFeaturedContent: IGetFeaturedContent,
    getFeaturesContent: IGetFeaturesContent,
    getFooterContent: IGetFooterContent,
    getHeaderContent: IGetHeaderContent,
    getNavigationContent: IGetNavigationContent,
    getNewsletterContent: IGetNewsletterContent,
    getWrongPagePromptContent: IGetWrongPagePromptContent,
    getResetPasswordContent: IGetResetPasswordContent,
    getUpdatePasswordContent: IGetUpdatePasswordContent,
    getUserSigninContent: IGetUserSigninContent,
    getUserSignoutContent: IGetUserSignoutContent,
    getUserSignupContent: IGetUserSignupContent,
    getTestimonialsContent: IGetTestimonialsContent,
    getUnsubscribeContent: IGetUnsubscribeContent,
    getActivateAccountContent: IGetActivateAccountContent,
    getUpdateSubscriberContent: IGetUpdateSubscriberContent
}

export interface AppThunkAction<TAction> 
{
    (dispatch: (action: TAction) => void, getState: () => IApplicationState): void;
}
