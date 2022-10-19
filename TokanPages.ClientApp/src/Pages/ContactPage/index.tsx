import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { Box, Container } from "@material-ui/core";
import { IApplicationState } from "../../Store/Configuration";
import { Navigation, Footer } from "../../Components/Layout";
import { ContactForm } from "../../Components/Contact";

import { 
    ContentNavigationAction, 
    ContentFooterAction, 
    ContentContactFormAction 
} from "../../Store/Actions";

export const ContactPage = () => 
{
    const dispatch = useDispatch();
    const language = useSelector((state: IApplicationState) => state.applicationLanguage);

    const form = useSelector((state: IApplicationState) => state.contentContactForm);
    const navigation = useSelector((state: IApplicationState) => state.contentNavigation);
    const footer = useSelector((state: IApplicationState) => state.contentFooter);

    React.useEffect(() => 
    {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentContactFormAction.get());
    }, 
    [ language?.id ]);

    return (
        <>
            <Navigation content={navigation?.content} isLoading={navigation?.isLoading} />
            <Container>
                <Box mt={8} >
                    <ContactForm content={form?.content} isLoading={form?.isLoading} />
                </Box>
            </Container>
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
}
