import * as React from "react";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import SignupForm from "../Components/Account/signupForm";
import Footer from "../Components/Layout/footer";
import { navigationDefault, signupFormDefault } from "../Api/Defaults";
import { getNavigationText, getSignupFormText } from "../Api/Services";

export default function SignupPage() 
{
    const mountedRef = React.useRef(true);
    const [signupForm, setSignupForm] = React.useState(signupFormDefault);
    const [navigation, setNavigation] = React.useState(navigationDefault);

    const updateContent = React.useCallback(async () => 
    {
        if (!mountedRef.current) return;
        setSignupForm(await getSignupFormText());
        setNavigation(await getNavigationText());
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
