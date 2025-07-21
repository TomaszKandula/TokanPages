import * as React from "react";
import { useLocation } from "react-router-dom";
import Icon from "@mdi/react";
import { mdiChevronUp } from "@mdi/js";
import { useScroll } from "../../../../Shared/Hooks";
import "./scrollToTop.css";

export interface Properties {
    children: React.ReactElement | React.ReactElement[];
}

export const ClearPageStart = (props: Properties): React.ReactElement => {
    const location = useLocation();

    React.useEffect(() => {
        window.scrollTo({ top: 0, left: 0, behavior: "smooth" });
    }, [location]);

    return <>{props.children}</>;
};

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

export const ScrollToTop = (): React.ReactElement => {
    const scroll = useScroll({ offset: 64 });

    return (
        <div style={{ visibility: scroll.isScrolledTop ? "hidden" : "visible" }}>
            <div onClick={handleClick} role="presentation" className="scroll-to-top">
                <div aria-label="scroll back to top" className="scroll-to-top-button">
                    <Icon path={mdiChevronUp} size={1} />
                </div>
            </div>
        </div>
    );
};
