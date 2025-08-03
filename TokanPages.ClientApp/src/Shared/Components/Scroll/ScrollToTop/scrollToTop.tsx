import * as React from "react";
import Icon from "@mdi/react";
import { mdiChevronUp } from "@mdi/js";
import { useDimensions, useScroll } from "../../../../Shared/Hooks";
import { APP_BAR_HEIGHT_DESKTOP, APP_BAR_HEIGHT_NON_DESKTOP } from "../../../../Shared/constants";
import "./scrollToTop.css";

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
    const media = useDimensions();
    const treshold = media.isDesktop ? APP_BAR_HEIGHT_DESKTOP : APP_BAR_HEIGHT_NON_DESKTOP;
    const scroll = useScroll({ treshold: treshold });

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
