import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { ContactForm } from "../../Components/Contact";
import { Navigation } from "../../Components/Layout";
import { Colours } from "../../Theme";

import {
    ContentNavigationAction,
    ContentFooterAction,
    ContentContactFormAction,
    ContentTemplatesAction,
} from "../../Store/Actions";

export const ContactPage = () => {
    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentContactFormAction.get());
        dispatch(ContentTemplatesAction.get());
    }, [language?.id]);

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
