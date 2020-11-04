import React from "react";
import { Link } from "react-router-dom";
import { makeStyles } from "@material-ui/core/styles";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import useScrollTrigger from "@material-ui/core/useScrollTrigger";
import Slide from "@material-ui/core/Slide";

interface Props 
{
	children: React.ReactElement;
}

const useStyles = makeStyles((theme) => (
{
	appBar:
	{
		background: "#1976D2"
	},
	toolBar: 
	{ 
		justifyContent: "center", 
	},
	mainLogo:
	{
		width: 210,
	},
	mainLink:
	{
		marginTop: "10px",
		variant:"h5", 
		color: "inherit", 
		underline: "none"
	}
}));

function HideOnScroll(props: Props) 
{
	const { children } = props;
	const trigger = useScrollTrigger();
 
	return (
	  <Slide appear={false} direction="down" in={!trigger}>
			{children}
	  </Slide>
	);
}

export default function HorizontalNav(props: { content: any; }) 
{

	const classes = useStyles();

	return (

		<HideOnScroll {...props}>
			<AppBar className={classes.appBar}>
	      		<Toolbar className={classes.toolBar}>
        			<Link to="/" className={classes.mainLink}>
          				<img className={classes.mainLogo} src="https://maindbstorage.blob.core.windows.net/tokanpages/icons/main_logo.svg" alt="" />
        			</Link>
    	  		</Toolbar>
	    	</AppBar>
		</HideOnScroll>

    );

}
