import SelectArticlesReducer from "./Reducers/selectArticleReducer";
import ListArticlesReducer from "./Reducers/listArticlesReducer";
import SendMessageReducer from "./Reducers/sendMessageReducer";
import AddSubscriberReducer from "./Reducers/addSubscriberReducer";
import UpdateSubscriberReducer from "./Reducers/updateSubscriberReducer";
import RemoveSubscriberReducer from "./Reducers/removeSubscriberReducer";
import UpdateArticleReducer from "./Reducers/updateArticleReducer";
import RaiseErrorReducer from "./Reducers/raiseErrorReducer";
import RaiseDialogReducer from "./Reducers/raiseDialogReducer";
import GetStaticContentReducer from "./Reducers/getStaticContentReducer";
import GetArticleFeatContentReducer from "./Reducers/getArticleFeatContentReducer";
import GetContactFormContentReducer from "./Reducers/getContactFormContentReducer";
import GetCookiesPromptContentReducer from "./Reducers/getCookiesPromptContentReducer";
import GetFeaturedContentReducer from "./Reducers/getFeaturedContentReducer";
import GetFeaturesContentReducer from "./Reducers/getFeaturesContentReducer";
import GetFooterContentReducer from "./Reducers/getFooterContentReducer";
import GetHeaderContentReducer from "./Reducers/getHeaderContentReducer";
import GetNavigationContentReducer from "./Reducers/getNavigationContentReducer";
import GetNewsletterContentReducer from "./Reducers/getNewsletterContentReducer";
import GetWrongPagePromptContentReducer from "./Reducers/getWrongPagePromptContentReducer";
import GetResetFormContentReducer from "./Reducers/getResetFormContentReducer";
import GetSigninFormContentReducer from "./Reducers/getSigninFormContentReducer";
import GetSignupFormContentReducer from "./Reducers/getSignupFormContentReducer";
import GetTestimonialsContentReducer from "./Reducers/getTestimonialsContentReducer";
import GetUnsubscribeContentReducer from "./Reducers/getUnsubscribeContentReducer";
import GetUpdateSubscriberContentReducer from "./Reducers/getUpdateSubscriberContentReducer";

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
    getResetFormContent: GetResetFormContentReducer,
    getSigninFormContent: GetSigninFormContentReducer,
    getSignupFormContent: GetSignupFormContentReducer,
    getTestimonialsContent: GetTestimonialsContentReducer,
    getUnsubscribeContent: GetUnsubscribeContentReducer,
    getUpdateSubscriberContent: GetUpdateSubscriberContentReducer
};
