import React from "react";
import Container from "@material-ui/core/Container";
import HorizontalNav from "../Components/Navigation/horizontal";
import SignupForm from "../Components/Account/signupForm";
import Footer from "../Components/Layout/footer";

export default function storyPage() 
{
  
    return (    
        <>     
            <HorizontalNav content={null} />
            <Container>
                <SignupForm />
            </Container>
            <Footer backgroundColor="#FAFAFA" />
        </>
    );

}
