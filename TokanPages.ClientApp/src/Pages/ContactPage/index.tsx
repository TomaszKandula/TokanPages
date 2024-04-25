import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { Box, Container } from "@material-ui/core";
import { ApplicationState } from "../../Store/Configuration";
import { Navigation, Footer } from "../../Components/Layout";
import { ContactForm } from "../../Components/Contact";

import { ContentNavigationAction, ContentFooterAction, ContentContactFormAction } from "../../Store/Actions";

export const ContactPage = () => {
    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentContactFormAction.get());
    }, [language?.id]);

    return (
        <>
            <Navigation />
            <Container>
                <Box mt={8}>
                    <ContactForm />
                </Box>
            </Container>
            <Footer />
        </>
    );
};
