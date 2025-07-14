import * as React from "react";
import Icon from "@mdi/react";
import { mdiMenu } from "@mdi/js";
import { IconButton } from "../../../../../Shared/Components";
import { NavigationViewProps } from "../../Abstractions";

export const RenderMenuIcon = (props: NavigationViewProps): React.ReactElement => {
    return (
        <IconButton aria-label="menu" onClick={props.menuHandler}>
            <Icon path={mdiMenu} size={1} />
        </IconButton>
    );
};
