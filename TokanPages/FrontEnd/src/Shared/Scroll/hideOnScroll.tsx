import React from "react";
import useScrollTrigger from "@material-ui/core/useScrollTrigger";
import Slide from "@material-ui/core/Slide";

interface Props 
{
	children: React.ReactElement;
}

export default function HideOnScroll(props: Props) 
{
	const { children } = props;
	const trigger = useScrollTrigger();
 
	return (
	  <Slide appear={false} direction="down" in={!trigger}>
			{children}
	  </Slide>
	);
}
