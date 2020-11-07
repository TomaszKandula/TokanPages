import React from "react";
import Container from "@material-ui/core/Container";
import HorizontalNav from "../Components/Navigation/horizontal";
import TermsContent from "../Components/Content/termsContent";
import Footer from "../Components/Layout/footer";

export default function TermsPage() 
{
  
    return (    
        <>     
            <HorizontalNav content={null} />
            <Container>
                <TermsContent content={null} />
            </Container>
            <Footer backgroundColor="#FAFAFA" />
        </>
    );

}
