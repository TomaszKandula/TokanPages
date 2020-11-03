import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import { Divider, IconButton, makeStyles } from "@material-ui/core";
import { ArrowBack } from "@material-ui/icons";
import ReactHtmlParser from 'react-html-parser';
import axios from "axios";

const useStyles = makeStyles((theme) => (
{
    divider:
    {
        marginBottom: "30px"
    }
    ,
	typography:
	{
        color: "#616161",
		lineHeight: 2.0
	}}));

export default function Content(props: { content: any; }) 
{

	const classes = useStyles();

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
                    <Link to="/">
                        <IconButton>
                            <ArrowBack/>
                        </IconButton>        		
                    </Link> 
                    <Divider className={classes.divider} />
                    <Typography variant="body1" component="p" className={classes.typography}>
                        {ReactHtmlParser(terms)}
                    </Typography>
        		</Box>
			</Container>
    	</section>
  	);
}
