import * as React from "react";
import { Link } from "react-router-dom";
import Icon from "@mdi/react";
import { mdiMenu, mdiCheck, mdiArrowLeft } from "@mdi/js";
import { LanguageItemDto } from "../../../../Api/Models/";
import { GET_FLAG_URL, GET_ICONS_URL } from "../../../../Api";
import { ApplicationLanguageState } from "../../../../Store/States/";
import { Item } from "../../../../Shared/Components/RenderMenu/Models";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { APP_BAR_HEIGHT } from "../../../../Shared/constants";
import { AppBar, Avatar, IconButton, RenderImage, RenderNavbarMenu } from "../../../../Shared/Components";
import { SideMenuView } from "./../SideMenu/sideMenuView";
import { v4 as uuidv4 } from "uuid";
import Validate from "validate.js";
import "./navigationView.css";

interface BaseProperties extends ViewProperties {
    drawerState: { open: boolean };
    openHandler: (event: any) => void;
    closeHandler: (event: any) => void;
    infoHandler: () => void;
    isAnonymous: boolean;
    avatarName: string;
    avatarSource: string;
    userAliasText: string;
    logoImgName: string;
    languages: ApplicationLanguageState;
    languageId: string;
    languageHandler: (id: string) => void;
    menu: { image: string; items: Item[] };
    backNavigationOnly?: boolean;
    backPathFragment?: string;
}

interface Properties extends BaseProperties {
    height?: number;
}

const RenderAvatar = (props: BaseProperties): React.ReactElement => {
    if (props.isAnonymous) {
        return (
            <Avatar alt="User avatar" title="Avatar">
                A
            </Avatar>
        );
    }

    if (Validate.isEmpty(props.avatarName)) {
        const userLetter = props.userAliasText?.charAt(0).toUpperCase();
        return (
            <Avatar alt="User avatar" title="Avatar">
                {userLetter}
            </Avatar>
        );
    }

    return (
        <Avatar alt="User avatar" title="Avatar" src={props.avatarSource} />
    );
};

const RenderAvatarIconButton = (props: BaseProperties): React.ReactElement => {
    return (
        <div className="navigation-user-avatar">
            <IconButton onClick={props.infoHandler}>
                <RenderAvatar {...props} />
            </IconButton>
        </div>
    );
};

const RenderContent = (props: BaseProperties): React.ReactElement => {
    return (
        <>
            <div className="navigation-languages-box">
                <RenderLanguageSelection {...props} />
            </div>
            {props.isAnonymous ? <></> : <RenderAvatarIconButton {...props} />}
        </>
    );
};

const RenderMenuIcon = (props: Properties): React.ReactElement => {
    return (
        <IconButton aria-label="menu" onClick={props.openHandler}>
            <Icon path={mdiMenu} size={1} />
        </IconButton>
    );
};

const RenderLanguageSelection = (props: Properties): React.ReactElement => {
    const toUpper = (value?: any): string | undefined => {
        if (value !== undefined) {
            return (value as string).toUpperCase();
        }

        return undefined;
    };

    const renderIcon = (selection: string): React.ReactElement | null => {
        if (props.languageId === selection) {
            return <Icon path={mdiCheck} size={1.5} className="navigation-languages-check" />;
        }

        return null;
    };

    return (
        <div className="bulma-navbar-menu">
        <div className="bulma-navbar-item bulma-has-dropdown bulma-is-hoverable">
            <a className="bulma-navbar-link">
                <RenderImage
                    base={GET_FLAG_URL}
                    source={`${props.languageId}.png`}
                    title="Language flag"
                    alt={`A flag (${props.languageId}) for current language selection`}
                    className="navigation-flag-image"
                />
                <div>{toUpper(props.languageId)}</div>
            </a>
            <div className="bulma-navbar-dropdown">
                {props.languages?.languages.map((item: LanguageItemDto, _index: number) => (
                    <a className="bulma-navbar-item" key={uuidv4()} onClick={() => props.languageHandler(item.id)}>
                        <RenderImage
                            base={GET_FLAG_URL}
                            source={`${item.id}.png`}
                            title="Language flag"
                            alt={`A flag (${item.name}) symbolizing available language`}
                            className="navigation-flag-image"
                        />
                        <div>{item.name}</div>
                        {renderIcon(item.id)}
                    </a>
                ))}
            </div>
        </div>
        </div>
    );
};

const RenderToolbarLargeScreen = (props: Properties): React.ReactElement => (
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
            {props.isLoading ? null : <RenderContent {...props} />}
        </div>
    </div>
);

const RenderToolbarSmallScreen = (props: Properties) => (
    <div className="navigation-tool-bar" style={{ height: props.height }}>
        <div className="navigation-nav-menu navigation-nav-left">
            {props.isLoading ? null : <RenderMenuIcon {...props} />}
        </div>
        <div className="navigation-nav-items navigation-nav-centre">
            <Link to={`/${props.languageId}`} className="navigation-app-logo-small" rel="noopener nofollow">
                <RenderImage
                    base={GET_ICONS_URL}
                    source={props?.logoImgName}
                    title="TomKandula logo"
                    alt="An application logo"
                    className="navigation-app-full-logo"
                />
            </Link>
            <Link to={`/${props.languageId}`} className="navigation-app-logo-large" rel="noopener nofollow">
                <RenderImage
                    base={GET_ICONS_URL}
                    source={props?.menu?.image}
                    title="TomKandula logo"
                    alt="An application logo"
                    className="navigation-app-just-logo"
                />
            </Link>
        </div>
        <div className="navigation-nav-items navigation-nav-right">
            {props.isLoading ? null : <RenderContent {...props} />}
        </div>
    </div>
);

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
