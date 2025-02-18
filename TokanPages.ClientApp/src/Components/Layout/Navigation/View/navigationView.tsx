import * as React from "react";
import { Link } from "react-router-dom";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import IconButton from "@material-ui/core/IconButton";
import Avatar from "@material-ui/core/Avatar";
import MenuIcon from "@material-ui/icons/Menu";
import CheckIcon from "@material-ui/icons/Check";
import ArrowBack from "@material-ui/icons/ArrowBack";
import { FormControl, Grid, MenuItem, Select, SelectProps } from "@material-ui/core";
import { LanguageItemDto } from "../../../../Api/Models/";
import { GET_FLAG_URL, GET_ICONS_URL } from "../../../../Api/Request";
import { ApplicationLanguageState } from "../../../../Store/States/";
import { HideOnScroll } from "../../../../Shared/Components/Scroll";
import { Item } from "../../../../Shared/Components/RenderMenu/Models";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { LanguageChangeEvent } from "../../../../Shared/types";
import { RenderImage, RenderNavbarMenu } from "../../../../Shared/Components";
import { SideMenuView } from "./../SideMenu/sideMenuView";
import { v4 as uuidv4 } from "uuid";
import Validate from "validate.js";

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
    languageHandler: (event: LanguageChangeEvent) => void;
    menu: { image: string; items: Item[] };
    backNavigationOnly?: boolean;
    backPathFragment?: string;
}

interface Properties extends BaseProperties {
    styleControl?: string;
    styleSelect?: string;
    styleMenu?: string;
}

const RenderAvatar = (props: BaseProperties): React.ReactElement => {
    if (props.isAnonymous) {
        return <Avatar>A</Avatar>;
    }

    if (Validate.isEmpty(props.avatarName)) {
        const userLetter = props.userAliasText?.charAt(0).toUpperCase();
        return <Avatar>{userLetter}</Avatar>;
    }

    return <Avatar alt="User avatar" src={props.avatarSource} />;
};

const RenderAvatarIconButton = (props: BaseProperties): React.ReactElement => {
    return (
        <div className="navigation-user-avatar">
            <IconButton color="inherit" onClick={props.infoHandler}>
                <RenderAvatar {...props} />
            </IconButton>
        </div>
    );
};

const RenderContent = (props: BaseProperties): React.ReactElement => {
    return (
        <>
            <div className="navigation-languages-box">
                <RenderLanguageSelection {...props} styleSelect="navigation-languages-selection" />
            </div>
            {props.isAnonymous ? <></> : <RenderAvatarIconButton {...props} />}
        </>
    );
};

const RenderMenuIcon = (props: Properties): React.ReactElement => {
    return (
        <IconButton color="inherit" aria-label="menu" onClick={props.openHandler} className="navigation-nav-icon">
            <MenuIcon />
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
            return <CheckIcon className="navigation-languages-check" />;
        }

        return null;
    };

    const renderValue = React.useCallback((value: SelectProps["value"]): React.ReactNode => {
        return (
            <div className="navigation-languages-wrapper">
                <RenderImage 
                    base={GET_FLAG_URL} 
                    source={`${value}.png`} 
                    alt={`A flag (${value}) for current language selection`}
                    className="navigation-flag-image"
                />
                <div>{toUpper(value)}</div>
            </div>
        );
    }, []);

    return (
        <FormControl className={props.styleControl}>
            <Select
                displayEmpty
                disableUnderline
                value={props.languageId}
                onChange={props.languageHandler}
                className={props.styleSelect}
                renderValue={renderValue}
            >
                {props.languages?.languages.map((item: LanguageItemDto, _index: number) => (
                    <MenuItem value={item.id} key={uuidv4()} className={props.styleMenu}>
                        <div className="navigation-languages-wrapper">
                            <RenderImage
                                base={GET_FLAG_URL}
                                source={`${item.id}.png`}
                                alt={`A flag (${item.name}) symbolizing available language`}
                                className="navigation-flag-image"
                            />
                            <div>{item.name}</div>
                            {renderIcon(item.id)}
                        </div>
                    </MenuItem>
                ))}
            </Select>
        </FormControl>
    );
};

const RenderToolbarLargeScreen = (props: Properties): React.ReactElement => {
    return (
        <Toolbar className="navigation-tool-bar">
            <div className="navigation-nav-menu navigation-nav-left">
                <Link to={`/${props.languageId}`} className="navigation-app-logo-small">
                    <RenderImage
                        base={GET_ICONS_URL}
                        source={props?.logoImgName}
                        alt="An application logo"
                        className="navigation-app-left-logo"
                    />
                </Link>
            </div>
            <div className="navigation-nav-items navigation-nav-centre">
                <RenderNavbarMenu
                    isAnonymous={props.isAnonymous}
                    languageId={props.languageId}
                    items={props.menu?.items}
                />
            </div>
            <div className="navigation-nav-items navigation-nav-right">
                {props.isLoading ? null : <RenderContent {...props} />}
            </div>
        </Toolbar>
    );
};

const RenderToolbarSmallScreen = (props: Properties) => {
    return (
        <Toolbar className="navigation-tool-bar">
            <Grid container item xs={12} spacing={3}>
                <Grid item xs className="navigation-nav-menu navigation-nav-left">
                    {props.isLoading ? null : <RenderMenuIcon {...props} />}
                </Grid>
                <Grid item xs className="navigation-nav-items navigation-nav-centre">
                    <Link to={`/${props.languageId}`} className="navigation-app-logo-small">
                        <RenderImage
                            base={GET_ICONS_URL}
                            source={props?.logoImgName}
                            alt="An application logo"
                            className="navigation-app-full-logo"
                        />
                    </Link>
                    <Link to={`/${props.languageId}`} className="navigation-app-logo-large">
                        <RenderImage
                            base={GET_ICONS_URL}
                            source={props?.menu?.image}
                            alt="An application logo"
                            className="navigation-app-just-logo"
                        />
                    </Link>
                </Grid>
                <Grid item xs className="navigation-nav-items navigation-nav-right">
                    {props.isLoading ? null : <RenderContent {...props} />}
                </Grid>
            </Grid>
        </Toolbar>
    );
};

export const NavigationView = (props: Properties): React.ReactElement => {
    const mainPath = `/${props.languageId}`;
    const navigationPath = props.backPathFragment === undefined ? mainPath : `${mainPath}${props.backPathFragment}`;
    return (
        <HideOnScroll {...props}>
            {props.backNavigationOnly ? (
                <AppBar className="navigation-app-bar" elevation={0}>
                    <div>
                        <Link to={navigationPath}>
                            <IconButton className="navigation-nav-back">
                                <ArrowBack />
                            </IconButton>
                        </Link>
                    </div>
                </AppBar>
            ) : (
                <AppBar className="navigation-app-bar" elevation={0}>
                    <div className="navigation-nav-large-screen">
                        <RenderToolbarLargeScreen {...props} />
                    </div>
                    <div className="navigation-nav-small-screen">
                        <RenderToolbarSmallScreen {...props} />
                    </div>
                    <SideMenuView
                        drawerState={props.drawerState}
                        closeHandler={props.closeHandler}
                        isAnonymous={props.isAnonymous}
                        languageId={props.languageId}
                        menu={props.menu}
                    />
                </AppBar>
            )}
        </HideOnScroll>
    );
};
