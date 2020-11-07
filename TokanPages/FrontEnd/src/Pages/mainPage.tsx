import React from "react";
import HorizontalNav from "../Components/Navigation/horizontal";
import Header from "../Components/Layout/header";
import Footer from "../Components/Layout/footer";
import Features from "../Components/Features/features";
import Featured from "../Components/Featured/featured";
import ContactForm from "../Components/ContactForm/contactForm";
import Cookies from "../Components/Cookies/cookies";
import ArticleFeat from "../Components/Articles/articleFeat";

export default function Index() 
{

    return (
        <>
            <HorizontalNav content={null} />
            <Header />
            <Features />
            <ArticleFeat />
            <Featured />
            <ContactForm />
            <Cookies />
            <Footer backgroundColor="#FAFAFA" />
        </>
    );

}
