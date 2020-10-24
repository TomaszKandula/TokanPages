import React from "react";
import Container from "@material-ui/core/Container";

import HorizontalNav from "../components/horizontal-navs/HorizontalNav";
import Content from "../components/content/Content";
import Footer from "../components/Footer";

export default function Mystory() 
{
  
  return (    
    <>     
      <HorizontalNav content={null} />

      <Container>
        <Content content={null} />
        <Footer />
      </Container>

    </>
  );

}
