import * as React from "react";
import Icon from "@mdi/react";
import { mdiArrowLeft } from "@mdi/js";
import { APP_BAR_HEIGHT } from "../../../../Shared/constants";
import { AppBar, IconButton } from "../../../../Shared/Components";
import { RenderDrawer, RenderToolbarLargeScreen, RenderToolbarSmallScreen } from "../Components";
import { NavigationViewProps } from "../Abstractions";

export const NavigationView = (props: NavigationViewProps): React.ReactElement =>
    props.backNavigationOnly ? (
        <AppBar height={APP_BAR_HEIGHT}>
            <IconButton onClick={props.backPathHandler} className="m-2">
                <Icon path={mdiArrowLeft} size={1} />
            </IconButton>
        </AppBar>
    ) : (
        <AppBar height={APP_BAR_HEIGHT}>
            <RenderToolbarLargeScreen {...props} />
            <RenderToolbarSmallScreen {...props} />
            <RenderDrawer {...props} />
        </AppBar>
    );
