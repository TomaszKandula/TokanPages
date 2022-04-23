import { ActivateAccountDefault } from "./Defaults/Users/activateAccountDefault";
import { AddSubscriberDefault } from "./Defaults/Subscribers/addSubscriberDefault";
import { GetAccountContentDefault } from "./Defaults/Content/getAccountContentDefault";
import { GetActivateAccountContentDefault } from "./Defaults/Content/getActivateAccountContentDefault";
import { GetArticleFeatContentDefault } from "./Defaults/Content/getArticleFeatContentDefault";
import { GetClientsContentDefault } from "./Defaults/Content/getClientsContentDefault";
import { GetContactFormContentDefault } from "./Defaults/Content/getContactFormContentDefault";
import { GetCookiesPromptContentDefault } from "./Defaults/Content/getCookiesPromptContentDefault";
import { GetFeaturedContentDefault } from "./Defaults/Content/getFeaturedContentDefault";
import { GetFeaturesContentDefault } from "./Defaults/Content/getFeaturesContentDefault";
import { GetFooterContentDefault } from "./Defaults/Content/getFooterContentDefault";
import { GetHeaderContentDefault } from "./Defaults/Content/getHeaderContentDefault";
import { GetNavigationContentDefault } from "./Defaults/Content/getNavigationContentDefault";
import { GetNewsletterContentDefault } from "./Defaults/Content/getNewsletterContentDefault";
import { GetResetPasswordContentDefault } from "./Defaults/Content/getResetPasswordContentDefault";
import { GetStaticContentDefault } from "./Defaults/Content/getStaticContentDefault";
import { GetTestimonialsContentDefault } from "./Defaults/Content/getTestimonialsContentDefault";
import { GetUnsubscribeContentDefault } from "./Defaults/Content/getUnsubscribeContentDefault";
import { GetUpdatePasswordContentDefault } from "./Defaults/Content/getUpdatePasswordContentDefault";
import { GetUpdateSubscriberContentDefault } from "./Defaults/Content/getUpdateSubscriberContentDefault";
import { GetUserSigninContentDefault } from "./Defaults/Content/getUserSigninContentDefault";
import { GetUserSignoutContentDefault } from "./Defaults/Content/getUserSignoutContentDefault";
import { GetUserSignupContentDefault } from "./Defaults/Content/getUserSignupContentDefault";
import { GetWrongPagePromptContentDefault } from "./Defaults/Content/getWrongPagePromptContentDefault";
import { IApplicationState } from "./applicationState";
import { ListArticlesDefault } from "./Defaults/Articles/listArticlesDefault";
import { RaiseDialogDefault } from "./Defaults/raiseDialogDefault";
import { RaiseErrorDefault } from "./Defaults/raiseErrorDefault";
import { RemoveSubscriberDefault } from "./Defaults/Subscribers/removeSubscriberDefault";
import { ResetUserPasswordDefault } from "./Defaults/Users/resetUserPasswordDefault";
import { SelectArticleDefault } from "./Defaults/Articles/selectArticleDefault";
import { SendMessageDefault } from "./Defaults/Mailer/sendMessageDefault";
import { SigninUserDefault } from "./Defaults/Users/signinUserDefault";
import { SignupUserDefault } from "./Defaults/Users/signupUserDefault";
import { StoreUserDataDefault } from "./Defaults/Users/storeUserDataDefault";
import { UpdateArticleDefault } from "./Defaults/Articles/updateArticleDefault";
import { UpdateSubscriberDefault } from "./Defaults/Subscribers/updateSubscriberDefault";
import { UpdateUserDefault } from "./Defaults/Users/updateUserDefault";
import { UpdateUserPasswordDefault } from "./Defaults/Users/updateUserPasswordDefault";

export const combinedDefaults: IApplicationState = 
{
    raiseError: RaiseErrorDefault,
    raiseDialog: RaiseDialogDefault,

    listArticles: ListArticlesDefault,
    selectArticle: SelectArticleDefault,
    storeUserData: StoreUserDataDefault,

    sendMessage: SendMessageDefault,
    updateArticle: UpdateArticleDefault,
    addSubscriber: AddSubscriberDefault,
    updateSubscriber: UpdateSubscriberDefault,
    removeSubscriber: RemoveSubscriberDefault,
    activateAccount: ActivateAccountDefault,
    signinUser: SigninUserDefault,
    signupUser: SignupUserDefault,
    updateUser: UpdateUserDefault,
    updateUserPassword: UpdateUserPasswordDefault,
    resetUserPassword: ResetUserPasswordDefault,

    getNavigationContent: GetNavigationContentDefault,
    getHeaderContent: GetHeaderContentDefault,
    getFooterContent: GetFooterContentDefault,
    getStaticContent: GetStaticContentDefault,
    getAccountContent: GetAccountContentDefault,
    getActivateAccountContent: GetActivateAccountContentDefault,

    getClientsContent: GetClientsContentDefault,
    getFeaturesContent: GetFeaturesContentDefault,
    getArticleFeatContent: GetArticleFeatContentDefault,
    getFeaturedContent: GetFeaturedContentDefault,
    getTestimonialsContent: GetTestimonialsContentDefault,
    getNewsletterContent: GetNewsletterContentDefault,
    getContactFormContent: GetContactFormContentDefault,
    getCookiesPromptContent: GetCookiesPromptContentDefault,
    getWrongPagePromptContent: GetWrongPagePromptContentDefault,

    getUserSigninContent: GetUserSigninContentDefault,
    getUserSignupContent: GetUserSignupContentDefault,
    getUserSignoutContent: GetUserSignoutContentDefault,
    getUpdatePasswordContent: GetUpdatePasswordContentDefault,
    getResetPasswordContent: GetResetPasswordContentDefault,

    getUnsubscribeContent: GetUnsubscribeContentDefault,
    getUpdateSubscriberContent: GetUpdateSubscriberContentDefault
};
