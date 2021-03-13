import { getDataFromUrl } from "../requests";
import { 
    INavigation,
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
    GET_NAVIGATION_TEXT,
    GET_ARTICLE_FEAT_TEXT, 
    GET_CONTACT_FORM_TEXT, 
    GET_COOKIES_PROMPT_TEXT,
    GET_FEATURED_TEXT,
    GET_FEATURES_TEXT,
    GET_NEWSLETTER_TEXT,
    GET_RESET_FORM_TEXT,
    GET_SIGNIN_FORM_TEXT,
    GET_SIGNUP_FORM_TEXT,
    GET_TESTIMONIALS_TEXT,
    GET_UNSUBSCRIBE_TEXT,
    GET_UPDATE_SUBSCRIBER_TEXT
} from "../../Shared/constants";

export const getNavigationText = async (): Promise<INavigation> =>
{
    return await getDataFromUrl(GET_NAVIGATION_TEXT);
};

export const getArticleFeatText = async (): Promise<IArticleFeat> =>
{
    return await getDataFromUrl(GET_ARTICLE_FEAT_TEXT); 
};

export const getContactFormText = async (): Promise<IContactForm> =>
{
    return await getDataFromUrl(GET_CONTACT_FORM_TEXT);
};

export const getCookiesPromptText = async (): Promise<ICookiesPrompt> =>
{
    return await getDataFromUrl(GET_COOKIES_PROMPT_TEXT);
};

export const getFeaturedText = async (): Promise<IFeatured> =>
{
    return await getDataFromUrl(GET_FEATURED_TEXT);
};

export const getFeaturesText = async (): Promise<IFeatures> =>
{
    return await getDataFromUrl(GET_FEATURES_TEXT);
};

export const getNewsletterText = async (): Promise<INewsletter> =>
{
    return await getDataFromUrl(GET_NEWSLETTER_TEXT);
};

export const getResetFormText = async (): Promise<IResetForm> =>
{
    return await getDataFromUrl(GET_RESET_FORM_TEXT);
};

export const getSigninFormText = async (): Promise<ISigninForm> =>
{
    return await getDataFromUrl(GET_SIGNIN_FORM_TEXT);
};

export const getSignupFormText = async (): Promise<ISignupForm> =>
{
    return await getDataFromUrl(GET_SIGNUP_FORM_TEXT);
};

export const getTestimonialsText = async (): Promise<ITestimonials> =>
{
    return await getDataFromUrl(GET_TESTIMONIALS_TEXT);
};

export const getUnsubscribeText = async (): Promise<IUnsubscribe> =>
{
    return await getDataFromUrl(GET_UNSUBSCRIBE_TEXT);
};

export const getUpdateSubscriberText = async (): Promise<IUpdateSubscriber> =>
{
    return await getDataFromUrl(GET_UPDATE_SUBSCRIBER_TEXT);
};
