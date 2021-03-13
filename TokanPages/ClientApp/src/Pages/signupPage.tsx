import * as React from "react";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import SignupForm from "../Components/Account/signupForm";
import Footer from "../Components/Layout/footer";
import { signupFormDefault } from "../Api/Defaults";
import { getSignupFormText } from "../Api/Services";

export default function SignupPage() 
{
    const mountedRef = React.useRef(true);
    const [signupForm, setSignupForm] = React.useState(signupFormDefault);

    const updateContent = React.useCallback(async () => 
    {
        if (!mountedRef.current) return;
        setSignupForm(await getSignupFormText());
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
                <SignupForm content={signupForm.content} />
            </Container>
            <Footer backgroundColor="#FAFAFA" />
        </>
    );
}
