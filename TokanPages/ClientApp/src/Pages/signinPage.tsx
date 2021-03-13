import * as React from "react";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import SigninForm from "../Components/Account/signinForm";
import Footer from "../Components/Layout/footer";
import { signinFormDefault } from "../Api/Defaults";
import { getSigninFormText } from "../Api/Services";

export default function SigninPage() 
{  
    const mountedRef = React.useRef(true);
    const [signinForm, setSigninForm] = React.useState(signinFormDefault);

    const updateContent = React.useCallback(async () => 
    {
        if (!mountedRef.current) return;
        setSigninForm(await getSigninFormText());
    }, [ ]);

    React.useEffect(() => 
    {
        updateContent();
        return () => { mountedRef.current = false; };
    }, [ updateContent ]);

    return (
        <>
            <Navigation content={null} />
            <Container>
                <SigninForm content={signinForm.content}  />
            </Container>
            <Footer backgroundColor="#FAFAFA" />
        </>
    );
}
