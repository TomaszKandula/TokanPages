import * as React from "react";
import { useScroll } from "../../../Shared/Hooks";
import "./appBar.css";

interface AppBarProps {
    height?: number;
    offset?: number;
    children: React.ReactElement | React.ReactElement[];
}

export const AppBar = (props: AppBarProps) => {
    const height = props.height ?? 50;
    const offset = props.offset ?? 0;

    const scroll = useScroll({ treshold: props.height });
    const [top, setTop] = React.useState(0);

    React.useEffect(() => {
        if (scroll.isScrollingUp || scroll.isScrolledTop) {
            setTop(0);
        } else {
            setTop(-height-offset);
        }
    }, [scroll.isScrollingUp, scroll.isScrolledTop]);

    return (
        <nav className="bulma-navbar app-bar" style={{ top: top, minHeight: props.height }}>
            {props.children}
        </nav>
    );
};
