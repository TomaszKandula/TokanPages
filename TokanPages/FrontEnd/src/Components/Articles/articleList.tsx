import React from "react";
import Container from "@material-ui/core/Container";
import { Box, Divider, Grid, IconButton, makeStyles } from "@material-ui/core";
import { Link } from "react-router-dom";
import { ArrowBack } from "@material-ui/icons";
import ArticleCard from "./articleCard";

const useStyles = makeStyles((theme) => (
{
	divider:
	{
		marginBottom: "30px"
	}
}));

export default function ArticleList() 
{

	const classes = useStyles();
	// Temp. mock
	const content =  JSON.parse('{"articles": [{"uid":"a8db7e28-2d47-463c-9c38-c17706056f72","title": "abc","desc":"mnbvcxz lkjhgfdsa poiuytrewq"},{"uid":"d2dc8a0d-1167-412b-86bd-1cf406f1ec71","title": "def","desc":"poiuytrewq lkjhgfdsa mnbvcxz"}]}');

	return (
    	<section>
      		<Container maxWidth="xs">       
			  	<Box pt={12} pb={8}>             
				  	<Link to="/">
                        <IconButton>
                            <ArrowBack/>
                        </IconButton>      
                    </Link> 
                    <Divider className={classes.divider} />
					<Grid container justify="center">    				
						<Grid item xs={12} sm={12}>
						  	{content.articles.map((item: { title: string; desc: string; uid: string; }) => ( 
								<ArticleCard title={item.title} desc={item.desc} uid={item.uid} key={item.uid} /> 
							))}
           				</Grid>
      				</Grid>
				</Box>
			</Container>
    	</section>
  	);
}
