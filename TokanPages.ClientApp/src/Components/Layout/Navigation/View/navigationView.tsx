import * as React from "react";
import { Link } from "react-router-dom";
import Icon from "@mdi/react";
import { mdiArrowLeft } from "@mdi/js";
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
                        <nav className="navigation-nav-drawer-root" style={{ width: props.width, left: props.isMenuOpen ? 0 : -props.width }} onMouseLeave={props.menuHandler}>
                            <div className="navigation-nav-drawer-backdrop" style={{ opacity: props.isMenuOpen ? 1 : 0 }} onClick={props.menuHandler}></div>
                            <div className="navigation-nav-drawer-container">
                                <div className="navigation-nav-drawer-hero">
                                    <RenderImage
                                        base={GET_ICONS_URL}
                                        source={props?.menu?.image}
                                        title="Logo"
                                        alt="An application logo"
                                        className="navigation-nav-drawer-logo"
                                    />
                                </div>
                                <RenderSideMenu
                                    isAnonymous={props.isAnonymous}
                                    languageId={props.languageId}
                                    items={props.menu?.items}
                                />
                            </div>
                        </nav>
                    </>
                </AppBar>
            )}
        </>
    );
};
