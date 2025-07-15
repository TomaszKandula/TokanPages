import * as React from "react";
import { useScroll } from "../../../Shared/Hooks";
import "./appBar.css";

interface AppBarProps {
    height?: number;
    children: React.ReactElement | React.ReactElement[];
}

export const AppBar = (props: AppBarProps) => {
    const scroll = useScroll();
    const [top, setTop] = React.useState(0);
    const height = props.height ?? 50;

    React.useEffect(() => {
        if (scroll.isScrollingUp || scroll.isScrolledTop) {
            setTop(0);
        } else {
            setTop(-height);
        }
    }, [scroll.isScrollingUp, scroll.isScrolledTop]);

    return (
        <nav className="bulma-navbar app-bar" style={{ top: top, minHeight: props.height }}>
            {props.children}
        </nav>
    );
};
