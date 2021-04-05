import * as React from "react";
import useScrollTrigger from "@material-ui/core/useScrollTrigger";
import Slide from "@material-ui/core/Slide";

export interface IHideOnScroll 
{
    children: React.ReactElement;
}

export default function HideOnScroll(props: IHideOnScroll) 
{
    const { children } = props;
    const trigger = useScrollTrigger();
 
    return (
        <Slide appear={false} direction="down" in={!trigger}>
            {children}
        </Slide>
    );
}
