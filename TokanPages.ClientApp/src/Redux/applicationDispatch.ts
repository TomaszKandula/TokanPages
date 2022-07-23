import { Dispatch } from "redux";

import { ActionCreators as NavigationContent } from "./Actions/Content/getNavigationContentAction";
import { ActionCreators as HeaderContent } from "./Actions/Content/getHeaderContentAction";
import { ActionCreators as ActivateAccountContent } from "./Actions/Content/getActivateAccountContentAction";
import { ActionCreators as ArticleFeaturesContent } from "./Actions/Content/getArticleFeaturesContentAction";
import { ActionCreators as FooterContent } from "./Actions/Content/getFooterContentAction";
import { ActionCreators as ClientsContent } from "./Actions/Content/getClientsContentAction";
import { ActionCreators as FeaturedContent } from "./Actions/Content/getFeaturedContentAction";
import { ActionCreators as FeaturesContent } from "./Actions/Content/getFeaturesContentAction";
import { ActionCreators as NewsletterContent } from "./Actions/Content/getNewsletterContentAction";
import { ActionCreators as ContactFormContent } from "./Actions/Content/getContactFormContentAction";
import { ActionCreators as CookiesContent } from "./Actions/Content/getCookiesPromptContentAction";
import { ActionCreators as ResetFormContent } from "./Actions/Content/getResetPasswordContentAction";
import { ActionCreators as SigninFormContent } from "./Actions/Content/getUserSigninContentAction";
import { ActionCreators as SignoutContent } from "./Actions/Content/getUserSignoutContentAction";
import { ActionCreators as SignupFormContent } from "./Actions/Content/getUserSignupContentAction";
import { ActionCreators as TestimonialsContent } from "./Actions/Content/getTestimonialsContentAction";
import { ActionCreators as UnsubscribeContent } from "./Actions/Content/getUnsubscribeContentAction";
import { ActionCreators as UpdateSubscriberContent } from "./Actions/Content/getUpdateSubscriberContentAction";
import { ActionCreators as UpdatePasswordContent } from "./Actions/Content/getUpdatePasswordContentAction";
import { ActionCreators as WrongPagePromptContent } from "./Actions/Content/getWrongPagePromptContentAction";
import { ActionCreators as StaticContent } from "./Actions/Content/getStaticContentAction";

export const GetMainPageContent = (dispatch: Dispatch<any>, isLanguageChanged: boolean = false) => 
{
    dispatch(NavigationContent.getNavigationContent(isLanguageChanged));
    dispatch(HeaderContent.getHeaderContent(isLanguageChanged));
    dispatch(ClientsContent.getClientsContent(isLanguageChanged));
    dispatch(ActivateAccountContent.getActivateAccountContent(isLanguageChanged));
    dispatch(FeaturesContent.getFeaturesContent(isLanguageChanged));
    dispatch(ArticleFeaturesContent.getArticleFeaturesContent(isLanguageChanged));
    dispatch(FeaturedContent.getFeaturedContent(isLanguageChanged));
    dispatch(TestimonialsContent.getTestimonialsContent(isLanguageChanged));
    dispatch(NewsletterContent.getNewsletterContent(isLanguageChanged));
    dispatch(ContactFormContent.getContactFormContent(isLanguageChanged));
    dispatch(FooterContent.getFooterContent(isLanguageChanged));
    dispatch(CookiesContent.getCookiesPromptContent(isLanguageChanged));
}

export const GetAllPagesContent = (dispatch: Dispatch<any>, isLanguageChanged: boolean = false) => 
{
    GetMainPageContent(dispatch, isLanguageChanged);

    dispatch(ResetFormContent.getResetPasswordContent(isLanguageChanged));
    dispatch(SigninFormContent.getUserSigninContent(isLanguageChanged));
    dispatch(SignoutContent.getUserSignoutContent(isLanguageChanged));
    dispatch(SignupFormContent.getUserSignupContent(isLanguageChanged));
    dispatch(UnsubscribeContent.getUnsubscribeContent(isLanguageChanged));
    dispatch(UpdateSubscriberContent.getUpdateSubscriberContent(isLanguageChanged));
    dispatch(UpdatePasswordContent.getUpdatePasswordContent(isLanguageChanged));
    dispatch(WrongPagePromptContent.getWrongPagePromptContent(isLanguageChanged));
    dispatch(StaticContent.getStoryContent(isLanguageChanged));
    dispatch(StaticContent.getTermsContent(isLanguageChanged));
    dispatch(StaticContent.getPolicyContent(isLanguageChanged));
}