import * as React from "react";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import IconButton from "@material-ui/core/IconButton";
import Avatar from "@material-ui/core/Avatar";
import MenuIcon from "@material-ui/icons/Menu";
import { FormControl, Grid, MenuItem, Select, Typography, Box } from "@material-ui/core";
import { LanguageItemDto } from "../../../../Api/Models/";
import { ApplicationLanguageState } from "../../../../Store/States/";
import { HideOnScroll } from "../../../../Shared/Components/Scroll";
import { Item } from "../../../../Shared/Components/ListRender/Models";
import { ViewProperties } from "../../../../Shared/interfaces";
import { LanguageChangeEvent } from "../../../../Shared/types";
import { SideMenuView } from "./../SideMenu/sideMenuView";
import { NavigationStyle } from "./navigationStyle";
import { v4 as uuidv4 } from "uuid";
import Validate from "validate.js";

interface Properties extends ViewProperties
{
    drawerState: { open: boolean };
    openHandler: (event: any) => void;
    closeHandler: (event: any) => void;
    infoHandler: () => void;
    isAnonymous: boolean;
    avatarName: string;
    avatarSource: string;
    userAliasText: string;
    languages: ApplicationLanguageState;
    languageId: string;
    languageHandler: (event: LanguageChangeEvent) => void;
    menu: { image: string, items: Item[] };
}

interface RenderLanguageSelection extends Properties
{
    styleControl?: string; 
    styleSelect?: string; 
    styleMenu?: string;
}

const RenderMenuIcon = (props: Properties): JSX.Element => 
{
    const classes = NavigationStyle();
    return(
        <IconButton color="inherit" aria-label="menu" 
            onClick={props.openHandler} className={classes.nav_icon}>
            <MenuIcon />
        </IconButton>
    );
}

const RenderAvatar = (props: Properties): JSX.Element => 
{
    if (props.isAnonymous)
    {
        return(<Avatar>A</Avatar>);
    }

    if (Validate.isEmpty(props.avatarName))
    {
        const userLetter = props.userAliasText?.charAt(0).toUpperCase();
        return(<Avatar>{userLetter}</Avatar>);
    }

    return(<Avatar alt="Avatar" src={props.avatarSource} />);
}

const RenderLanguageSelection = (props: RenderLanguageSelection): JSX.Element => 
{
    return(
        <FormControl className={props.styleControl}>
            <Select value={props.languageId} onChange={props.languageHandler} disableUnderline className={props.styleSelect}>
                {props.languages?.languages.map((item: LanguageItemDto, _index: number) => (
                    <MenuItem value={item.id} key={uuidv4()} className={props.styleMenu}>
                        {item.name}
                    </MenuItem>
                ))}
            </Select>
        </FormControl>
    );
}

const RenderContent = (props: Properties): JSX.Element => 
{
    const classes = NavigationStyle();
    return(
        <>
            <Box className={classes.languagesBox}>
                <RenderLanguageSelection 
                    {...props} 
                    styleSelect={classes.languages_selection} 
                    styleMenu={classes.languages_menu}
                />
            </Box>
            <div className={classes.user_avatar}>
                <IconButton color="inherit" onClick={props.infoHandler} >
                    <RenderAvatar {...props} /> 
                </IconButton>
            </div>
        </>
    );
}

export const NavigationView = (props: Properties): JSX.Element => 
{
    const classes = NavigationStyle();
    const fullName = "</> tom kandula";
    const justLogo = "</>";
    return (
        <HideOnScroll {...props}>
            <AppBar className={classes.app_bar}>
                <Toolbar className={classes.tool_bar}>

                    <Grid container item xs={12} spacing={3}>
                        <Grid item xs className={classes.nav_menu}>
                            {props.isLoading ? null : <RenderMenuIcon {...props} />}
                        </Grid>
                        <Grid item xs className={classes.app_link}>
                            <Typography className={classes.app_full_logo}>{fullName}</Typography>
                            <Typography className={classes.app_just_logo}>{justLogo}</Typography>
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
}
