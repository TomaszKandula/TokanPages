import * as React from "react";
import { Link } from "react-router-dom";
import Icon from "@mdi/react";
import { mdiArrowLeft } from "@mdi/js";
import { Drawer } from "@material-ui/core";
import { GET_ICONS_URL } from "../../../../Api";
import { APP_BAR_HEIGHT } from "../../../../Shared/constants";
import { AppBar, IconButton, RenderImage } from "../../../../Shared/Components";
import { RenderSideMenu, RenderToolbarLargeScreen, RenderToolbarSmallScreen } from "../Components";
import { Properties } from "../Abstractions";
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
                        <Drawer anchor="left" open={props.drawerState.open} onClose={props.closeHandler}>
                            <nav className="sidemenu-drawer-container">
                                <div className="sidemenu-drawer-hero">
                                    <RenderImage
                                        base={GET_ICONS_URL}
                                        source={props?.menu?.image}
                                        title="Logo"
                                        alt="An application logo"
                                        className="sidemenu-logo"
                                    />
                                </div>
                                <RenderSideMenu
                                    isAnonymous={props.isAnonymous}
                                    languageId={props.languageId}
                                    items={props.menu?.items}
                                />
                            </nav>
                        </Drawer>
                    </>
                </AppBar>
            )}
        </>
    );
};
