import React from "react";
import { usePageContent, useSnapshot, useUnhead } from "../../Shared/Hooks";
import { ContactForm } from "../../Components/Contact";
import { Footer, Navigation } from "../../Components/Layout";

export const ContactPage = () => {
    const heading = useUnhead("ContactPage");
    useSnapshot();
    usePageContent(
        ["layoutNavigation", "layoutFooter", "templates", "sectionCookiesPrompt", "sectionContactForm"],
        "ContactPage"
    );

    return (
        <>
            <Navigation />
            <main>
                <h1 className="seo-only">{heading}</h1>
                <ContactForm hasIcon hasShadow mode="page" />
            </main>
            <Footer />
        </>
    );
};
