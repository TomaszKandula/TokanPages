import * as React from "react";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import IconButton from "@material-ui/core/IconButton";
import Avatar from "@material-ui/core/Avatar";
import MenuIcon from "@material-ui/icons/Menu";
import { FormControl, Grid, MenuItem, Select, Box } from "@material-ui/core";
import { LanguageItemDto } from "../../../../Api/Models/";
import { GET_FLAG_URL, GET_ICONS_URL } from "../../../../Api/Request";
import { ApplicationLanguageState } from "../../../../Store/States/";
import { HideOnScroll } from "../../../../Shared/Components/Scroll";
import { Item } from "../../../../Shared/Components/ListRender/Models";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { LanguageChangeEvent } from "../../../../Shared/types";
import { RenderImage } from "../../../../Shared/Components";
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

const RenderContent = (props: BaseProperties): JSX.Element => {
    const classes = NavigationStyle();
    const RenderAvatarIconButton = (): JSX.Element => {
        return (
            <div className={classes.user_avatar}>
                <IconButton color="inherit" onClick={props.infoHandler}>
                    <RenderAvatar {...props} />
                </IconButton>
            </div>
        );
    };

    return (
        <>
            <Box className={classes.languagesBox}>
                <RenderLanguageSelection
                    {...props}
                    styleControl={classes.languages_control}
                    styleSelect={classes.languages_selection}
                />
            </Box>
            {props.isAnonymous ? <></> : <RenderAvatarIconButton />}
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

    return (
        <FormControl className={props.styleControl}>
            <Select
                displayEmpty
                disableUnderline
                value={props.languageId}
                onChange={props.languageHandler}
                className={props.styleSelect}
                renderValue={value => (
                    <div style={{ display: "flex", alignItems: "center" }}>
                        {RenderImage(GET_FLAG_URL, `${value}.png`, classes.flag_image)}
                        <div>{toUpper(value)}</div>
                    </div>
                )}
            >
                {props.languages?.languages.map((item: LanguageItemDto, _index: number) => (
                    <MenuItem value={item.id} key={uuidv4()} className={props.styleMenu}>
                        <div style={{ display: "flex", alignItems: "center" }}>
                            {RenderImage(GET_FLAG_URL, `${item.id}.png`, classes.flag_image)}
                            <div>{item.name}</div>
                        </div>
                    </MenuItem>
                ))}
            </Select>
        </FormControl>
    );
};

export const NavigationView = (props: Properties): JSX.Element => {
    const classes = NavigationStyle();
    return (
        <HideOnScroll {...props}>
            <AppBar className={classes.app_bar} elevation={0}>
                <Toolbar className={classes.tool_bar}>
                    <Grid container item xs={12} spacing={3}>
                        <Grid item xs className={classes.nav_menu}>
                            {props.isLoading ? null : <RenderMenuIcon {...props} />}
                        </Grid>
                        <Grid item xs className={classes.content_right_side}>
                            {RenderImage(GET_ICONS_URL, props.logoImgName, classes.app_full_logo)}
                            {RenderImage(GET_ICONS_URL, props.menu.image, classes.app_just_logo)}
                        </Grid>
                        <Grid item xs className={classes.content_right_side}>
                            {props.isLoading ? null : <RenderContent {...props} />}
                        </Grid>
                    </Grid>
                </Toolbar>
                <SideMenuView
                    drawerState={props.drawerState}
                    closeHandler={props.closeHandler}
                    isAnonymous={props.isAnonymous}
                    menu={props.menu}
                />
            </AppBar>
        </HideOnScroll>
    );
};
