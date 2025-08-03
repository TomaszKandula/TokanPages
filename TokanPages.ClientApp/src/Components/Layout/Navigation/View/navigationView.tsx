import * as React from "react";
import Icon from "@mdi/react";
import { mdiArrowLeft } from "@mdi/js";
import { APP_BAR_HEIGHT_DESKTOP, APP_BAR_HEIGHT_NON_DESKTOP } from "../../../../Shared/constants";
import { AppBar, IconButton } from "../../../../Shared/Components";
import { RenderDrawer, RenderToolbarLargeScreen, RenderToolbarSmallScreen } from "../Components";
import { NavigationViewProps } from "../Abstractions";

const RenderBackNavigationOnly = (props: NavigationViewProps): React.ReactElement => (
    <AppBar height={APP_BAR_HEIGHT_DESKTOP}>
        <IconButton hasNoHoverEffect={props.media.isMobile || props.media.isTablet} onClick={props.backPathHandler}>
            <Icon path={mdiArrowLeft} size={1} />
        </IconButton>
    </AppBar>
);

const RenderFullNavigation = (props: NavigationViewProps): React.ReactElement => {
    const height = props.media.isDesktop ? APP_BAR_HEIGHT_DESKTOP : APP_BAR_HEIGHT_NON_DESKTOP;

    return (
        <AppBar height={height}>
            <RenderToolbarLargeScreen {...props} />
            <RenderToolbarSmallScreen {...props} />
            <RenderDrawer {...props} />
        </AppBar>
    );
};

export const NavigationView = (props: NavigationViewProps): React.ReactElement =>
    props.backNavigationOnly ? <RenderBackNavigationOnly {...props} /> : <RenderFullNavigation {...props} />;
