import * as React from "react";
import { Link } from "react-router-dom";
import Icon from "@mdi/react";
import { mdiArrowLeft } from "@mdi/js";
import { APP_BAR_HEIGHT } from "../../../../Shared/constants";
import { AppBar, IconButton } from "../../../../Shared/Components";
import { Properties } from "../Abstractions";
import { SideMenuView } from "../SideMenu/sideMenuView";
import { RenderToolbarLargeScreen } from "../RenderToolbarLargeScreen";
import { RenderToolbarSmallScreen } from "../RenderToolbarSmallScreen";
import "./navigationView.css";

export const NavigationView = (props: Properties): React.ReactElement => {
    const mainPath = `/${props.languageId}`;
    const navigationPath = props.backPathFragment === undefined ? mainPath : `${mainPath}${props.backPathFragment}`;

    return (
        <>
            {props.backNavigationOnly ? (
                <AppBar height={APP_BAR_HEIGHT}>
                    <Link to={navigationPath} rel="noopener nofollow">
                        <div className="navigation-nav-back">
                            <IconButton>
                                <Icon path={mdiArrowLeft} size={1} />
                            </IconButton>
                        </div>
                    </Link>
                </AppBar>
            ) : (
                <AppBar height={APP_BAR_HEIGHT}>
                    <>
                        <nav className="bulma-navbar navigation-nav-large-screen">
                            <RenderToolbarLargeScreen {...props} height={APP_BAR_HEIGHT} />
                        </nav>
                        <nav className="bulma-navbar navigation-nav-small-screen">
                            <RenderToolbarSmallScreen {...props} height={APP_BAR_HEIGHT} />
                        </nav>
                        <SideMenuView
                            drawerState={props.drawerState}
                            closeHandler={props.closeHandler}
                            isAnonymous={props.isAnonymous}
                            languageId={props.languageId}
                            menu={props.menu}
                        />
                    </>
                </AppBar>
            )}
        </>
    );
};
