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
	},
	typography:
	{
		color: "#616161",
		lineHeight: 2.0
	}
}));

export default function Content(props: { content: any; }) 
{

	const classes = useStyles();

    const [ story, setStory ] = useState("Fetching content...");
    const fetchStory = async () => 
    {
        const response = await axios.get("http://localhost:3000/static/mystory.html", {method: "get", responseType: "text"});
        setStory(response.data);    
    }

    useEffect( () => { fetchStory() }, [ story ] );
	
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
                    <Typography variant="body1" component="span" className={classes.typography}>
                        {ReactHtmlParser(story)}
                    </Typography>
        		</Box>
			</Container>
    	</section>
  	);
}
