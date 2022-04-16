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
import { ActivateAccountDefault } from "./Defaults/Users/activateAccountDefault";
import { ResetUserPasswordDefault } from "./Defaults/Users/resetUserPasswordDefault";
import { UpdateUserPasswordDefault } from "./Defaults/Users/updateUserPasswordDefault";
import { SendMessageStateDefault } from "./Defaults/Mailer/sendMessageDefault";
import { RaiseErrorDefault } from "./Defaults/raiseErrorDefault";
import { RaiseDialogDefault } from "./Defaults/raiseDialogDefault";
import { GetStaticContentDefault } from "./Defaults/Content/getStaticContentDefault";
import { GetArticleFeatContentDefault } from "./Defaults/Content/getArticleFeatContentDefault";
import { GetContactFormContentDefault } from "./Defaults/Content/getContactFormContentDefault";
import { GetCookiesPromptContentDefault } from "./Defaults/Content/getCookiesPromptContentDefault";
import { GetClientsContentDefault } from "./Defaults/Content/getClientsContentDefault";
import { GetFeaturedContentDefault } from "./Defaults/Content/getFeaturedContentDefault";
import { GetFeaturesContentDefault } from "./Defaults/Content/getFeaturesContentDefault";
import { GetFooterContentDefault } from "./Defaults/Content/getFooterContentDefault";
import { GetHeaderContentDefault } from "./Defaults/Content/getHeaderContentDefault";
import { GetNavigationContentDefault } from "./Defaults/Content/getNavigationContentDefault";
import { GetNewsletterContentDefault } from "./Defaults/Content/getNewsletterContentDefault";
import { GetWrongPagePromptContentDefault } from "./Defaults/Content/getWrongPagePromptContentDefault";
import { GetResetPasswordContentDefault } from "./Defaults/Content/getResetPasswordContentDefault";
import { GetUpdatePasswordContentDefault } from "./Defaults/Content/getUpdatePasswordContentDefault";
import { GetUserSigninContentDefault } from "./Defaults/Content/getUserSigninContentDefault";
import { GetUserSignoutContentDefault } from "./Defaults/Content/getUserSignoutContentDefault";
import { GetUserSignupContentDefault } from "./Defaults/Content/getUserSignupContentDefault";
import { GetTestimonialsContentDefault } from "./Defaults/Content/getTestimonialsContentDefault";
import { GetUnsubscribeContentDefault } from "./Defaults/Content/getUnsubscribeContentDefault";
import { GetActivateAccountContentDefault } from "./Defaults/Content/getActivateAccountContentDefault";
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
    updateUserPassword: UpdateUserPasswordDefault,
    activateAccount: ActivateAccountDefault,
    getStaticContent: GetStaticContentDefault,
    getArticleFeatContent: GetArticleFeatContentDefault,
    getContactFormContent: GetContactFormContentDefault,
    getCookiesPromptContent: GetCookiesPromptContentDefault,
    getClientsContent: GetClientsContentDefault,
    getFeaturedContent: GetFeaturedContentDefault,
    getFeaturesContent: GetFeaturesContentDefault,
    getFooterContent: GetFooterContentDefault,
    getHeaderContent: GetHeaderContentDefault,
    getNavigationContent: GetNavigationContentDefault,
    getNewsletterContent: GetNewsletterContentDefault,
    getWrongPagePromptContent: GetWrongPagePromptContentDefault,
    getResetPasswordContent: GetResetPasswordContentDefault,
    getUpdatePasswordContent: GetUpdatePasswordContentDefault,
    getUserSigninContent: GetUserSigninContentDefault,
    getUserSignoutContent: GetUserSignoutContentDefault,
    getUserSignupContent: GetUserSignupContentDefault,
    getTestimonialsContent: GetTestimonialsContentDefault,
    getUnsubscribeContent: GetUnsubscribeContentDefault,
    getActivateAccountContent: GetActivateAccountContentDefault,
    getUpdateSubscriberContent: GetUpdateSubscriberContentDefault     
};
