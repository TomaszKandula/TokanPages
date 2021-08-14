import { IApplicationState } from "./applicationState";
import { SelectArticleDefault } from "./Defaults/selectArticleDefault";
import { ListArticlesDefault } from "./Defaults/listArticlesDefault";
import { UpdateArticleDefault } from "./Defaults/updateArticleDefault";
import { SendMessageStateDefault } from "./Defaults/sendMessageDefault";
import { AddSubscriberDefault } from "./Defaults/addSubscriberDefault";
import { UpdateSubscriberDefault } from "./Defaults/updateSubscriberDefault";
import { RemoveSubscriberDefault } from "./Defaults/removeSubscriberDefault";
import { RaiseErrorDefault } from "./Defaults/raiseErrorDefault";
import { RaiseDialogDefault } from "./Defaults/raiseDialogDefault";
import { UpdateUserDataDefault } from "./Defaults/updateUserDataDefault";
import { SigninUserDefault } from "./Defaults/signinUserDefault";
import { GetStaticContentDefault } from "./Defaults/getStaticContentDefault";
import { GetArticleFeatContentDefault } from "./Defaults/getArticleFeatContentDefault";
import { GetContactFormContentDefault } from "./Defaults/getContactFormContentDefault";
import { GetCookiesPromptContentDefault } from "./Defaults/getCookiesPromptContentDefault";
import { GetFeaturedContentDefault } from "./Defaults/getFeaturedContentDefault";
import { GetFeaturesContentDefault } from "./Defaults/getFeaturesContentDefault";
import { GetFooterContentDefault } from "./Defaults/getFooterContentDefault";
import { GetHeaderContentDefault } from "./Defaults/getHeaderContentDefault";
import { GetNavigationContentDefault } from "./Defaults/getNavigationContentDefault";
import { GetNewsletterContentDefault } from "./Defaults/getNewsletterContentDefault";
import { GetWrongPagePromptContentDefault } from "./Defaults/getWrongPagePromptContentDefault";
import { GetResetFormContentDefault } from "./Defaults/getResetFormContentDefault";
import { GetSigninFormContentDefault } from "./Defaults/getSigninFormContentDefault";
import { GetSignupFormContentDefault } from "./Defaults/getSignupFormContentDefault";
import { GetTestimonialsContentDefault } from "./Defaults/getTestimonialsContentDefault";
import { GetUnsubscribeContentDefault } from "./Defaults/getUnsubscribeContentDefault";
import { GetUpdateSubscriberContentDefault } from "./Defaults/getUpdateSubscriberContentDefault";

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
    getResetFormContent: GetResetFormContentDefault,
    getSigninFormContent: GetSigninFormContentDefault,
    getSignupFormContent: GetSignupFormContentDefault,
    getTestimonialsContent: GetTestimonialsContentDefault,
    getUnsubscribeContent: GetUnsubscribeContentDefault,
    getUpdateSubscriberContent: GetUpdateSubscriberContentDefault
};
