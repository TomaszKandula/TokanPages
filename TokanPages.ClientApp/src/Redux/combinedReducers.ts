import { ActivateAccountReducer } from "./Reducers/Users/activateAccountReducer";
import { AddSubscriberReducer } from "./Reducers/Subscribers/addSubscriberReducer";
import { GetAccountContentReducer } from "./Reducers/Content/getAccountContenReducer";
import { GetActivateAccountContentReducer } from "./Reducers/Content/getActivateAccountContentReducer";
import { GetArticleFeaturesContentReducer } from "./Reducers/Content/getArticleFeaturesContentReducer";
import { GetClientsContentReducer } from "./Reducers/Content/getClientsContentReducer";
import { GetContactFormContentReducer } from "./Reducers/Content/getContactFormContentReducer";
import { GetCookiesPromptContentReducer } from "./Reducers/Content/getCookiesPromptContentReducer";
import { GetFeaturedContentReducer } from "./Reducers/Content/getFeaturedContentReducer";
import { GetFeaturesContentReducer } from "./Reducers/Content/getFeaturesContentReducer";
import { GetFooterContentReducer } from "./Reducers/Content/getFooterContentReducer";
import { GetHeaderContentReducer } from "./Reducers/Content/getHeaderContentReducer";
import { GetNavigationContentReducer } from "./Reducers/Content/getNavigationContentReducer";
import { GetNewsletterContentReducer } from "./Reducers/Content/getNewsletterContentReducer";
import { GetResetPasswordContentReducer } from "./Reducers/Content/getResetPasswordContentReducer";

import { GetTestimonialsContentReducer } from "./Reducers/Content/getTestimonialsContentReducer";
import { GetUnsubscribeContentReducer } from "./Reducers/Content/getUnsubscribeContentReducer";
import { GetUpdatePasswordContentReducer } from "./Reducers/Content/getUpdatePasswordContentReducer";
import { GetUpdateSubscriberContentReducer } from "./Reducers/Content/getUpdateSubscriberContentReducer";
import { GetUserSigninContentReducer } from "./Reducers/Content/getUserSigninContentReducer";
import { GetUserSignoutContentReducer } from "./Reducers/Content/getUserSignoutContentReducer";
import { GetUserSignupContentReducer } from "./Reducers/Content/getUserSignupContentReducer";
import { GetWrongPagePromptContentReducer } from "./Reducers/Content/getWrongPagePromptContentReducer";
import { ListArticlesReducer } from "./Reducers/Articles/listArticlesReducer";
import { RaiseDialogReducer } from "./Reducers/raiseDialogReducer";
import { RaiseErrorReducer } from "./Reducers/raiseErrorReducer";
import { ReAuthenticateUserReducer } from "./Reducers/Users/reAuthenticateUserReducer";
import { RemoveAccountReducer } from "./Reducers/Users/removeAccountReducer";
import { RemoveSubscriberReducer } from "./Reducers/Subscribers/removeSubscriberReducer";
import { ResetUserPasswordReducer } from "./Reducers/Users/resetUserPasswordReducer";
import { SelectArticleReducer } from "./Reducers/Articles/selectArticleReducer";
import { SendMessageReducer } from "./Reducers/Mailer/sendMessageReducer";
import { SigninUserReducer } from "./Reducers/Users/signinUserReducer";
import { SignupUserReducer } from "./Reducers/Users/signupUserReducer";
import { StoreUserDataReducer } from "./Reducers/Users/storeUserDataReducer";
import { UpdateArticleReducer } from "./Reducers/Articles/updateArticleReducer";
import { UpdateSubscriberReducer } from "./Reducers/Subscribers/updateSubscriberReducer";
import { UpdateUserPasswordReducer } from "./Reducers/Users/updateUserPasswordReducer";
import { UpdateUserReducer } from "./Reducers/Users/updateUserReducer";
import { UserLanguageReducer } from "./Reducers/userLanguageReducer";

export const combinedReducers = 
{
    raiseError: RaiseErrorReducer,
    raiseDialog: RaiseDialogReducer,

    userLanguage: UserLanguageReducer,

    listArticles: ListArticlesReducer,
    selectArticle: SelectArticleReducer,
    storeUserData: StoreUserDataReducer,

    sendMessage: SendMessageReducer,
    updateArticle: UpdateArticleReducer,
    addSubscriber: AddSubscriberReducer,
    updateSubscriber: UpdateSubscriberReducer,
    removeSubscriber: RemoveSubscriberReducer,
    removeAccount: RemoveAccountReducer,
    activateAccount: ActivateAccountReducer,
    reAuthenticateUser: ReAuthenticateUserReducer,
    signinUser: SigninUserReducer,
    signupUser: SignupUserReducer,
    updateUser: UpdateUserReducer,
    updateUserPassword: UpdateUserPasswordReducer,
    resetUserPassword: ResetUserPasswordReducer,

    getNavigationContent: GetNavigationContentReducer,
    getHeaderContent: GetHeaderContentReducer,
    getFooterContent: GetFooterContentReducer,

    getAccountContent: GetAccountContentReducer, 
    getActivateAccountContent: GetActivateAccountContentReducer,

    getClientsContent: GetClientsContentReducer,
    getFeaturesContent: GetFeaturesContentReducer,
    getArticleFeaturesContent: GetArticleFeaturesContentReducer,
    getFeaturedContent: GetFeaturedContentReducer,
    getTestimonialsContent: GetTestimonialsContentReducer,
    getNewsletterContent: GetNewsletterContentReducer,
    getContactFormContent: GetContactFormContentReducer,
    getCookiesPromptContent: GetCookiesPromptContentReducer,
    getWrongPagePromptContent: GetWrongPagePromptContentReducer,

    getUserSigninContent: GetUserSigninContentReducer,
    getUserSignupContent: GetUserSignupContentReducer,
    getUserSignoutContent: GetUserSignoutContentReducer,
    getUpdatePasswordContent: GetUpdatePasswordContentReducer,
    getResetPasswordContent: GetResetPasswordContentReducer,

    getUnsubscribeContent: GetUnsubscribeContentReducer,
    getUpdateSubscriberContent: GetUpdateSubscriberContentReducer
};
