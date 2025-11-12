import * as React from "react";
import Icon from "@mdi/react";
import { mdiMenu } from "@mdi/js";
import { IconButton } from "../../../../../Shared/Components";
import { NavigationViewProps } from "../../Types";

export const RenderMenuIcon = (props: NavigationViewProps): React.ReactElement => {
    const isMobile = props.media.isMobile ?? undefined;
    const isTablet = props.media.isTablet ?? undefined;

    return (
        <IconButton
            size={3.0}
            aria-label="menu"
            hasNoHoverEffect={isMobile || isTablet}
            className="no-select"
            onClick={props.triggerSideMenu}
        >
            <Icon path={mdiMenu} size={1} />
        </IconButton>
    );
};
