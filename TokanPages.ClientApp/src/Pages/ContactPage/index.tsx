import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { Box, Container } from "@material-ui/core";
import { IApplicationState } from "../../Store/Configuration";
import { Navigation, Footer } from "../../Components/Layout";
import { ContactForm } from "../../Components/Contact";

import { 
    GetNavigationContentAction, 
    GetFooterContentAction, 
    GetContactFormContentAction 
} from "../../Store/Actions";

export const ContactPage = () => 
{
    const dispatch = useDispatch();

    const language = useSelector((state: IApplicationState) => state.userLanguage);
    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);
    const contactForm = useSelector((state: IApplicationState) => state.getContactFormContent);

    React.useEffect(() => 
    {
        dispatch(GetNavigationContentAction.getNavigationContent());
        dispatch(GetFooterContentAction.getFooterContent());
        dispatch(GetContactFormContentAction.getContactFormContent());
    }, 
    [ dispatch, language?.id ]);

    return (
        <>
            <Navigation content={navigation?.content} isLoading={navigation?.isLoading} />
            <Container>
                <Box mt={8} >
                    <ContactForm content={contactForm?.content} isLoading={contactForm?.isLoading} />
                </Box>
            </Container>
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
}
