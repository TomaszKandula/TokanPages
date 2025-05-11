import * as React from "react";
import Slide from "@material-ui/core/Slide";
import { useScrollTrigger } from "../../../../Shared/Hooks";

interface Properties {
    children: React.ReactElement;
}

export const HideOnScroll = (props: Properties): React.ReactElement => {
    const hasTrigger = useScrollTrigger();
    return (
        <Slide appear={false} direction="down" in={hasTrigger}>
            {props.children}
        </Slide>
    );
};
