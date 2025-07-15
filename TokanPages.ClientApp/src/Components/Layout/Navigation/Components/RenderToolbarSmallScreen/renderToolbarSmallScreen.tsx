import * as React from "react";
import { Link } from "react-router-dom";
import { NavigationViewProps } from "../../Abstractions";
import { RenderMenuIcon } from "../RenderMenuIcon";
import { CustomImage } from "../../../../../Shared/Components";
import { GET_ICONS_URL } from "../../../../../Api";
import { RenderLanguageSection } from "../RenderLanguageSection";

export const RenderToolbarSmallScreen = (props: NavigationViewProps): React.ReactElement => (
    props.media.isMobile || props.media.isTablet ?
        <>
            <div className="bulma-navbar-start">
                {props.isLoading ? null : <RenderMenuIcon {...props} />}
            </div>
            <div className="is-flex is-align-self-center">
                {props.media.isTablet
                ? <Link to={`/${props.languageId}`} rel="noopener nofollow" className="is-flex is-align-self-center">
                    <CustomImage
                        base={GET_ICONS_URL}
                        source={props?.logo}
                        title="TomKandula logo"
                        alt="An application logo"
                        width={180}
                        height={30}
                    />
                </Link>
                : <></>}
                {props.media.isMobile
                ? <Link to={`/${props.languageId}`} rel="noopener nofollow" className="is-flex is-align-self-center">
                    <CustomImage
                        base={GET_ICONS_URL}
                        source={props?.menu?.image}
                        title="TomKandula logo"
                        alt="An application logo"
                        width={40}
                        height={40}
                    />
                </Link>
                : <></>}
            </div>
            <div className="bulma-navbar-end">
                {props.isLoading ? null : <RenderLanguageSection {...props} />}
            </div>
        </>
    : <></>
);
