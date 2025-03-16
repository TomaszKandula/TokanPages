import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { ContentPageDataAction } from "../../Store/Actions";
import { TryPostStateSnapshot } from "../../Shared/Services/SpaCaching";
import { ContactForm } from "../../Components/Contact";
import { Navigation } from "../../Components/Layout";

export const ContactPage = () => {
    const dispatch = useDispatch();

    const state = useSelector((state: ApplicationState) => state);
    const language = state.applicationLanguage;
    const contact = state?.contentPageData?.components?.sectionContactForm;

    React.useEffect(() => {
        dispatch(
            ContentPageDataAction.request(["layoutNavigation", "templates", "sectionCookiesPrompt", "sectionContactForm"], "ContactPage")
        );
    }, [language?.id]);

    React.useEffect(() => {
        if (contact?.language !== "") {
            TryPostStateSnapshot(state);
        }
    }, [state]);

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <main>
                <ContactForm
                    hasCaption={false}
                    hasIcon={true}
                    hasShadow={true}
                    background="background-colour-light-grey"
                    className="pt-120 pb-240"
                />
            </main>
        </>
    );
};
