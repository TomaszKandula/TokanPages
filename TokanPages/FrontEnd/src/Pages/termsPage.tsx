import * as React from "react";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import StaticContent from "../Components/Content/staticContent";
import Footer from "../Components/Layout/footer";
import * as apiUrls from "../Shared/apis";

export default function TermsPage() 
{
  
    return (    
        <>     
            <Navigation content={null} />
            <Container>
                <StaticContent dataUrl={apiUrls.TERMS_URL} />
            </Container>
            <Footer backgroundColor="#FAFAFA" />
        </>
    );

}
