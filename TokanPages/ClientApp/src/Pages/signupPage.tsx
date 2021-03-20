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
    const [signupForm, setSignupFormContent] = React.useState({ data: signupFormDefault, isLoading: true });
    const [navigation, setNavigationContent] = React.useState({ data: navigationDefault, isLoading: true });
    const [footer, setFooterContent] = React.useState({ data: footerDefault, isLoading: true });

    const updateContent = React.useCallback(async () => 
    {
        if (!mountedRef.current) return;
        setSignupFormContent({ data: await getSignupFormContent(), isLoading: false });
        setNavigationContent({ data: await getNavigationContent(), isLoading: false });
        setFooterContent({ data: await getFooterContent(), isLoading: false });
    }, [ ]);

    React.useEffect(() => 
    {
        updateContent();
        return () => { mountedRef.current = false; };
    }, [ updateContent ]);

    return (
        <>
            <Navigation navigation={navigation.data} isLoading={navigation.isLoading} />
            <Container>
                <SignupForm signupForm={signupForm.data} isLoading={signupForm.isLoading} />
            </Container>
            <Footer footer={footer.data} isLoading={footer.isLoading} backgroundColor="#FAFAFA" />
        </>
    );
}
