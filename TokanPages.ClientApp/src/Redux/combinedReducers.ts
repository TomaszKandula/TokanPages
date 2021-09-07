import SelectArticlesReducer from "./Reducers/Articles/selectArticleReducer";
import ListArticlesReducer from "./Reducers/Articles/listArticlesReducer";
import UpdateArticleReducer from "./Reducers/Articles/updateArticleReducer";
import AddSubscriberReducer from "./Reducers/Subscribers/addSubscriberReducer";
import UpdateSubscriberReducer from "./Reducers/Subscribers/updateSubscriberReducer";
import RemoveSubscriberReducer from "./Reducers/Subscribers/removeSubscriberReducer";
import UpdateUserDataReducer from "./Reducers/Users/updateUserDataReducer";
import SigninUserReducer from "./Reducers/Users/signinUserReducer";
import SignupUserReducer from "./Reducers/Users/signupUserReducer";
import ResetUserPasswordReducer from "./Reducers/Users/resetUserPasswordReducer";
import SendMessageReducer from "./Reducers/Mailer/sendMessageReducer";
import RaiseErrorReducer from "./Reducers/raiseErrorReducer";
import RaiseDialogReducer from "./Reducers/raiseDialogReducer";
import GetStaticContentReducer from "./Reducers/Content/getStaticContentReducer";
import GetArticleFeatContentReducer from "./Reducers/Content/getArticleFeatContentReducer";
import GetContactFormContentReducer from "./Reducers/Content/getContactFormContentReducer";
import GetCookiesPromptContentReducer from "./Reducers/Content/getCookiesPromptContentReducer";
import GetFeaturedContentReducer from "./Reducers/Content/getFeaturedContentReducer";
import GetFeaturesContentReducer from "./Reducers/Content/getFeaturesContentReducer";
import GetFooterContentReducer from "./Reducers/Content/getFooterContentReducer";
import GetHeaderContentReducer from "./Reducers/Content/getHeaderContentReducer";
import GetNavigationContentReducer from "./Reducers/Content/getNavigationContentReducer";
import GetNewsletterContentReducer from "./Reducers/Content/getNewsletterContentReducer";
import GetWrongPagePromptContentReducer from "./Reducers/Content/getWrongPagePromptContentReducer";
import GetResetPasswordContentReducer from "./Reducers/Content/getResetPasswordContentReducer";
import GetUserSigninContentReducer from "./Reducers/Content/getUserSigninContentReducer";
import GetUserSignoutContentReducer from "./Reducers/Content/getUserSignoutContentReducer";
import GetUserSignupContentReducer from "./Reducers/Content/getUserSignupContentReducer";
import GetTestimonialsContentReducer from "./Reducers/Content/getTestimonialsContentReducer";
import GetUnsubscribeContentReducer from "./Reducers/Content/getUnsubscribeContentReducer";
import GetUpdateSubscriberContentReducer from "./Reducers/Content/getUpdateSubscriberContentReducer";

export const combinedReducers = 
{
    raiseError: RaiseErrorReducer,
    raiseDialog: RaiseDialogReducer,
    selectArticle: SelectArticlesReducer,
    listArticles: ListArticlesReducer,
    updateArticle: UpdateArticleReducer,
    sendMessage: SendMessageReducer,
    addSubscriber: AddSubscriberReducer,
    updateSubscriber: UpdateSubscriberReducer,
    removeSubscriber: RemoveSubscriberReducer,
    updateUserData: UpdateUserDataReducer,
    signinUser: SigninUserReducer,
    signupUser: SignupUserReducer,
    resetUserPassword: ResetUserPasswordReducer,
    getStaticContent: GetStaticContentReducer,
    getArticleFeatContent: GetArticleFeatContentReducer,
    getContactFormContent: GetContactFormContentReducer,
    getCookiesPromptContent: GetCookiesPromptContentReducer,
    getFeaturedContent: GetFeaturedContentReducer,
    getFeaturesContent: GetFeaturesContentReducer,
    getFooterContent: GetFooterContentReducer,
    getHeaderContent: GetHeaderContentReducer,
    getNavigationContent: GetNavigationContentReducer,
    getNewsletterContent: GetNewsletterContentReducer,
    getWrongPagePromptContent: GetWrongPagePromptContentReducer,
    getResetPasswordContent: GetResetPasswordContentReducer,
    getUserSigninContent: GetUserSigninContentReducer,
    getUserSignoutContent: GetUserSignoutContentReducer,
    getUserSignupContent: GetUserSignupContentReducer,
    getTestimonialsContent: GetTestimonialsContentReducer,
    getUnsubscribeContent: GetUnsubscribeContentReducer,
    getUpdateSubscriberContent: GetUpdateSubscriberContentReducer
};
