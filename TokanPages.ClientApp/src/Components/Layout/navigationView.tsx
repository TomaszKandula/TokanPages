import * as React from "react";
import { Link } from "react-router-dom";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import HideOnScroll from "../../Shared/Components/Scroll/hideOnScroll";
import { ICONS_PATH } from "../../Shared/constants";
import { renderImage } from "../../Shared/Components/CustomImage/customImage";
import navigationStyle from "./Styles/navigationStyle";
import IconButton from '@material-ui/core/IconButton';
import Avatar from '@material-ui/core/Avatar';
import MenuIcon from '@material-ui/icons/Menu';
import { Grid, Typography } from "@material-ui/core";
import MenuView from "./Components/menuView";

interface IBinding
{
    bind: IProperties;
}

interface IProperties
{
    drawerState: { open: boolean };
    openHandler: any;
    closeHandler: any;
    projectsHandler: any;
    projectsState: boolean;
    interestsHandler: any;
    interestState: boolean;
    isAnonymous: boolean;
    anonymous: string;
    userAlias: string;
    content: any;//add model
}

export default function NavigationView(props: IBinding) 
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
                                    {renderImage(ICONS_PATH, props.bind.content?.logo, classes.logo)}
                                </div>
                            </Link>
                        </Grid>
                        <Grid item xs className={classes.avatar}>
                            <Typography className={classes.userAlias} component={"span"} variant={"body1"} >
                                {props.bind.isAnonymous ? props.bind.anonymous : props.bind.userAlias}
                            </Typography>
                            <IconButton color="inherit">
                                <Avatar alt="Avatar" src={props.bind.content.avatar} /> 
                            </IconButton>
                        </Grid>
                    </Grid>

                </Toolbar>

                <MenuView bind=
                {{
                    drawerState: props.bind.drawerState,
                    closeHandler: props.bind.closeHandler,
                    projectsHandler: props.bind.projectsHandler,
                    projectsState: props.bind.projectsState,
                    interestsHandler: props.bind.interestsHandler,
                    interestState: props.bind.interestState,
                    content: props.bind.content
                }}/>

            </AppBar>
        </HideOnScroll>
    );
}
