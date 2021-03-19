import * as React from "react";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import SignupForm from "../Components/Account/signupForm";
import Footer from "../Components/Layout/footer";
import { footerDefault, navigationDefault, signupFormDefault } from "../Api/Defaults";
import { getFooterContent, getNavigationContent, getSignupFormContent } from "../Api/Services";

export default function SignupPage() 
{
    const mountedRef = React.useRef(true);
    const [signupForm, setSignupFormContent] = React.useState(signupFormDefault);
    const [navigation, setNavigationContent] = React.useState(navigationDefault);
    const [footer, setFooterContent] = React.useState(footerDefault);

    const updateContent = React.useCallback(async () => 
    {
        if (!mountedRef.current) return;
        setSignupFormContent(await getSignupFormContent());
        setNavigationContent(await getNavigationContent());
        setFooterContent(await getFooterContent());
    }, [ ]);

    React.useEffect(() => 
    {
        updateContent();
        return () => { mountedRef.current = false; };
    }, [ updateContent ]);

    return (
        <>
            <Navigation content={navigation.content} />
            <Container>
                <SignupForm content={signupForm.content} />
            </Container>
            <Footer footer={footer} backgroundColor="#FAFAFA" />
        </>
    );
}
