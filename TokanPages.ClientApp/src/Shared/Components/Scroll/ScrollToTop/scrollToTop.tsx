import * as React from "react";
import { useLocation } from "react-router-dom";
import Zoom from "@material-ui/core/Zoom";
import useScrollTrigger from "@material-ui/core/useScrollTrigger";
import "./scrollToTop.css";

export interface Properties {
    children: React.ReactElement;
}

export const ClearPageStart = (props: Properties): React.ReactElement => {
    const location = useLocation();

    React.useEffect(() => {
        window.scrollTo({ top: 0, left: 0, behavior: "smooth" });
    }, [location]);

    return <>{props.children}</>;
};

export const ScrollToTop = (props: Properties): React.ReactElement => {
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
            <div onClick={handleClick} role="presentation" className="scroll-to-top">
                {props.children}
            </div>
        </Zoom>
    );
};
