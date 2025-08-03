import * as React from "react";
import { useSelector } from "react-redux";
import { Link as RouterLink } from "react-router-dom";
import { NavigationViewProps } from "../../Abstractions";
import { RenderMenuIcon } from "../RenderMenuIcon";
import { GET_FLAG_URL, GET_ICONS_URL } from "../../../../../Api";
import { ApplicationState } from "../../../../../Store/Configuration";
import { CustomImage, Icon, Link, Media } from "../../../../../Shared/Components";
import { APP_BAR_HEIGHT_DESKTOP, APP_BAR_HEIGHT_NON_DESKTOP_TOP } from "Shared/constants";
import "./renderToolbarSmallScreen.css";

const RenderDoubleToolbar = (props: NavigationViewProps) => { 
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const signup = props.menu.items.filter((item) => item.link?.includes("signup") ? item : undefined)[0];

    return (
        <div className="is-flex is-flex-direction-column is-flex-grow-1">
            <div className="is-flex is-justify-content-space-between is-align-items-center" style={{ height: APP_BAR_HEIGHT_NON_DESKTOP_TOP }}>
                <a className="bulma-navbar-start is-flex is-align-items-center ml-4" onClick={() => {}}>
                    <CustomImage
                        base={GET_FLAG_URL}
                        source={`${props.languageId}.png`}
                        title="Language flag"
                        alt={`A flag (${props.languageId}) for current language selection`}
                        className="bulma-image bulma-is-16x16"
                    />
                    <div className="has-text-black ml-2">{props.languageId?.toUpperCase()}</div>
                    <Icon name="ChevronDown" size={0.75} className="ml-1" />
                </a>
                <Link to={signup.link ?? ""} className="bulma-navbar-end is-flex mr-4">
                    <Icon name="PlusCircleOutline" size={0.75} className="mr-1" />
                    <p className="is-size-7 has-text-black">{data.components.accountUserSignup.caption}</p>
                </Link>
            </div>
            <hr className="navbar-top-section"/>
            <div className="is-flex is-justify-content-space-between is-align-items-center" style={{ height: APP_BAR_HEIGHT_DESKTOP }}>
                <div className="bulma-navbar-start">{props.isLoading ? null : <RenderMenuIcon {...props} />}</div>
                <div className="bulma-navbar-end mr-2">
                    <RouterLink to={`/${props.languageId}`} rel="noopener nofollow" className="is-flex is-align-self-center">
                        <CustomImage
                            base={GET_ICONS_URL}
                            source={props?.logo}
                            title="TomKandula logo"
                            alt="An application logo"
                            width={180}
                            height={30}
                        />
                    </RouterLink>
                </div>
            </div>
        </div>
);
};

export const RenderToolbarSmallScreen = (props: NavigationViewProps): React.ReactElement => (
    <>
        <Media.TabletOnly>
            <RenderDoubleToolbar { ...props} />
        </Media.TabletOnly>
        <Media.MobileOnly>
            <RenderDoubleToolbar { ...props} />
        </Media.MobileOnly>
    </>  
);
