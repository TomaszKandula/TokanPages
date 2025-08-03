import * as React from "react";
import { useDimensions, useScroll } from "../../../Shared/Hooks";
import "./appBar.css";

interface AppBarProps {
    height?: number;
    offset?: number;
    children: React.ReactElement | React.ReactElement[];
}

export const AppBar = (props: AppBarProps) => {
    const media = useDimensions();
    const height = props.height ?? 50;
    const offset = props.offset ?? 0;
    const isMobileOrTablet = media.isMobile || media.isTablet;

    const scroll = useScroll({ treshold: props.height });
    const [top, setTop] = React.useState(0);

    React.useEffect(() => {
        if (scroll.isScrollingUp || scroll.isScrolledTop) {
            setTop(0);
        } else {
            setTop(-height - offset);
        }
    }, [scroll.isScrollingUp, scroll.isScrolledTop]);

    return (
        <nav
            className={`bulma-navbar app-bar ${isMobileOrTablet ? "is-flex" : ""} `}
            style={{ top: top, minHeight: props.height }}
        >
            {props.children}
        </nav>
    );
};
