import * as React from "react";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import SignupForm from "../Components/Account/signupForm";
import Footer from "../Components/Layout/footer";

export default function storyPage() 
{
    return (
        <>
            <Navigation content={null} />
            <Container>
                <SignupForm />
            </Container>
            <Footer backgroundColor="#FAFAFA" />
        </>
    );
}
