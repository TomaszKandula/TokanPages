import React from "react";
import Container from "@material-ui/core/Container";

export default function List(props: { content: any; }) 
{

	return (
    	<section>
      		<Container maxWidth="sm">       
                List of articles displayed here...
			</Container>
    	</section>
  	);
}
