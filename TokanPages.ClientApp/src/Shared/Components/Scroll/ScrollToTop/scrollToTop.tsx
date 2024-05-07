import * as React from "react";
import { useLocation } from "react-router-dom";
import Zoom from "@material-ui/core/Zoom";
import useScrollTrigger from "@material-ui/core/useScrollTrigger";
import { ScrollToTopStyle } from "./scrollToTopStyle";

export interface Properties {
    children: React.ReactElement;
}

export const ClearPageStart = (props: Properties): JSX.Element => {
    const location = useLocation();

    React.useEffect(() => {
        window.scrollTo({top: 0, left: 0, behavior: "smooth" });
    }, [location]);

    return <>{props.children}</>;
}

export const ScrollToTop = (props: Properties): JSX.Element => {
    const classes = ScrollToTopStyle();
    const hasTrigger = useScrollTrigger({
        disableHysteresis: true,
        threshold: 100,
    });

    const handleClick = (event: React.MouseEvent<HTMLDivElement>) => {
        const ownerDocument = (event.target as HTMLDivElement).ownerDocument || document;
        const anchor = ownerDocument.querySelector("#back-to-top-anchor") as Element;
        const headerOffset: number = 85;
        const elementPosition = anchor.getBoundingClientRect().top;
        const offsetPosition = elementPosition + window.scrollY - headerOffset;

        window.scrollTo({
            top: offsetPosition,
            behavior: "smooth",
        });
    };

    return (
        <Zoom in={hasTrigger}>
            <div onClick={handleClick} role="presentation" className={classes.scrollToTop}>
                {props.children}
            </div>
        </Zoom>
    );
};
