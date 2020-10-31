import React from "react";

import HorizontalNav from "../Components/Navigation/horizontal";
import Header from "../Components/header";
import MainPicture from "../Components/mainPicture";
import Features from "../Components/features";
import Featured from "../Components/featured";
import ContactMe from "../Components/contactMe";
import Footer from "../Components/footer";

import Structure from "../Shared/structure";

export default function Index() 
{

	return (
    	<>
      		<HorizontalNav content={null} />
			<Structure 
        		column1 = {<Header />} 
        		column2 = {<MainPicture />} 
      		/>
      		<Features />
      		<Featured />
	  		<ContactMe />
      		<Footer />
	    </>
    );

}
