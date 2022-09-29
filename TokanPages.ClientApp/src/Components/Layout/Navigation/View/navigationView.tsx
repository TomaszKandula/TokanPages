import * as React from "react";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import IconButton from '@material-ui/core/IconButton';
import Avatar from '@material-ui/core/Avatar';
import MenuIcon from '@material-ui/icons/Menu';
import { FormControl, Grid, MenuItem, Select, Typography, Box } from "@material-ui/core";
import { ILanguageItem } from "../../../../Api/Models/";
import { IUserLanguage } from "../../../../Store/States/userLanguageState";
import { HideOnScroll } from "../../../../Shared/Components/Scroll";
import { IItem } from "../../../../Shared/Components/ListRender/Models";
import { AVATARS_PATH } from "../../../../Shared/constants";
import { SideMenuView } from "./../SideMenu/sideMenuView";
import { NavigationStyle } from "./navigationStyle";
import Validate from "validate.js";

interface IBinding
{
    bind: IProperties;
}

interface IProperties
{
    isLoading: boolean;
    drawerState: { open: boolean };
    openHandler: any;
    closeHandler: any;
    infoHandler: any;
    isAnonymous: boolean;
    logo: string;
    avatarName: string;
    userAliasText: string;
    languages: IUserLanguage;
    languageId: string;
    languageHandler: any;
    menu: { image: string, items: IItem[] };
}

export const NavigationView = (props: IBinding): JSX.Element => 
{
    const classes = NavigationStyle();
    const fullName = "</> tom kandula";
    const justLogo = "</>";

    const RenderLanguageSelection = (args: { styleControl?: string, styleSelect?: string, styleMenu?: string }): JSX.Element => 
    {
        return(
            <FormControl className={args.styleControl}>
                <Select value={props.bind?.languageId} onChange={props.bind?.languageHandler} disableUnderline className={args.styleSelect}>
                    {props.bind?.languages?.languages.map((item: ILanguageItem, index: number) => (
                        <MenuItem value={item.id} key={index} className={args.styleMenu}>
                            {item.name}
                        </MenuItem>
                    ))}
                </Select>
            </FormControl>
        );
    }

    const RenderMenuIcon = (): JSX.Element => 
    {
        return(
            <IconButton color="inherit" aria-label="menu" 
                onClick={props.bind?.openHandler} className={classes.nav_icon}>
                <MenuIcon />
            </IconButton>
        );
    }

    const RenderAvatar = (): JSX.Element => 
    {
        if (props.bind?.isAnonymous)
        {
            return(<Avatar>A</Avatar>);
        }

        if (Validate.isEmpty(props.bind?.avatarName))
        {
            const userLetter = props.bind?.userAliasText?.charAt(0).toUpperCase();
            return(<Avatar>{userLetter}</Avatar>);
        }

        const avatarName = `${AVATARS_PATH}${props.bind?.avatarName}`;
        return(<Avatar alt="Avatar" src={avatarName} />);
    }

    const RenderContent = (): JSX.Element => 
    {
        return(
            <>
                <Box className={classes.languagesBox}>
                    <RenderLanguageSelection 
                        styleSelect={classes.languages_selection} 
                        styleMenu={classes.languages_menu} />
                </Box>
                <div className={classes.user_avatar}>
                    <IconButton color="inherit" onClick={props.bind?.infoHandler} >
                        <RenderAvatar /> 
                    </IconButton>
                </div>
            </>
        );
    }

    return (
        <HideOnScroll {...props}>
            <AppBar className={classes.app_bar}>
                <Toolbar className={classes.tool_bar}>

                    <Grid container item xs={12} spacing={3}>
                        <Grid item xs className={classes.nav_menu}>
                            {props.bind?.isLoading ? null : <RenderMenuIcon />}
                        </Grid>
                        <Grid item xs className={classes.app_link}>
                            <Typography className={classes.app_full_logo}>{fullName}</Typography>
                            <Typography className={classes.app_just_logo}>{justLogo}</Typography>
                        </Grid>
                        <Grid item xs className={classes.content_right_side}>
                            {props.bind?.isLoading ? null : <RenderContent />}
                        </Grid>
                    </Grid>

                </Toolbar>

                <SideMenuView bind=
                {{
                    drawerState: props.bind.drawerState,
                    closeHandler: props.bind.closeHandler,
                    isAnonymous: props.bind.isAnonymous,
                    menu: props.bind.menu
                }}/>

            </AppBar>
        </HideOnScroll>
    );
}
