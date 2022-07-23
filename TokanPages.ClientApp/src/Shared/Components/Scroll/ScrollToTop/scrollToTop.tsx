import * as React from "react";
import Zoom from "@material-ui/core/Zoom";
import useScrollTrigger from "@material-ui/core/useScrollTrigger";
import { ScrollToTopStyle } from "./scrollToTopStyle";

export interface IScrollTop 
{
    children: React.ReactElement;
}

export const ScrollToTop = (props: IScrollTop): JSX.Element =>
{
    const classes = ScrollToTopStyle();
    const trigger = useScrollTrigger(
    {
        disableHysteresis: true,
        threshold: 100
    });
  
    const handleClick = (event: React.MouseEvent<HTMLDivElement>) => 
    {
        const ownerDocument = ((event.target as HTMLDivElement).ownerDocument || document);
        const anchor = ownerDocument.querySelector("#back-to-top-anchor") as Element;
        const headerOffset: number = 85;
        const elementPosition = anchor.getBoundingClientRect().top;
        const offsetPosition = elementPosition + window.pageYOffset - headerOffset;
 
        window.scrollTo(
        {
            top: offsetPosition,
            behavior: "smooth"
        });
    };
  
    return (
        <Zoom in={trigger}>
            <div onClick={handleClick} role="presentation" className={classes.scrollToTop}>
                {props.children}
            </div>
        </Zoom>
    );
}
