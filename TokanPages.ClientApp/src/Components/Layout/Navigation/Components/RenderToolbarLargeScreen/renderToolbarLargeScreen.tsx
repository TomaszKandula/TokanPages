import * as React from "react";
import { Link } from "react-router-dom";
import { NavigationViewProps } from "../../Abstractions";
import { GET_IMAGES_URL } from "../../../../../Api";
import { Image, Media } from "../../../../../Shared/Components";
import { RenderLanguageSection } from "../RenderLanguageSection";
import { RenderNavbarMenu } from "../RenderMenu";
import "./renderToolbarLargeScreen.css";

export const RenderToolbarLargeScreen = (props: NavigationViewProps): React.ReactElement => (
    <Media.DesktopOnly>
        <div className="bulma-navbar-start render-toolbar-constraints-start">
            <Link to={`/${props.languageId}`} rel="noopener nofollow" className="is-flex is-align-self-center">
                <Image
                    base={GET_IMAGES_URL}
                    source={props.navigation?.logo}
                    title="TomKandula logo"
                    alt="An application logo"
                    width={180}
                    height={30}
                    className="ml-4"
                />
            </Link>
        </div>
        <RenderNavbarMenu
            isAnonymous={props.isAnonymous}
            languageId={props.languageId}
            items={props.navigation?.menu?.items}
        />
        <div className="bulma-navbar-end render-toolbar-constraints-end">
            {props.isLoading ? null : <RenderLanguageSection {...props} />}
        </div>
    </Media.DesktopOnly>
);
