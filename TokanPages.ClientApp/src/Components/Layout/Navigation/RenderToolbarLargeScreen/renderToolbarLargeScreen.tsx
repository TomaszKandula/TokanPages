import * as React from "react";
import { Properties } from "../Abstractions";
import { Link } from "react-router-dom";
import { GET_ICONS_URL } from "../../../../Api";
import { RenderImage, RenderNavbarMenu } from "../../../../Shared/Components";
import { RenderLanguageSection } from "../RenderLanguageSection";

export const RenderToolbarLargeScreen = (props: Properties): React.ReactElement => (
    <div className="navigation-tool-bar" style={{ height: props.height }}>
        <div className="navigation-nav-menu navigation-nav-left">
            <Link to={`/${props.languageId}`} className="navigation-app-logo-small" rel="noopener nofollow">
                <RenderImage
                    base={GET_ICONS_URL}
                    source={props?.logoImgName}
                    title="TomKandula logo"
                    alt="An application logo"
                    className="navigation-app-left-logo"
                />
            </Link>
        </div>
        <div className="navigation-nav-items navigation-nav-centre">
            <RenderNavbarMenu isAnonymous={props.isAnonymous} languageId={props.languageId} items={props.menu?.items} />
        </div>
        <div className="navigation-nav-items navigation-nav-right">
            {props.isLoading ? null : <RenderLanguageSection {...props} />}
        </div>
    </div>
);
