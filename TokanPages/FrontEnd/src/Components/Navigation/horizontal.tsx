import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import Link from "@material-ui/core/Link";
import useScrollTrigger from "@material-ui/core/useScrollTrigger";
import Slide from "@material-ui/core/Slide";

interface Props 
{
	children: React.ReactElement;
}

const useStyles = makeStyles((theme) => (
{
	appbar:
	{
		background: "#1976D2"
	},
	toolbar: 
	{ 
		justifyContent: "center", 
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

	const content = 
	{
    	"brand": { image: "https://maindbstorage.blob.core.windows.net/tokanpages/images/main_logo.png", width: 165 },
    	...props.content
	};

	let brand;

	if (content.brand.image) 
  	{
    	brand = <img src={ content.brand.image } alt="" width={ content.brand.width } />;
  	} 
  	else 
  	{
    	brand = content.brand.text || '';
  	}

	return (

		<HideOnScroll {...props}>
			<AppBar className={classes.appbar}>
	      		<Toolbar className={classes.toolbar}>
        			<Link href="#" variant="h5" color="inherit" underline="none">
          				{brand}
        			</Link>
    	  		</Toolbar>
	    	</AppBar>
		</HideOnScroll>

    );

}
