import * as React from "react";
import { usePageContent, useSnapshot, useUnhead } from "../../Shared/Hooks";
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
    useUnhead("MainPage");
    useSnapshot();
    usePageContent([
        "layoutNavigation",
        "layoutHeader",
        "layoutFooter",
        "sectionClients",
        "sectionTechnologies",
        "sectionArticle",
        "sectionFeatured",
        "sectionTestimonials",
        "sectionShowcase",
        "sectionSocials",
        "sectionNewsletter",
        "sectionContactForm",
        "sectionCookiesPrompt",
        "templates",
    ],
    "MainPage");

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
