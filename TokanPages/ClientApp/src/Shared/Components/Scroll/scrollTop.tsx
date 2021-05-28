import * as React from "react";
import Zoom from "@material-ui/core/Zoom";
import useScrollTrigger from "@material-ui/core/useScrollTrigger";
import scrollTopStyle from "./scrollTopStyle";

export interface IScrollTop 
{
    children: React.ReactElement;
}

export default function ScrollTop(props: IScrollTop) 
{
    const { children } = props;
    const classes = scrollTopStyle();

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
