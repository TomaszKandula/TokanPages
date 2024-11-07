import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { ContentPageDataAction } from "../../Store/Actions";
import { TrySnapshotState } from "../../Shared/Services/SpaCaching";
import { ContactForm } from "../../Components/Contact";
import { Navigation } from "../../Components/Layout";
import { Colours } from "../../Theme";

export const ContactPage = () => {
    const dispatch = useDispatch();

    const state = useSelector((state: ApplicationState) => state);
    const language = state.applicationLanguage;
    const contact = state?.contentPageData?.components?.contactForm;

    React.useEffect(() => {
        dispatch(ContentPageDataAction.request(["navigation", "templates", "contactForm"], "ContactPage"));
    }, [language?.id]);

    React.useEffect(() => {
        if (contact?.language !== "") {
            TrySnapshotState(state);
        }
    }, [state]);

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <ContactForm
                pt={15}
                pb={30}
                hasCaption={false}
                hasIcon={true}
                hasShadow={true}
                background={{ backgroundColor: Colours.colours.lightGray3 }}
            />
        </>
    );
};
