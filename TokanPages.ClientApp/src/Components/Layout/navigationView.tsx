import * as React from "react";
import { Link } from "react-router-dom";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import IconButton from '@material-ui/core/IconButton';
import Avatar from '@material-ui/core/Avatar';
import MenuIcon from '@material-ui/icons/Menu';
import { Grid, Typography } from "@material-ui/core";
import HideOnScroll from "../../Shared/Components/Scroll/hideOnScroll";
import { ICONS_PATH } from "../../Shared/constants";
import { renderImage } from "../../Shared/Components/CustomImage/customImage";
import { IItem } from "../../Shared/Components/ListRender/Models/item";
import MenuView from "./Components/menuView";
import navigationStyle from "./Styles/navigationStyle";

interface IBinding
{
    bind: IProperties;
}

interface IProperties
{
    drawerState: { open: boolean };
    openHandler: any;
    closeHandler: any;
    isAnonymous: boolean;
    logo: string;
    avatar: string;
    anonymousText: string;
    userAliasText: string;
    menu: { image: string, items: IItem[] };
}

const NavigationView = (props: IBinding): JSX.Element => 
{
    const classes = navigationStyle();
    return (
        <HideOnScroll {...props}>
            <AppBar className={classes.appBar}>
                <Toolbar className={classes.toolBar}>

                    <Grid container item xs={12} spacing={3}>
                        <Grid item xs className={classes.menu}>
                            <IconButton color="inherit" aria-label="menu" onClick={props.bind.openHandler}>
                                <MenuIcon />
                            </IconButton>
                        </Grid>
                        <Grid item xs className={classes.link}>
                            <Link to="/">
                                <div data-aos="fade-down" className={classes.image} >
                                    {renderImage(ICONS_PATH, props.bind.logo, classes.logo)}
                                </div>
                            </Link>
                        </Grid>
                        <Grid item xs className={classes.avatar}>
                            <Typography className={classes.userAlias} component={"span"} variant={"body1"} >
                                {props.bind.isAnonymous ? props.bind.anonymousText : props.bind.userAliasText}
                            </Typography>
                            <IconButton color="inherit">
                                <Avatar alt="Avatar" src={props.bind.avatar} /> 
                            </IconButton>
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
