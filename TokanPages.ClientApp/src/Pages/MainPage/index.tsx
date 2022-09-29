import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "../../Store/Configuration";
import { Clients } from "../../Components/Clients";
import { Technologies } from "../../Components/Technologies";
import { Featured } from "../../Components/Featured";
import { Testimonials } from "../../Components/Testimonials";
import { Newsletter } from "../../Components/Newsletter";
import { ContactForm } from "../../Components/Contact";
import { Cookies } from "../../Components/Cookies";
import { Features } from "../../Components/Features";

import { 
    Navigation, 
    Header, 
    Footer 
} from "../../Components/Layout";

import { 
    GetNavigationContentAction,
    GetHeaderContentAction,
    GetActivateAccountContentAction,
    GetArticleFeaturesContentAction,
    GetFooterContentAction,
    GetClientsContentAction,
    GetFeaturedContentAction,
    GetFeaturesContentAction,
    GetNewsletterContentAction,
    GetContactFormContentAction,
    GetCookiesPromptContentAction,
    GetTestimonialsContentAction    
} from "../../Store/Actions";

export const MainPage = (): JSX.Element => 
{
    const dispatch = useDispatch();

    const language = useSelector((state: IApplicationState) => state.userLanguage);

    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);
    const header = useSelector((state: IApplicationState) => state.getHeaderContent);
    const clients = useSelector((state: IApplicationState) => state.getClientsContent);
    const features = useSelector((state: IApplicationState) => state.getFeaturesContent);
    const articles = useSelector((state: IApplicationState) => state.getArticleFeaturesContent);
    const featured = useSelector((state: IApplicationState) => state.getFeaturedContent);
    const testimonials = useSelector((state: IApplicationState) => state.getTestimonialsContent);
    const newsletter = useSelector((state: IApplicationState) => state.getNewsletterContent);
    const contactForm = useSelector((state: IApplicationState) => state.getContactFormContent);
    const cookiesPrompt = useSelector((state: IApplicationState) => state.getCookiesPromptContent);

    React.useEffect(() => 
    { 
        dispatch(GetNavigationContentAction.getNavigationContent());
        dispatch(GetHeaderContentAction.getHeaderContent());
        dispatch(GetActivateAccountContentAction.getActivateAccountContent());
        dispatch(GetArticleFeaturesContentAction.getArticleFeaturesContent());
        dispatch(GetFooterContentAction.getFooterContent());
        dispatch(GetClientsContentAction.getClientsContent());
        dispatch(GetFeaturedContentAction.getFeaturedContent());
        dispatch(GetFeaturesContentAction.getFeaturesContent());
        dispatch(GetNewsletterContentAction.getNewsletterContent());
        dispatch(GetContactFormContentAction.getContactFormContent());
        dispatch(GetCookiesPromptContentAction.getCookiesPromptContent());    
        dispatch(GetTestimonialsContentAction.getTestimonialsContent());
    }, 
    [ dispatch, language?.id ]);

    return (
        <>
            <Navigation content={navigation?.content} isLoading={navigation?.isLoading} />
            <Header content={header?.content} isLoading={header?.isLoading} />
            <Clients content={clients?.content} isLoading={clients?.isLoading} />
            <Technologies content={features?.content} isLoading={features?.isLoading} />
            <Features content={articles?.content} isLoading={articles?.isLoading} />
            <Featured content={featured?.content} isLoading={featured?.isLoading} />
            <Testimonials content={testimonials?.content} isLoading={testimonials?.isLoading} />
            <Newsletter content={newsletter?.content} isLoading={newsletter?.isLoading} />
            <ContactForm content={contactForm?.content} isLoading={contactForm?.isLoading} />
            <Cookies content={cookiesPrompt?.content} isLoading={cookiesPrompt?.isLoading} />
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
}
