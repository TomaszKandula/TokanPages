import { getDataFromUrl } from "../requests";
import { 
    INavigation,
    IHeader,
    IFooter,
    IArticleFeat, 
    IContactForm, 
    ICookiesPrompt,
    IFeatured,
    IFeatures,
    INewsletter,
    IResetForm,
    ISigninForm,
    ISignupForm,
    ITestimonials,
    IUnsubscribe,
    IUpdateSubscriber
} from "../../Api/Models";
import { 
    GET_NAVIGATION_CONTENT,
    GET_HEADER_CONTENT,
    GET_FOOTER_CONTENT,
    GET_ARTICLE_FEAT_CONTENT, 
    GET_CONTACT_FORM_CONTENT, 
    GET_COOKIES_PROMPT_CONTENT,
    GET_FEATURED_CONTENT,
    GET_FEATURES_CONTENT,
    GET_NEWSLETTER_CONTENT,
    GET_RESET_FORM_CONTENT,
    GET_SIGNIN_FORM_CONTENT,
    GET_SIGNUP_FORM_CONTENT,
    GET_TESTIMONIALS_CONTENT,
    GET_UNSUBSCRIBE_CONTENT,
    GET_UPDATE_SUBSCRIBER_CONTENT
} from "../../Shared/constants";

export const getNavigationContent = async (): Promise<INavigation> =>
{
    return await getDataFromUrl(GET_NAVIGATION_CONTENT);
};

export const getHeaderContent = async (): Promise<IHeader> =>
{
    return await getDataFromUrl(GET_HEADER_CONTENT);
};

export const getFooterContent = async (): Promise<IFooter> =>
{
    return await getDataFromUrl(GET_FOOTER_CONTENT);
};

export const getArticleFeatContent = async (): Promise<IArticleFeat> =>
{
    return await getDataFromUrl(GET_ARTICLE_FEAT_CONTENT); 
};

export const getContactFormContent = async (): Promise<IContactForm> =>
{
    return await getDataFromUrl(GET_CONTACT_FORM_CONTENT);
};

export const getCookiesPromptContent = async (): Promise<ICookiesPrompt> =>
{
    return await getDataFromUrl(GET_COOKIES_PROMPT_CONTENT);
};

export const getFeaturedContent = async (): Promise<IFeatured> =>
{
    return await getDataFromUrl(GET_FEATURED_CONTENT);
};

export const getFeaturesContent = async (): Promise<IFeatures> =>
{
    return await getDataFromUrl(GET_FEATURES_CONTENT);
};

export const getNewsletterContent = async (): Promise<INewsletter> =>
{
    return await getDataFromUrl(GET_NEWSLETTER_CONTENT);
};

export const getResetFormContent = async (): Promise<IResetForm> =>
{
    return await getDataFromUrl(GET_RESET_FORM_CONTENT);
};

export const getSigninFormContent = async (): Promise<ISigninForm> =>
{
    return await getDataFromUrl(GET_SIGNIN_FORM_CONTENT);
};

export const getSignupFormContent = async (): Promise<ISignupForm> =>
{
    return await getDataFromUrl(GET_SIGNUP_FORM_CONTENT);
};

export const getTestimonialsContent = async (): Promise<ITestimonials> =>
{
    return await getDataFromUrl(GET_TESTIMONIALS_CONTENT);
};

export const getUnsubscribeContent = async (): Promise<IUnsubscribe> =>
{
    return await getDataFromUrl(GET_UNSUBSCRIBE_CONTENT);
};

export const getUpdateSubscriberContent = async (): Promise<IUpdateSubscriber> =>
{
    return await getDataFromUrl(GET_UPDATE_SUBSCRIBER_CONTENT);
};
