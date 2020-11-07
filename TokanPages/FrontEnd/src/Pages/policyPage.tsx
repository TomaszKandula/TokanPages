import React from "react";
import Container from "@material-ui/core/Container";
import HorizontalNav from "../Components/Navigation/horizontal";
import StaticContent from "../Components/Content/staticContent";
import Footer from "../Components/Layout/footer";
import * as apiUrls from "../Shared/apis";

export default function PolicyPage() 
{
  
    return (    
        <>     
            <HorizontalNav content={null} />
            <Container>
                <StaticContent dataUrl={apiUrls.POLICY_URL} />
            </Container>
            <Footer backgroundColor="#FAFAFA" />
        </>
    );

}
