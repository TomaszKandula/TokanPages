import * as React from "react";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import SigninForm from "../Components/Account/signinForm";
import Footer from "../Components/Layout/footer";

export default function storyPage() 
{  
    return (
        <>
            <Navigation content={null} />
            <Container>
                <SigninForm />
            </Container>
            <Footer backgroundColor="#FAFAFA" />
        </>
    );
}
