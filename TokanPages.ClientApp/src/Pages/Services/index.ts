import { Dispatch } from "redux";

import { ActionCreators as NavigationContent } from "../../Redux/Actions/Content/getNavigationContentAction";
import { ActionCreators as HeaderContent } from "../../Redux/Actions/Content/getHeaderContentAction";
import { ActionCreators as ActivateAccountContent } from "../../Redux/Actions/Content/getActivateAccountContentAction";
import { ActionCreators as ArticleFeaturesContent } from "../../Redux/Actions/Content/getArticleFeaturesContentAction";
import { ActionCreators as FooterContent } from "../../Redux/Actions/Content/getFooterContentAction";
import { ActionCreators as ClientsContent } from "../../Redux/Actions/Content/getClientsContentAction";
import { ActionCreators as FeaturedContent } from "../../Redux/Actions/Content/getFeaturedContentAction";
import { ActionCreators as FeaturesContent } from "../../Redux/Actions/Content/getFeaturesContentAction";
import { ActionCreators as NewsletterContent } from "../../Redux/Actions/Content/getNewsletterContentAction";
import { ActionCreators as ContactFormContent } from "../../Redux/Actions/Content/getContactFormContentAction";
import { ActionCreators as CookiesContent } from "../../Redux/Actions/Content/getCookiesPromptContentAction";
import { ActionCreators as ResetFormContent } from "../../Redux/Actions/Content/getResetPasswordContentAction";
import { ActionCreators as SigninFormContent } from "../../Redux/Actions/Content/getUserSigninContentAction";
import { ActionCreators as SignoutContent } from "../../Redux/Actions/Content/getUserSignoutContentAction";
import { ActionCreators as SignupFormContent } from "../../Redux/Actions/Content/getUserSignupContentAction";
import { ActionCreators as TestimonialsContent } from "../../Redux/Actions/Content/getTestimonialsContentAction";
import { ActionCreators as UnsubscribeContent } from "../../Redux/Actions/Content/getUnsubscribeContentAction";
import { ActionCreators as UpdateSubscriberContent } from "../../Redux/Actions/Content/getUpdateSubscriberContentAction";
import { ActionCreators as UpdatePasswordContent } from "../../Redux/Actions/Content/getUpdatePasswordContentAction";
import { ActionCreators as WrongPagePromptContent } from "../../Redux/Actions/Content/getWrongPagePromptContentAction";
import { ActionCreators as StaticContent } from "../../Redux/Actions/Content/getStaticContentAction";

export const GetMainPageContent = (dispatch: Dispatch<any>, isLanguageChanged: boolean = false) => 
{
    dispatch(NavigationContent.getNavigationContent(isLanguageChanged));
    dispatch(HeaderContent.getHeaderContent(isLanguageChanged));
    dispatch(FooterContent.getFooterContent(isLanguageChanged));
    dispatch(ClientsContent.getClientsContent(isLanguageChanged));
    dispatch(ActivateAccountContent.getActivateAccountContent(isLanguageChanged));
    dispatch(ArticleFeaturesContent.getArticleFeaturesContent(isLanguageChanged));
    dispatch(FeaturedContent.getFeaturedContent(isLanguageChanged));
    dispatch(FeaturesContent.getFeaturesContent(isLanguageChanged));
    dispatch(NewsletterContent.getNewsletterContent(isLanguageChanged));
    dispatch(ContactFormContent.getContactFormContent(isLanguageChanged));
    dispatch(CookiesContent.getCookiesPromptContent(isLanguageChanged));
}

export const GetAllPagesContent = (dispatch: Dispatch<any>, isLanguageChanged: boolean = false) => 
{
    GetMainPageContent(dispatch, isLanguageChanged);

    dispatch(ResetFormContent.getResetPasswordContent(isLanguageChanged));
    dispatch(SigninFormContent.getUserSigninContent(isLanguageChanged));
    dispatch(SignoutContent.getUserSignoutContent(isLanguageChanged));
    dispatch(SignupFormContent.getUserSignupContent(isLanguageChanged));
    dispatch(TestimonialsContent.getTestimonialsContent(isLanguageChanged));
    dispatch(UnsubscribeContent.getUnsubscribeContent(isLanguageChanged));
    dispatch(UpdateSubscriberContent.getUpdateSubscriberContent(isLanguageChanged));
    dispatch(UpdatePasswordContent.getUpdatePasswordContent(isLanguageChanged));
    dispatch(WrongPagePromptContent.getWrongPagePromptContent(isLanguageChanged));
    dispatch(StaticContent.getStoryContent(isLanguageChanged));
    dispatch(StaticContent.getTermsContent(isLanguageChanged));
    dispatch(StaticContent.getPolicyContent(isLanguageChanged));
}
