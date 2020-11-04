import React from "react";
import Container from "@material-ui/core/Container";
import HorizontalNav from "../Components/Navigation/horizontal";
import Footer from "../Components/Layout/footer";
import List from "../Components/Articles/list";

export default function ArticlesPage() 
{

	return (
    	<>
		    <HorizontalNav content={null} />
    	  	<Container>
            <List content={null} />
	      	</Container>
      		<Footer backgroundColor="#FAFAFA" />
	    </>
    );

}
