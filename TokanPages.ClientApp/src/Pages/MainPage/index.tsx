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
import { ArticleFeature } from "../../Components/Articles";

import { Navigation, Header, Footer } from "../../Components/Layout";

import {
    ContentNavigationAction,
    ContentHeaderAction,
    ContentActivateAccountAction,
    ContentArticleFeaturesAction,
    ContentFooterAction,
    ContentClientsAction,
    ContentFeaturedAction,
    ContentTechnologiesAction,
    ContentNewsletterAction,
    ContentContactFormAction,
    ContentCookiesPromptAction,
    ContentTestimonialsAction,
    ContentTemplatesAction,
} from "../../Store/Actions";

export const MainPage = (): JSX.Element => {
    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentHeaderAction.get());
        dispatch(ContentActivateAccountAction.get());
        dispatch(ContentArticleFeaturesAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentClientsAction.get());
        dispatch(ContentFeaturedAction.get());
        dispatch(ContentTechnologiesAction.get());
        dispatch(ContentNewsletterAction.get());
        dispatch(ContentContactFormAction.get());
        dispatch(ContentCookiesPromptAction.get());
        dispatch(ContentTestimonialsAction.get());
        dispatch(ContentTemplatesAction.get());
    }, [language?.id]);

    return (
        <>
            <Navigation />
            <Header />
            <Clients />
            <Technologies />
            <ArticleFeature />
            <Featured />
            <Testimonials />
            <Newsletter />
            <ContactForm pt={6} hasCaption={true} />
            <Cookies />
            <Footer />
        </>
    );
};
