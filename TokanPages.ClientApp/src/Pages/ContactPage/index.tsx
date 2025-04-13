import React from "react";
import { usePageContent, useSnapshot, useUnhead } from "../../Shared/Hooks";
import { ContactForm } from "../../Components/Contact";
import { Navigation } from "../../Components/Layout";

export const ContactPage = () => {
    useUnhead("ContactPage");
    useSnapshot();
    usePageContent(["layoutNavigation", "templates", "sectionCookiesPrompt", "sectionContactForm"], "ContactPage");

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <main>
                <ContactForm hasCaption={false} hasIcon={true} hasShadow={true} className="pt-120 pb-240" />
            </main>
        </>
    );
};
