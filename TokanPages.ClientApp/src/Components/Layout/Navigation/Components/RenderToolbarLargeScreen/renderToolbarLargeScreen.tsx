import * as React from "react";
import { Link } from "react-router-dom";
import { NavigationViewProps } from "../../Abstractions";
import { GET_ICONS_URL } from "../../../../../Api";
import { CustomImage } from "../../../../../Shared/Components";
import { RenderLanguageSection } from "../RenderLanguageSection";
import { RenderNavbarMenu } from "../RenderMenu";

export const RenderToolbarLargeScreen = (props: NavigationViewProps): React.ReactElement =>
    props.media.isDesktop ? (
        <>
            <div className="bulma-navbar-start is-flex is-align-self-center">
                <Link to={`/${props.languageId}`} rel="noopener nofollow">
                    <CustomImage
                        base={GET_ICONS_URL}
                        source={props?.logo}
                        title="TomKandula logo"
                        alt="An application logo"
                        width={180}
                        height={30}
                        className="ml-4"
                    />
                </Link>
            </div>
            <div className="is-flex is-align-self-center">
                <RenderNavbarMenu
                    isAnonymous={props.isAnonymous}
                    languageId={props.languageId}
                    items={props.menu?.items}
                />
            </div>
            <div className="bulma-navbar-end">{props.isLoading ? null : <RenderLanguageSection {...props} />}</div>
        </>
    ) : (
        <></>
    );
