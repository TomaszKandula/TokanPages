import * as React from "react";
import Icon from "@mdi/react";
import { mdiArrowLeft } from "@mdi/js";
import { APP_BAR_HEIGHT } from "../../../../Shared/constants";
import { AppBar, IconButton } from "../../../../Shared/Components";
import { RenderDrawer, RenderToolbarLargeScreen, RenderToolbarSmallScreen } from "../Components";
import { Properties } from "../Abstractions";
import "./navigationView.css";

export const NavigationView = (props: Properties): React.ReactElement => (
    <>
        {props.backNavigationOnly ? (
            <AppBar height={APP_BAR_HEIGHT}>
                <div className="navigation-nav-back">
                    <IconButton onClick={props.backPathHandler}>
                        <Icon path={mdiArrowLeft} size={1} />
                    </IconButton>
                </div>
            </AppBar>
        ) : (
            <AppBar height={APP_BAR_HEIGHT}>
                <RenderToolbarLargeScreen {...props} height={APP_BAR_HEIGHT} />
                <RenderToolbarSmallScreen {...props} height={APP_BAR_HEIGHT} />
                <RenderDrawer {...props} />
            </AppBar>
        )}
    </>
);
