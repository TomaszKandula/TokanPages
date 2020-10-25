import React from "react";
import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";

import HorizontalNav from "../components/horizontal-navs/HorizontalNav";
import Header from "../components/Header";
import Elements from "../components/Elements";
import Features from "../components/Features";
import Featured from "../components/Featured";
import Contactme from "../components/Contactme";
import Footer from "../components/Footer";

export default function Index() 
{

  return (

    <>

      <HorizontalNav content={null} />
      <Container>
        <Grid container spacing={2}>
          <Grid item xs={12} md={6}>
            <Header />
          </Grid>
          <Grid item xs={12} md={6}>
            <Elements />
          </Grid>
        </Grid>
      </Container>

      <Features />
      <Featured />
      <Contactme />
      <Footer />

    </>

  );

}
