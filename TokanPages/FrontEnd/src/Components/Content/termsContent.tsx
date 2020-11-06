import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import { Divider, IconButton, makeStyles } from "@material-ui/core";
import { ArrowBack } from "@material-ui/icons";
import ReactHtmlParser from 'react-html-parser';
import axios from "axios";
import * as apiUrls from "../../Shared/apis";
import { IsEmpty } from "../../Shared/helpers"; 

const useStyles = makeStyles((theme) => (
{
    container:
    {
        maxWidth: "700px"
    },
    divider:
    {
        marginBottom: "30px"
    },
	typography:
	{
        textAlign: "justify",
        color: "#616161",
		lineHeight: 2.0
    }
}));

export default function TermsContent(props: { content: any; }) 
{

	const classes = useStyles();

    const [ terms, setTerms ] = useState("Fetching content...");
    const fetchTerms = async () => 
    {
        const response = await axios.get(apiUrls.TERMS_URL, {method: "get", responseType: "text"});
        setTerms(response.data);    
    }

    useEffect( () => { fetchTerms() }, [ terms ] );

    const renderTerms = (text: string) => 
    {
        return(
            <div data-aos="fade-up">
                {ReactHtmlParser(text)}
            </div>
        );
    }

    const content = !IsEmpty(terms) ? "Fetching content..." : renderTerms(terms);
    
	return (
    	<section>
      		<Container className={classes.container}>       
		        <Box py={12}>
                    <Link to="/">
                        <IconButton>
                            <ArrowBack/>
                        </IconButton>        		
                    </Link> 
                    <Divider className={classes.divider} />
                    <Typography variant="body1" component="span" className={classes.typography}>
                        {content}
                    </Typography>
        		</Box>
			</Container>
    	</section>
  	);
}
