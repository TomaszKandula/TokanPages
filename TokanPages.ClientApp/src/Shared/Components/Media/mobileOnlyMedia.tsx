import React from "react";
import { useDimensions } from "../../../Shared/Hooks";

export interface MobileOnlyMediaProps {
    children: React.ReactElement | React.ReactElement[];
}

export const MobileOnlyMedia = (props: MobileOnlyMediaProps): React.ReactElement => {
    const media = useDimensions();
    return media.isMobile ? <>{props.children}</> : <></>;
}
