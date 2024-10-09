import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { ContentPageDataAction } from "../../Store/Actions";
import { Clients } from "../../Components/Clients";
import { Technologies } from "../../Components/Technologies";
import { Featured } from "../../Components/Featured";
import { Testimonials } from "../../Components/Testimonials";
import { Newsletter } from "../../Components/Newsletter";
import { ContactForm } from "../../Components/Contact";
import { Cookies } from "../../Components/Cookies";
import { ArticleFeature } from "../../Components/Articles";
import { Navigation, Header, Footer } from "../../Components/Layout";

export const MainPage = (): React.ReactElement => {
    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(
            ContentPageDataAction.request([
                "navigation",
                "header",
                "clients",
                "technologies",
                "articleFeatures",
                "featured",
                "testimonials",
                "newsletter",
                "contactForm",
                "cookiesPrompt",
                "footer",
                "templates",
            ])
        );
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
