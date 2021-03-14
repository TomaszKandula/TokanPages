import * as React from "react";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import SignupForm from "../Components/Account/signupForm";
import Footer from "../Components/Layout/footer";
import { navigationDefault, signupFormDefault } from "../Api/Defaults";
import { getNavigationContent, getSignupFormContent } from "../Api/Services";

export default function SignupPage() 
{
    const mountedRef = React.useRef(true);
    const [signupForm, setSignupFormContent] = React.useState(signupFormDefault);
    const [navigation, setNavigationContent] = React.useState(navigationDefault);

    const updateContent = React.useCallback(async () => 
    {
        if (!mountedRef.current) return;
        setSignupFormContent(await getSignupFormContent());
        setNavigationContent(await getNavigationContent());
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
            <Footer backgroundColor="#FAFAFA" />
        </>
    );
}
