import { IApplicationState } from "./applicationState";
import { SelectArticleDefault } from "./Defaults/Articles/selectArticleDefault";
import { ListArticlesDefault } from "./Defaults/Articles/listArticlesDefault";
import { UpdateArticleDefault } from "./Defaults/Articles/updateArticleDefault";
import { AddSubscriberDefault } from "./Defaults/Subscribers/addSubscriberDefault";
import { UpdateSubscriberDefault } from "./Defaults/Subscribers/updateSubscriberDefault";
import { RemoveSubscriberDefault } from "./Defaults/Subscribers/removeSubscriberDefault";
import { UpdateUserDataDefault } from "./Defaults/Users/updateUserDataDefault";
import { SigninUserDefault } from "./Defaults/Users/signinUserDefault";
import { SignupUserDefault } from "./Defaults/Users/signupUserDefault";
import { ResetUserPasswordDefault } from "./Defaults/Users/resetUserPasswordDefault";
import { SendMessageStateDefault } from "./Defaults/Mailer/sendMessageDefault";
import { RaiseErrorDefault } from "./Defaults/raiseErrorDefault";
import { RaiseDialogDefault } from "./Defaults/raiseDialogDefault";
import { GetStaticContentDefault } from "./Defaults/Content/getStaticContentDefault";
import { GetArticleFeatContentDefault } from "./Defaults/Content/getArticleFeatContentDefault";
import { GetContactFormContentDefault } from "./Defaults/Content/getContactFormContentDefault";
import { GetCookiesPromptContentDefault } from "./Defaults/Content/getCookiesPromptContentDefault";
import { GetFeaturedContentDefault } from "./Defaults/Content/getFeaturedContentDefault";
import { GetFeaturesContentDefault } from "./Defaults/Content/getFeaturesContentDefault";
import { GetFooterContentDefault } from "./Defaults/Content/getFooterContentDefault";
import { GetHeaderContentDefault } from "./Defaults/Content/getHeaderContentDefault";
import { GetNavigationContentDefault } from "./Defaults/Content/getNavigationContentDefault";
import { GetNewsletterContentDefault } from "./Defaults/Content/getNewsletterContentDefault";
import { GetWrongPagePromptContentDefault } from "./Defaults/Content/getWrongPagePromptContentDefault";
import { GetResetPasswordContentDefault } from "./Defaults/Content/getResetPasswordContentDefault";
import { GetUserSigninContentDefault } from "./Defaults/Content/getUserSigninContentDefault";
import { GetUserSignoutContentDefault } from "./Defaults/Content/getUserSignoutContentDefault";
import { GetUserSignupContentDefault } from "./Defaults/Content/getUserSignupContentDefault";
import { GetTestimonialsContentDefault } from "./Defaults/Content/getTestimonialsContentDefault";
import { GetUnsubscribeContentDefault } from "./Defaults/Content/getUnsubscribeContentDefault";
import { GetUpdateSubscriberContentDefault } from "./Defaults/Content/getUpdateSubscriberContentDefault";

export const combinedDefaults: IApplicationState = 
{
    raiseError: RaiseErrorDefault,
    raiseDialog: RaiseDialogDefault,
    selectArticle: SelectArticleDefault,
    listArticles: ListArticlesDefault,
    updateArticle: UpdateArticleDefault,
    sendMessage: SendMessageStateDefault,
    addSubscriber: AddSubscriberDefault,
    updateSubscriber: UpdateSubscriberDefault,
    removeSubscriber: RemoveSubscriberDefault,
    updateUserData: UpdateUserDataDefault,
    signinUser: SigninUserDefault,
    signupUser: SignupUserDefault,
    resetUserPassword: ResetUserPasswordDefault,
    getStaticContent: GetStaticContentDefault,
    getArticleFeatContent: GetArticleFeatContentDefault,
    getContactFormContent: GetContactFormContentDefault,
    getCookiesPromptContent: GetCookiesPromptContentDefault,
    getFeaturedContent: GetFeaturedContentDefault,
    getFeaturesContent: GetFeaturesContentDefault,
    getFooterContent: GetFooterContentDefault,
    getHeaderContent: GetHeaderContentDefault,
    getNavigationContent: GetNavigationContentDefault,
    getNewsletterContent: GetNewsletterContentDefault,
    getWrongPagePromptContent: GetWrongPagePromptContentDefault,
    getResetPasswordContent: GetResetPasswordContentDefault,
    getUserSigninContent: GetUserSigninContentDefault,
    getUserSignoutContent: GetUserSignoutContentDefault,
    getUserSignupContent: GetUserSignupContentDefault,
    getTestimonialsContent: GetTestimonialsContentDefault,
    getUnsubscribeContent: GetUnsubscribeContentDefault,
    getUpdateSubscriberContent: GetUpdateSubscriberContentDefault
};
