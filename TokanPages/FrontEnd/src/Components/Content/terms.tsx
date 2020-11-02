import React, { useEffect, useState } from "react";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import ReactHtmlParser from 'react-html-parser';
import axios from "axios";

export default function Content(props: { content: any; }) 
{

    const [ terms, setTerms ] = useState("Fetching content...");
    const fetchTerms = async () => 
    {
        const response = await axios.get("http://localhost:3000/static/terms.html", {method: "get", responseType: "text"});
        setTerms(response.data);    
    }

    useEffect( () => { fetchTerms() }, [ terms ] );

	return (
    	<section>
      		<Container maxWidth="sm">       
		        <Box py={12}>
                <Typography variant="overline" component="span">
                    {ReactHtmlParser(terms)}
                </Typography>
        		</Box>
			</Container>
    	</section>
  	);
}
