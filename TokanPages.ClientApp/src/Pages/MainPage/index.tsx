import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { ContentPageDataAction } from "../../Store/Actions";
import { TryPostStateSnapshot } from "../../Shared/Services/SpaCaching";
import { Clients } from "../../Components/Clients";
import { Technologies } from "../../Components/Technologies";
import { Featured } from "../../Components/Featured";
import { Testimonials } from "../../Components/Testimonials";
import { Socials } from "../../Components/Socials";
import { Newsletter } from "../../Components/Newsletter";
import { ContactForm } from "../../Components/Contact";
import { Cookies } from "../../Components/Cookies";
import { ArticleFeature } from "../../Components/Articles";
import { Navigation, Header, Footer } from "../../Components/Layout";

export const MainPage = (): React.ReactElement => {
    const dispatch = useDispatch();

    const state = useSelector((state: ApplicationState) => state);
    const language = state.applicationLanguage;
    const header = state?.contentPageData?.components?.header;

    React.useEffect(() => {
        dispatch(
            ContentPageDataAction.request(
                [
                    "navigation",
                    "header",
                    "clients",
                    "technologies",
                    "articleFeatures",
                    "featured",
                    "testimonials",
                    "socials",
                    "newsletter",
                    "contactForm",
                    "cookiesPrompt",
                    "footer",
                    "templates",
                ],
                "MainPage"
            )
        );
    }, [language?.id]);

    React.useEffect(() => {
        if (header?.language !== "") {
            TryPostStateSnapshot(state);
        }
    }, [state]);

    return (
        <>
            <Navigation />
            <Header />
            <Clients />
            <Technologies />
            <ArticleFeature />
            <Featured />
            <Testimonials />
            <Socials />
            <Newsletter />
            <ContactForm hasCaption={true} />
            <Cookies />
            <Footer />
        </>
    );
};
