import React from "react";
import Container from "@material-ui/core/Container";
import HorizontalNav from "../Components/Navigation/horizontal";
import ResetForm from "../Components/Account/resetForm";
import Footer from "../Components/Layout/footer";

export default function storyPage() 
{
  
    return (    
        <>     
            <HorizontalNav content={null} />
            <Container>
                <ResetForm />
            </Container>
            <Footer backgroundColor="#FAFAFA" />
        </>
    );

}
