import React from "react";
import Container from "@material-ui/core/Container";
import HorizontalNav from "../Components/Navigation/horizontal";
import PolicyContent from "../Components/Content/policyContent";
import Footer from "../Components/Layout/footer";

export default function PolicyPage() 
{
  
    return (    
        <>     
            <HorizontalNav content={null} />
            <Container>
                <PolicyContent content={null} />
            </Container>
            <Footer backgroundColor="#FAFAFA" />
        </>
    );

}
