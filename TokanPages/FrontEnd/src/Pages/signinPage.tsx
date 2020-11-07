import React from "react";
import Container from "@material-ui/core/Container";
import HorizontalNav from "../Components/Navigation/horizontal";
import SigninForm from "../Components/Account/signinForm";
import Footer from "../Components/Layout/footer";

export default function storyPage() 
{
  
    return (    
        <>     
            <HorizontalNav content={null} />
            <Container>
                <SigninForm />
            </Container>
            <Footer backgroundColor="#FAFAFA" />
        </>
    );

}
