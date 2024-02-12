import * as React from "react";
import useScrollTrigger from "@material-ui/core/useScrollTrigger";
import Slide from "@material-ui/core/Slide";

interface Properties {
    children: React.ReactElement;
}

export const HideOnScroll = (props: Properties): JSX.Element => {
    const hasTrigger = useScrollTrigger();
    return (
        <Slide appear={false} direction="down" in={!hasTrigger}>
            {props.children}
        </Slide>
    );
};
