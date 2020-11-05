import * as React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Zoom from "@material-ui/core/Zoom";
import useScrollTrigger from "@material-ui/core/useScrollTrigger";

interface Props 
{
	children: React.ReactElement;
}

const useStyles = makeStyles((theme) => (
{
	scrollToTop: 
	{
		position: "fixed",
		bottom: theme.spacing(2),
		right: theme.spacing(2),
	}
}));

export default function ScrollToTop(props: Props) 
{

	const { children } = props;
	const classes = useStyles();

	const trigger = useScrollTrigger(
	{
	  	disableHysteresis: true,
	  	threshold: 100
	});
  
	const handleClick = (event: React.MouseEvent<HTMLDivElement>) => 
	{

		const anchor = ((event.target as HTMLDivElement).ownerDocument || document).querySelector("#back-to-top-anchor");
  
		if (anchor) 
	  	{
            anchor.scrollIntoView({ behavior: "smooth"});
        }

	};
  
	return (
	  <Zoom in={trigger}>
		<div onClick={handleClick} role="presentation" className={classes.scrollToTop}>
		  	{children}
		</div>
	  </Zoom>
	);
}
