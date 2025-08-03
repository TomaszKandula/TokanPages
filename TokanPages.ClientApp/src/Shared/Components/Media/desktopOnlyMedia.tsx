import React from "react";
import { useDimensions } from "../../../Shared/Hooks";

export interface DesktopOnlyMediaProps {
    children: React.ReactElement | React.ReactElement[];
}

export const DesktopOnlyMedia = (props: DesktopOnlyMediaProps): React.ReactElement => {
    const media = useDimensions();
    return media.isDesktop ? <>{props.children}</> : <></>;
};
