import { IActivateAccount } from "./States/Users/activateAccountState";
import { IAddSubscriber } from "./States/Subscribers/addSubscriberState";
import { IArticle } from "./States/Articles/selectArticleState";
import { IArticles } from "./States/Articles/listArticlesState";
import { IGetAccountContent } from "./States/Content/getAccountContentState";
import { IGetActivateAccountContent } from "./States/Content/getActivateAccountContentState";
import { IGetArticleFeaturesContent } from "./States/Content/getArticleFeaturesContentState";
import { IGetClientsContent } from "./States/Content/getClientsContentState";
import { IGetContactFormContent } from "./States/Content/getContactFormContentState";
import { IGetCookiesPromptContent } from "./States/Content/getCookiesPromptContentState";
import { IGetFeaturedContent } from "./States/Content/getFeaturedContentState";
import { IGetFeaturesContent } from "./States/Content/getFeaturesContentState";
import { IGetFooterContent } from "./States/Content/getFooterContentState";
import { IGetHeaderContent } from "./States/Content/getHeaderContentState";
import { IGetNavigationContent } from "./States/Content/getNavigationContentState";
import { IGetNewsletterContent } from "./States/Content/getNewsletterContentState";
import { IGetResetPasswordContent } from "./States/Content/getResetPasswordContentState";

import { IGetTestimonialsContent } from "./States/Content/getTestimonialsContentState";
import { IGetUnsubscribeContent } from "./States/Content/getUnsubscribeContentState";
import { IGetUpdatePasswordContent } from "./States/Content/getUpdatePasswordContentState";
import { IGetUpdateSubscriberContent } from "./States/Content/getUpdateSubscriberContentState";
import { IGetUserSigninContent } from "./States/Content/getUserSigninContentState";
import { IGetUserSignoutContent } from "./States/Content/getUserSignoutContentState";
import { IGetUserSignupContent } from "./States/Content/getUserSignupContentState";
import { IGetWrongPagePromptContent } from "./States/Content/getWrongPagePromptContentState";
import { IRaiseDialog } from "./States/raiseDialogState";
import { IRaiseError } from "./States/raiseErrorState";
import { IReAuthenticateUser } from "./States/Users/reAuthenticateUserState";
import { IRemoveAccount } from "./States/Users/removeAccountState";
import { IRemoveSubscriber } from "./States/Subscribers/removeSubscriberState";
import { IResetUserPassword } from "./States/Users/resetUserPasswordState";
import { ISendMessage } from "./States/Mailer/sendMessageState";
import { ISigninUser } from "./States/Users/signinUserState";
import { ISignupUser } from "./States/Users/signupUserState";
import { IStoreUserData } from "./States/Users/storeUserDataState";
import { IUpdateArticle } from "./States/Articles/updateArticleState";
import { IUpdateSubscriber } from "./States/Subscribers/updateSubscriberState";
import { IUpdateUser } from "./States/Users/updateUserState";
import { IUpdateUserPassword } from "./States/Users/updateUserPasswordState";
import { IUserLanguage } from "./States/userLanguageState";

export interface IApplicationState 
{
    raiseError: IRaiseError;
    raiseDialog: IRaiseDialog;

    userLanguage: IUserLanguage,

    listArticles: IArticles;
    selectArticle: IArticle;
    storeUserData: IStoreUserData,

    sendMessage: ISendMessage,
    updateArticle: IUpdateArticle,
    addSubscriber: IAddSubscriber,
    updateSubscriber: IUpdateSubscriber,
    removeSubscriber: IRemoveSubscriber,
    removeAccount: IRemoveAccount,
    activateAccount: IActivateAccount,
    reAuthenticateUser: IReAuthenticateUser,
    signinUser: ISigninUser,
    signupUser: ISignupUser,
    updateUser: IUpdateUser,
    updateUserPassword: IUpdateUserPassword,
    resetUserPassword: IResetUserPassword,

    getNavigationContent: IGetNavigationContent,
    getHeaderContent: IGetHeaderContent,
    getFooterContent: IGetFooterContent,

    getAccountContent: IGetAccountContent,
    getActivateAccountContent: IGetActivateAccountContent,

    getClientsContent: IGetClientsContent,
    getFeaturesContent: IGetFeaturesContent,
    getArticleFeaturesContent: IGetArticleFeaturesContent,
    getFeaturedContent: IGetFeaturedContent,
    getTestimonialsContent: IGetTestimonialsContent,
    getNewsletterContent: IGetNewsletterContent,
    getContactFormContent: IGetContactFormContent,
    getCookiesPromptContent: IGetCookiesPromptContent,
    getWrongPagePromptContent: IGetWrongPagePromptContent,

    getUserSigninContent: IGetUserSigninContent,
    getUserSignupContent: IGetUserSignupContent,
    getUserSignoutContent: IGetUserSignoutContent,
    getUpdatePasswordContent: IGetUpdatePasswordContent,
    getResetPasswordContent: IGetResetPasswordContent,

    getUnsubscribeContent: IGetUnsubscribeContent,
    getUpdateSubscriberContent: IGetUpdateSubscriberContent
}

export interface AppThunkAction<TAction> 
{
    (dispatch: (action: TAction) => void, getState: () => IApplicationState): void;
}
