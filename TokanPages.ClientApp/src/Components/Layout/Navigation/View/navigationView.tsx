import * as React from "react";
import { Link } from "react-router-dom";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import IconButton from "@material-ui/core/IconButton";
import Avatar from "@material-ui/core/Avatar";
import MenuIcon from "@material-ui/icons/Menu";
import CheckIcon from "@material-ui/icons/Check";
import ArrowBack from "@material-ui/icons/ArrowBack";
import { FormControl, Grid, MenuItem, Select, Box, SelectProps } from "@material-ui/core";
import { LanguageItemDto } from "../../../../Api/Models/";
import { GET_FLAG_URL, GET_ICONS_URL } from "../../../../Api/Request";
import { ApplicationLanguageState } from "../../../../Store/States/";
import { HideOnScroll } from "../../../../Shared/Components/Scroll";
import { Item } from "../../../../Shared/Components/RenderMenu/Models";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { LanguageChangeEvent } from "../../../../Shared/types";
import { RenderImage, RenderNavbarMenu } from "../../../../Shared/Components";
import { SideMenuView } from "./../SideMenu/sideMenuView";
import { NavigationStyle } from "./navigationStyle";
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

const RenderAvatar = (props: BaseProperties): JSX.Element => {
    if (props.isAnonymous) {
        return <Avatar>A</Avatar>;
    }

    if (Validate.isEmpty(props.avatarName)) {
        const userLetter = props.userAliasText?.charAt(0).toUpperCase();
        return <Avatar>{userLetter}</Avatar>;
    }

    return <Avatar alt="Avatar" src={props.avatarSource} />;
};

const RenderAvatarIconButton = (props: BaseProperties): JSX.Element => {
    const classes = NavigationStyle();
    return (
        <div className={classes.user_avatar}>
            <IconButton color="inherit" onClick={props.infoHandler}>
                <RenderAvatar {...props} />
            </IconButton>
        </div>
    );
};

const RenderContent = (props: BaseProperties): JSX.Element => {
    const classes = NavigationStyle();
    return (
        <>
            <Box className={classes.languagesBox}>
                <RenderLanguageSelection
                    {...props}
                    styleControl={classes.languages_control}
                    styleSelect={classes.languages_selection}
                />
            </Box>
            {props.isAnonymous ? <></> : <RenderAvatarIconButton {...props} />}
        </>
    );
};

const RenderMenuIcon = (props: Properties): JSX.Element => {
    const classes = NavigationStyle();
    return (
        <IconButton color="inherit" aria-label="menu" onClick={props.openHandler} className={classes.nav_icon}>
            <MenuIcon />
        </IconButton>
    );
};

const RenderLanguageSelection = (props: Properties): JSX.Element => {
    const classes = NavigationStyle();
    const toUpper = (value?: any): string | undefined => {
        if (value !== undefined) {
            return (value as string).toUpperCase();
        }

        return undefined;
    };

    const renderIcon = (selection: string): JSX.Element | null => {
        if (props.languageId === selection) {
            return <CheckIcon className={classes.languages_check} />;
        }

        return null;
    };

    const renderValue = React.useCallback((value: SelectProps["value"]): React.ReactNode => {
        return (
            <div className={classes.languages_wrapper}>
                {RenderImage(GET_FLAG_URL, `${value}.png`, classes.flag_image)}
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
                        <div className={classes.languages_wrapper}>
                            {RenderImage(GET_FLAG_URL, `${item.id}.png`, classes.flag_image)}
                            <div>{item.name}</div>
                            {renderIcon(item.id)}
                        </div>
                    </MenuItem>
                ))}
            </Select>
        </FormControl>
    );
};

const RenderToolbarLargeScreen = (props: Properties): JSX.Element => {
    const classes = NavigationStyle();
    return (
        <Toolbar className={classes.tool_bar}>
            <Box className={`${classes.nav_menu} ${classes.nav_left}`}>
                <Link to="/" className={classes.app_logo_small}>
                    {RenderImage(GET_ICONS_URL, props?.logoImgName, classes.app_left_logo)}
                </Link>
            </Box>
            <Box className={`${classes.nav_items} ${classes.nav_centre}`}>
                <RenderNavbarMenu isAnonymous={props.isAnonymous} items={props.menu?.items} />
            </Box>
            <Box className={`${classes.nav_items} ${classes.nav_right}`}>
                {props.isLoading ? null : <RenderContent {...props} />}
            </Box>
        </Toolbar>
    );
};

const RenderToolbarSmallScreen = (props: Properties) => {
    const classes = NavigationStyle();
    return (
        <Toolbar className={classes.tool_bar}>
            <Grid container item xs={12} spacing={3}>
                <Grid item xs className={`${classes.nav_menu} ${classes.nav_left}`}>
                    {props.isLoading ? null : <RenderMenuIcon {...props} />}
                </Grid>
                <Grid item xs className={`${classes.nav_items} ${classes.nav_centre}`}>
                    <Link to="/" className={classes.app_logo_small}>
                        {RenderImage(GET_ICONS_URL, props?.logoImgName, classes.app_full_logo)}
                    </Link>
                    <Link to="/" className={classes.app_logo_large}>
                        {RenderImage(GET_ICONS_URL, props?.menu?.image, classes.app_just_logo)}
                    </Link>
                </Grid>
                <Grid item xs className={`${classes.nav_items} ${classes.nav_right}`}>
                    {props.isLoading ? null : <RenderContent {...props} />}
                </Grid>
            </Grid>
        </Toolbar>
    );
};

export const NavigationView = (props: Properties): JSX.Element => {
    const classes = NavigationStyle();
    return (
        <HideOnScroll {...props}>
            {props.backNavigationOnly ? (
                <AppBar className={classes.app_bar} elevation={0}>
                    <div>
                        <Link to={props.backPathFragment ?? "/"}>
                            <IconButton className={classes.nav_back}>
                                <ArrowBack />
                            </IconButton>
                        </Link>
                    </div>
                </AppBar>
            ) : (
                <AppBar className={classes.app_bar} elevation={0}>
                    <div className={classes.nav_large_screen}>
                        <RenderToolbarLargeScreen {...props} />
                    </div>
                    <div className={classes.nav_small_screen}>
                        <RenderToolbarSmallScreen {...props} />
                    </div>
                    <SideMenuView
                        drawerState={props.drawerState}
                        closeHandler={props.closeHandler}
                        isAnonymous={props.isAnonymous}
                        menu={props.menu}
                    />
                </AppBar>
            )}
        </HideOnScroll>
    );
};
