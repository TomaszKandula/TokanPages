import React from "react";
import Container from "@material-ui/core/Container";
import HorizontalNav from "../Components/Navigation/horizontal";
import Story from "../Components/Content/storyContent";
import Footer from "../Components/Layout/footer";

export default function storyPage() 
{
  
	return (    
    	<>     
		    <HorizontalNav content={null} />
    	  	<Container>
        		<Story content={null} />
	      	</Container>
      		<Footer backgroundColor="#FAFAFA" />
    	</>
	);

}
