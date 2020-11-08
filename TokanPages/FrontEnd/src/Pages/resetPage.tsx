import * as React from "react";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import ResetForm from "../Components/Account/resetForm";
import Footer from "../Components/Layout/footer";

export default function storyPage() 
{
  
    return (    
        <>     
            <Navigation content={null} />
            <Container>
                <ResetForm />
            </Container>
            <Footer backgroundColor="#FAFAFA" />
        </>
    );

}
