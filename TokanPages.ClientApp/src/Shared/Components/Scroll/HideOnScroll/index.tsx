import * as React from "react";
import useScrollTrigger from "@material-ui/core/useScrollTrigger";
import Slide from "@material-ui/core/Slide";

export interface IHideOnScroll 
{
    children: React.ReactElement;
}

export const HideOnScroll = (props: IHideOnScroll): JSX.Element =>
{
    const trigger = useScrollTrigger();
    return (
        <Slide appear={false} direction="down" in={!trigger}>
            {props.children}
        </Slide>
    );
}
