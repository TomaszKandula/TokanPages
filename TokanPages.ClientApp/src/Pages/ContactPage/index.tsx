import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { ContentPageDataAction } from "../../Store/Actions";
import { useSnapshot, useUnhead } from "../../Shared/Hooks";
import { ContactForm } from "../../Components/Contact";
import { Navigation } from "../../Components/Layout";

export const ContactPage = () => {
    useUnhead("ContactPage");
    useSnapshot();

    const dispatch = useDispatch();
    const state = useSelector((state: ApplicationState) => state);
    const language = state.applicationLanguage;

    React.useEffect(() => {
        dispatch(
            ContentPageDataAction.request(
                ["layoutNavigation", "templates", "sectionCookiesPrompt", "sectionContactForm"],
                "ContactPage"
            )
        );
    }, [language?.id]);

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <main>
                <ContactForm hasCaption={false} hasIcon={true} hasShadow={true} className="pt-120 pb-240" />
            </main>
        </>
    );
};
