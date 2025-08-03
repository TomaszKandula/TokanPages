import React from "react";
import { useDimensions } from "../../../Shared/Hooks";

export interface TabletOnlyMediaProps {
    children: React.ReactElement | React.ReactElement[];
}

export const TabletOnlyMedia = (props: TabletOnlyMediaProps): React.ReactElement => {
    const media = useDimensions();
    return media.isTablet ? <>{props.children}</> : <></>;
};
