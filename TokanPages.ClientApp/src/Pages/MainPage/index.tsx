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
import { ArticleFeature } from "../../Components/Articles";
import { Navigation, Header, Footer } from "../../Components/Layout";
import { Showcase } from "../../Components/Showcase";

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
                    "featureShowcase",
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
            <main>
                <Header />
                <Clients />
                <Technologies background="background-colour-light-grey" />
                <Showcase />
                <ArticleFeature background="background-colour-light-grey" />
                <Featured />
                <Testimonials background="background-colour-light-grey" />
                <Socials />
                <Newsletter background="background-colour-light-grey" />
                <ContactForm hasCaption={true} />
            </main>
            <Footer />
        </>
    );
};
