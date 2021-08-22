import { IArticle } from "./States/Articles/selectArticleState";
import { IArticles } from "./States/Articles/listArticlesState";
import { IUpdateArticle } from "./States/Articles/updateArticleState";
import { IAddSubscriber } from "./States/Subscribers/addSubscriberState";
import { IUpdateSubscriber } from "./States/Subscribers/updateSubscriberState";
import { IRemoveSubscriber } from "./States/Subscribers/removeSubscriberState";
import { IUpdateUserData } from "./States/Users/updateUserDataState";
import { ISigninUser } from "./States/Users/signinUserState";
import { ISendMessage } from "./States/Mailer/sendMessageState";
import { IRaiseError } from "./States/raiseErrorState";
import { IRaiseDialog } from "./States/raiseDialogState";
import { IGetStaticContent } from "./States/Content/getStaticContentState";
import { IGetArticleFeatContent } from "./States/Content/getArticleFeatContentState";
import { IGetContactFormContent } from "./States/Content/getContactFormContentState";
import { IGetCookiesPromptContent } from "./States/Content/getCookiesPromptContentState";
import { IGetFeaturedContent } from "./States/Content/getFeaturedContentState";
import { IGetFeaturesContent } from "./States/Content/getFeaturesContentState";
import { IGetFooterContent } from "./States/Content/getFooterContentState";
import { IGetHeaderContent } from "./States/Content/getHeaderContentState";
import { IGetNavigationContent } from "./States/Content/getNavigationContentState";
import { IGetNewsletterContent } from "./States/Content/getNewsletterContentState";
import { IGetWrongPagePromptContent } from "./States/Content/getWrongPagePromptContentState";
import { IGetResetPasswordContent } from "./States/Content/getResetPasswordContentState";
import { IGetUserSigninContent } from "./States/Content/getUserSigninContentState";
import { IGetUserSignoutContent } from "./States/Content/getUserSignoutContentState";
import { IGetUserSignupContent } from "./States/Content/getUserSignupContentState";
import { IGetTestimonialsContent } from "./States/Content/getTestimonialsContentState";
import { IGetUnsubscribeContent } from "./States/Content/getUnsubscribeContentState";
import { IGetUpdateSubscriberContent } from "./States/Content/getUpdateSubscriberContentState";

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
    getUserSignoutContent: IGetUserSignoutContent,
    getUserSignupContent: IGetUserSignupContent,
    getTestimonialsContent: IGetTestimonialsContent,
    getUnsubscribeContent: IGetUnsubscribeContent,
    getUpdateSubscriberContent: IGetUpdateSubscriberContent
}

export interface AppThunkAction<TAction> 
{
    (dispatch: (action: TAction) => void, getState: () => IApplicationState): void;
}
