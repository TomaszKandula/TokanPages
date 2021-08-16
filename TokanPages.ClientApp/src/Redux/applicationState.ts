import { IArticle } from "./States/selectArticleState";
import { IArticles } from "./States/listArticlesState";
import { ISendMessage } from "./States/sendMessageState";
import { IAddSubscriber } from "./States/addSubscriberState";
import { IUpdateSubscriber } from "./States/updateSubscriberState";
import { IRemoveSubscriber } from "./States/removeSubscriberState";
import { IUpdateArticle } from "./States/updateArticleState";
import { IRaiseError } from "./States/raiseErrorState";
import { IRaiseDialog } from "./States/raiseDialogState";
import { IUpdateUserData } from "./States/updateUserDataState";
import { ISigninUser } from "./States/signinUserState";
import { IGetStaticContent } from "./States/getStaticContentState";
import { IGetArticleFeatContent } from "./States/getArticleFeatContentState";
import { IGetContactFormContent } from "./States/getContactFormContentState";
import { IGetCookiesPromptContent } from "./States/getCookiesPromptContentState";
import { IGetFeaturedContent } from "./States/getFeaturedContentState";
import { IGetFeaturesContent } from "./States/getFeaturesContentState";
import { IGetFooterContent } from "./States/getFooterContentState";
import { IGetHeaderContent } from "./States/getHeaderContentState";
import { IGetNavigationContent } from "./States/getNavigationContentState";
import { IGetNewsletterContent } from "./States/getNewsletterContentState";
import { IGetWrongPagePromptContent } from "./States/getWrongPagePromptContentState";
import { IGetResetPasswordContent } from "./States/getResetPasswordContentState";
import { IGetUserSigninContent } from "./States/getUserSigninContentState";
import { IGetUserSignupContent } from "./States/getUserSignupContentState";
import { IGetTestimonialsContent } from "./States/getTestimonialsContentState";
import { IGetUnsubscribeContent } from "./States/getUnsubscribeContentState";
import { IGetUpdateSubscriberContent } from "./States/getUpdateSubscriberContentState";

export interface IApplicationState 
{
    raiseError: IRaiseError;
    raiseDialog: IRaiseDialog;
    selectArticle: IArticle;
    listArticles: IArticles;
    updateArticle: IUpdateArticle,
    sendMessage: ISendMessage,
    addSubscriber: IAddSubscriber,
    updateSubscriber: IUpdateSubscriber,
    removeSubscriber: IRemoveSubscriber,
    updateUserData: IUpdateUserData,
    signinUser: ISigninUser,
    getStaticContent: IGetStaticContent,
    getArticleFeatContent: IGetArticleFeatContent,
    getContactFormContent: IGetContactFormContent,
    getCookiesPromptContent: IGetCookiesPromptContent,
    getFeaturedContent: IGetFeaturedContent,
    getFeaturesContent: IGetFeaturesContent,
    getFooterContent: IGetFooterContent,
    getHeaderContent: IGetHeaderContent,
    getNavigationContent: IGetNavigationContent,
    getNewsletterContent: IGetNewsletterContent,
    getWrongPagePromptContent: IGetWrongPagePromptContent,
    getResetPasswordContent: IGetResetPasswordContent,
    getUserSigninContent: IGetUserSigninContent,
    getUserSignupContent: IGetUserSignupContent,
    getTestimonialsContent: IGetTestimonialsContent,
    getUnsubscribeContent: IGetUnsubscribeContent,
    getUpdateSubscriberContent: IGetUpdateSubscriberContent
}

export interface AppThunkAction<TAction> 
{
    (dispatch: (action: TAction) => void, getState: () => IApplicationState): void;
}
