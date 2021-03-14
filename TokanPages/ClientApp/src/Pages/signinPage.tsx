import * as React from "react";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import SigninForm from "../Components/Account/signinForm";
import Footer from "../Components/Layout/footer";
import { navigationDefault, signinFormDefault } from "../Api/Defaults";
import { getNavigationContent, getSigninFormContent } from "../Api/Services";

export default function SigninPage() 
{  
    const mountedRef = React.useRef(true);
    const [signinForm, setSigninFormContent] = React.useState(signinFormDefault);
    const [navigation, setNavigationContent] = React.useState(navigationDefault);

    const updateContent = React.useCallback(async () => 
    {
        if (!mountedRef.current) return;
        setSigninFormContent(await getSigninFormContent());
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
                <SigninForm content={signinForm.content}  />
            </Container>
            <Footer backgroundColor="#FAFAFA" />
        </>
    );
}
