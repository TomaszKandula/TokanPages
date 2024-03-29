import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { Clients } from "../../Components/Clients";
import { Technologies } from "../../Components/Technologies";
import { Featured } from "../../Components/Featured";
import { Testimonials } from "../../Components/Testimonials";
import { Newsletter } from "../../Components/Newsletter";
import { ContactForm } from "../../Components/Contact";
import { Cookies } from "../../Components/Cookies";
import { Features } from "../../Components/Features";

import { Navigation, Header, Footer } from "../../Components/Layout";

import {
    ContentNavigationAction,
    ContentHeaderAction,
    ContentActivateAccountAction,
    ContentArticleFeaturesAction,
    ContentFooterAction,
    ContentClientsAction,
    ContentFeaturedAction,
    ContentFeaturesAction,
    ContentNewsletterAction,
    ContentContactFormAction,
    ContentCookiesPromptAction,
    ContentTestimonialsAction,
} from "../../Store/Actions";

export const MainPage = (): JSX.Element => {
    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    const navigation = useSelector((state: ApplicationState) => state.contentNavigation);
    const footer = useSelector((state: ApplicationState) => state.contentFooter);
    const header = useSelector((state: ApplicationState) => state.contentHeader);
    const clients = useSelector((state: ApplicationState) => state.contentClients);
    const features = useSelector((state: ApplicationState) => state.contentFeatures);
    const articles = useSelector((state: ApplicationState) => state.contentArticleFeatures);
    const featured = useSelector((state: ApplicationState) => state.contentFeatured);
    const testimonials = useSelector((state: ApplicationState) => state.contentTestimonials);
    const newsletter = useSelector((state: ApplicationState) => state.contentNewsletter);
    const contactForm = useSelector((state: ApplicationState) => state.contentContactForm);
    const cookiesPrompt = useSelector((state: ApplicationState) => state.contentCookiesPrompt);

    React.useEffect(() => {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentHeaderAction.get());
        dispatch(ContentActivateAccountAction.get());
        dispatch(ContentArticleFeaturesAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentClientsAction.get());
        dispatch(ContentFeaturedAction.get());
        dispatch(ContentFeaturesAction.get());
        dispatch(ContentNewsletterAction.get());
        dispatch(ContentContactFormAction.get());
        dispatch(ContentCookiesPromptAction.get());
        dispatch(ContentTestimonialsAction.get());
    }, [language?.id]);

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
};
