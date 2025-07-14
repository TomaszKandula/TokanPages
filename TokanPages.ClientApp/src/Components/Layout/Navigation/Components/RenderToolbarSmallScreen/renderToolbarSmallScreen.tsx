import * as React from "react";
import { Link } from "react-router-dom";
import { NavigationViewProps } from "../../Abstractions";
import { RenderMenuIcon } from "../RenderMenuIcon";
import { CustomImage } from "../../../../../Shared/Components";
import { GET_ICONS_URL } from "../../../../../Api";
import { RenderLanguageSection } from "../RenderLanguageSection";

export const RenderToolbarSmallScreen = (props: NavigationViewProps) => (
    <nav className="bulma-navbar navigation-nav-small-screen">
        <div className={`navigation-tool-bar ${props.isMobile ? "px-1" : "px-6"}`} style={{ height: props.height }}>
            <div className="navigation-nav-menu navigation-nav-left">
                {props.isLoading ? null : <RenderMenuIcon {...props} />}
            </div>
            <div className="navigation-nav-items navigation-nav-centre">
                <Link to={`/${props.languageId}`} className="navigation-app-logo-small" rel="noopener nofollow">
                    <CustomImage
                        base={GET_ICONS_URL}
                        source={props?.logo}
                        title="TomKandula logo"
                        alt="An application logo"
                        className="navigation-app-full-logo"
                    />
                </Link>
                <Link to={`/${props.languageId}`} className="navigation-app-logo-large" rel="noopener nofollow">
                    <CustomImage
                        base={GET_ICONS_URL}
                        source={props?.menu?.image}
                        title="TomKandula logo"
                        alt="An application logo"
                        className="navigation-app-just-logo"
                    />
                </Link>
            </div>
            <div className="navigation-nav-items navigation-nav-right">
                {props.isLoading ? null : <RenderLanguageSection {...props} />}
            </div>
        </div>
    </nav>
);
