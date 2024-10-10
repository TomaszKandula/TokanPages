import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { ContentPageDataAction } from "../../Store/Actions";
import { ContactForm } from "../../Components/Contact";
import { Navigation } from "../../Components/Layout";
import { Colours } from "../../Theme";

export const ContactPage = () => {
    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(ContentPageDataAction.request(["navigation", "templates", "contactForm"], "ContactPage"));
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
