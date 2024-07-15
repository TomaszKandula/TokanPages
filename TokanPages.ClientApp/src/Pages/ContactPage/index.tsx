import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { CustomBreadcrumb } from "../../Shared/Components";
import { Navigation, Footer } from "../../Components/Layout";
import { ContactForm } from "../../Components/Contact";

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
            <Navigation />
            <CustomBreadcrumb mt={12} mb={2} mr={5} ml={5} mtDivider={4} mbDivider={4} />
            <ContactForm hasCaption={false} hasIcon={true} hasShadow={true} />
            <Footer />
        </>
    );
};
