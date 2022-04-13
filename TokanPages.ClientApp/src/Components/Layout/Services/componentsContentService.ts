import { Dispatch } from "redux";

import { ActionCreators as NavigationContent } from "../../../Redux/Actions/Content/getNavigationContentAction";
import { ActionCreators as HeaderContent } from "../../../Redux/Actions/Content/getHeaderContentAction";
import { ActionCreators as ActivateAccountContent } from "../../../Redux/Actions/Content/getActivateAccountContentAction";
import { ActionCreators as ArticleFeatContent } from "../../../Redux/Actions/Content/getArticleFeatContentAction";
import { ActionCreators as FooterContent } from "../../../Redux/Actions/Content/getFooterContentAction";
import { ActionCreators as FeaturedContent } from "../../../Redux/Actions/Content/getFeaturedContentAction";
import { ActionCreators as FeaturesContent } from "../../../Redux/Actions/Content/getFeaturesContentAction";
import { ActionCreators as NewsletterContent } from "../../../Redux/Actions/Content/getNewsletterContentAction";
import { ActionCreators as ContactFormContent } from "../../../Redux/Actions/Content/getContactFormContentAction";
import { ActionCreators as CookiesContent } from "../../../Redux/Actions/Content/getCookiesPromptContentAction";
import { ActionCreators as ResetFormContent } from "../../../Redux/Actions/Content/getResetPasswordContentAction";
import { ActionCreators as SigninFormContent } from "../../../Redux/Actions/Content/getUserSigninContentAction";
import { ActionCreators as SignoutContent } from "../../../Redux/Actions/Content/getUserSignoutContentAction";
import { ActionCreators as SignupFormContent } from "../../../Redux/Actions/Content/getUserSignupContentAction";
import { ActionCreators as TestimonialsContent } from "../../../Redux/Actions/Content/getTestimonialsContentAction";
import { ActionCreators as UnsubscribeContent } from "../../../Redux/Actions/Content/getUnsubscribeContentAction";
import { ActionCreators as UpdateSubscriberContent } from "../../../Redux/Actions/Content/getUpdateSubscriberContentAction";
import { ActionCreators as UpdatePasswordContent } from "../../../Redux/Actions/Content/getUpdatePasswordContentAction";
import { ActionCreators as WrongPagePromptContent } from "../../../Redux/Actions/Content/getWrongPagePromptContentAction";

export const Reload = (dispatch: Dispatch<any>) => 
{
    dispatch(NavigationContent.getNavigationContent(true));
    dispatch(HeaderContent.getHeaderContent(true));
    dispatch(FooterContent.getFooterContent(true));
    dispatch(ActivateAccountContent.getActivateAccountContent(true));
    dispatch(ArticleFeatContent.getArticleFeaturesContent(true));
    dispatch(FeaturedContent.getFeaturedContent(true));
    dispatch(FeaturesContent.getFeaturesContent(true));
    dispatch(NewsletterContent.getNewsletterContent(true));
    dispatch(ContactFormContent.getContactFormContent(true));
    dispatch(CookiesContent.getCookiesPromptContent(true));
    dispatch(ResetFormContent.getResetPasswordContent(true));
    dispatch(SigninFormContent.getUserSigninContent(true));
    dispatch(SignoutContent.getUserSignoutContent(true));
    dispatch(SignupFormContent.getUserSignupContent(true));
    dispatch(TestimonialsContent.getTestimonialsContent(true));
    dispatch(UnsubscribeContent.getUnsubscribeContent(true));
    dispatch(UpdateSubscriberContent.getUpdateSubscriberContent(true));
    dispatch(UpdatePasswordContent.getUpdatePasswordContent(true));
    dispatch(WrongPagePromptContent.getWrongPagePromptContent(true));
}

export default Reload;
