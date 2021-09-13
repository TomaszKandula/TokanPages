import * as React from "react";
import Zoom from "@material-ui/core/Zoom";
import useScrollTrigger from "@material-ui/core/useScrollTrigger";
import scrollTopStyle from "./scrollTopStyle";

export interface IScrollTop 
{
    children: React.ReactElement;
}

const ScrollTop = (props: IScrollTop): JSX.Element =>
{
    const classes = scrollTopStyle();
    const trigger = useScrollTrigger(
    {
        disableHysteresis: true,
        threshold: 100
    });
  
    const handleClick = (event: React.MouseEvent<HTMLDivElement>) => 
    {
        const ownerDocument = ((event.target as HTMLDivElement).ownerDocument || document);
        const anchor = ownerDocument.querySelector("#back-to-top-anchor");
        if (anchor) anchor.scrollIntoView({ behavior: "smooth"});
    };
  
    return (
        <Zoom in={trigger}>
            <div onClick={handleClick} role="presentation" className={classes.scrollToTop}>
                {props.children}
            </div>
        </Zoom>
    );
}

export default ScrollTop;
