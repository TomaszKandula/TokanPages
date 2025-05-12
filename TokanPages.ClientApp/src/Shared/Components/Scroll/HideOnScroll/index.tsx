import * as React from "react";
import Slide from "@material-ui/core/Slide";
import { useScroll } from "../../../../Shared/Hooks";

interface Properties {
    children: React.ReactElement;
}

export const HideOnScroll = (props: Properties): React.ReactElement => {
    const scroll = useScroll();
    return (
        <Slide appear={false} direction="down" in={scroll.isScrollingUp}>
            {props.children}
        </Slide>
    );
};
