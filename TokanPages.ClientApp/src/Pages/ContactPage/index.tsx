import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { Box, Container } from "@material-ui/core";
import { ActionCreators as NavigationContent } from "../../Redux/Actions/Content/getNavigationContentAction";
import { ActionCreators as FooterContent } from "../../Redux/Actions/Content/getFooterContentAction";
import { ActionCreators as ContactFormContent } from "../../Redux/Actions/Content/getContactFormContentAction";
import { IApplicationState } from "../../Redux/applicationState";
import Navigation from "../../Components/Layout/navigation";
import Footer from "../../Components/Layout/footer";
import ContactForm from "../../Components/Contact/contactForm";

const ContactPage = () => 
{
    const dispatch = useDispatch();

    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);
    const contactForm = useSelector((state: IApplicationState) => state.getContactFormContent);

    React.useEffect(() => 
    {
        dispatch(NavigationContent.getNavigationContent());
        dispatch(FooterContent.getFooterContent());
        dispatch(ContactFormContent.getContactFormContent());
    }, 
    [ dispatch ]);

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

export default ContactPage;
