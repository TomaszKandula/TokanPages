import React from "react";
import Container from "@material-ui/core/Container";
import HorizontalNav from "../Components/Navigation/horizontal";
import Content from "../Components/Content/terms";
import Footer from "../Components/footer";

export default function Terms() 
{
  
	return (    
    	<>     
		    <HorizontalNav content={null} />
    	  	<Container>
        		<Content content={null} />
	      	</Container>
			<Footer />
    	</>
	);

}
