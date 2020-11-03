import React from "react";
import HorizontalNav from "../Components/Navigation/horizontal";
import Header from "../Components/header";
import Features from "../Components/features";
import Featured from "../Components/featured";
import ContactMe from "../Components/contactMe";
import Footer from "../Components/footer";

export default function Index() 
{

	return (
    	<>
      		<HorizontalNav content={null} />
			<Header />
      		<Features />
      		<Featured />
	  		<ContactMe />
      		<Footer backgroundColor="#FFFFFF" />
	    </>
    );

}
