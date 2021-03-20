import * as React from "react";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import SigninForm from "../Components/Account/signinForm";
import Footer from "../Components/Layout/footer";
import { footerDefault, navigationDefault, signinFormDefault } from "../Api/Defaults";
import { getFooterContent, getNavigationContent, getSigninFormContent } from "../Api/Services";

export default function SigninPage() 
{  
    const mountedRef = React.useRef(true);
    const [signinForm, setSigninFormContent] = React.useState({ data: signinFormDefault, isLoading: true });
    const [navigation, setNavigationContent] = React.useState({ data: navigationDefault, isLoading: true });
    const [footer, setFooterContent] = React.useState({ data: footerDefault, isLoading: true });

    const updateContent = React.useCallback(async () => 
    {
        if (!mountedRef.current) return;
        setSigninFormContent({ data: await getSigninFormContent(), isLoading: false });
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
                <SigninForm signinForm={signinForm.data} isLoading={signinForm.isLoading} />
            </Container>
            <Footer footer={footer.data} isLoading={footer.isLoading} />
        </>
    );
}
