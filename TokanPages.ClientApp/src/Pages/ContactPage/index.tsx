import React from "react";
import { usePageContent, useSnapshot, useUnhead } from "../../Shared/Hooks";
import { ContactForm } from "../../Components/Contact";
import { Footer, Navigation } from "../../Components/Layout";

export const ContactPage = () => {
    useUnhead("ContactPage");
    useSnapshot();
    usePageContent(["layoutNavigation", "layoutFooter", "templates", "sectionCookiesPrompt", "sectionContactForm"], "ContactPage");

    return (
        <>
            <Navigation />
            <main>
                <ContactForm hasIcon hasShadow />
            </main>
            <Footer />
        </>
    );
};
