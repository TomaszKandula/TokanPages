import * as React from "react";
import { usePageContent, useSnapshot, useUnhead } from "../../Shared/Hooks";
import { Clients } from "../../Components/Clients";
import { Technologies } from "../../Components/Technologies";
import { Featured } from "../../Components/Featured";
import { Testimonials } from "../../Components/Testimonials";
import { Socials } from "../../Components/Socials";
import { NewsletterSection } from "../../Components/Newsletter";
import { ContactForm } from "../../Components/Contact";
import { ArticleFeature } from "../../Components/Articles";
import { Navigation, Header, Footer } from "../../Components/Layout";
import { Showcase } from "../../Components/Showcase";

export const MainPage = (): React.ReactElement => {
    useUnhead("MainPage");
    useSnapshot();
    usePageContent(
        [
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
        "MainPage"
    );

    return (
        <>
            <Navigation />
            <main>
                <Header />
                <Clients className="has-background-white-ter" />
                <Technologies />
                <Showcase className="has-background-white-bis" />
                <ArticleFeature />
                <Featured className="has-background-white-bis" />
                <Testimonials />
                <Socials className="has-background-white-bis" />
                <NewsletterSection className="has-background-info-95" />
                <ContactForm hasCaption={true} mode="section" />
            </main>
            <Footer />
        </>
    );
};
