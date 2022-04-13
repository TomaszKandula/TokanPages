import * as React from "react";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import IconButton from '@material-ui/core/IconButton';
import Avatar from '@material-ui/core/Avatar';
import MenuIcon from '@material-ui/icons/Menu';
import { FormControl, Grid, MenuItem, Select, Typography, Box } from "@material-ui/core";
import HideOnScroll from "../../Shared/Components/Scroll/hideOnScroll";
import { IItem } from "../../Shared/Components/ListRender/Models/item";
import { ILanguage } from "../../Shared/Services/languageService";
import MenuView from "./Components/menuView";
import navigationStyle from "./Styles/navigationStyle";

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
    avatar: string;
    anonymousText: string;
    userAliasText: string;
    languages: ILanguage[];
    selectedLanguage: string;
    languageHandler: any;
    menu: { image: string, items: IItem[] };
}

const NavigationView = (props: IBinding): JSX.Element => 
{
    const classes = navigationStyle();
    const fullName = "</> tom kandula";
    const justLogo = "</>";

    const LanguageSelection = (args: { styleControl?: string, styleSelect?: string, styleMenu?: string }): JSX.Element => 
    {
        return(
            <FormControl className={args.styleControl}>
                <Select value={props.bind?.selectedLanguage} onChange={props.bind?.languageHandler} disableUnderline className={args.styleSelect}>
                    {props.bind?.languages.map((item: ILanguage, index: number) => (
                        <MenuItem value={item.id} key={index} className={args.styleMenu}>
                            {item.name}
                        </MenuItem>
                    ))}
                </Select>
            </FormControl>
        );
    }

    return (
        <HideOnScroll {...props}>
            <AppBar className={classes.app_bar}>
                <Toolbar className={classes.tool_bar}>

                    <Grid container item xs={12} spacing={3}>
                        <Grid item xs className={classes.nav_menu}>
                            <IconButton color="inherit" aria-label="menu" onClick={props.bind?.openHandler} className={classes.nav_icon}>
                                <MenuIcon />
                            </IconButton>
                        </Grid>
                        <Grid item xs className={classes.app_link}>
                            <Typography className={classes.app_full_logo}>{fullName}</Typography>
                            <Typography className={classes.app_just_logo}>{justLogo}</Typography>
                        </Grid>
                        <Grid item xs className={classes.content_right_side}>
                            <Box className={classes.languagesBox}>
                                {props.bind?.isLoading ? null : <LanguageSelection 
                                    styleSelect={classes.languages_selection} 
                                    styleMenu={classes.languages_menu} />}
                            </Box>
                            <div className={classes.user_avatar}>
                                <Typography className={classes.user_alias}>
                                    {props.bind?.isAnonymous ? props.bind?.anonymousText : props.bind?.userAliasText}
                                </Typography>
                                <IconButton color="inherit" onClick={props.bind?.infoHandler} >
                                    <Avatar alt="Avatar" src={props.bind?.avatar} /> 
                                </IconButton>
                            </div>
                        </Grid>
                    </Grid>

                </Toolbar>

                <MenuView bind=
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

export default NavigationView;
