import * as React from "react";
import { Link } from "react-router-dom";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import { IGetNavigationContent } from "../../Redux/States/getNavigationContentState";
import HideOnScroll from "../../Shared/Components/Scroll/hideOnScroll";
import { ICONS_PATH } from "../../Shared/constants";
import { renderImage } from "../../Shared/Components/CustomImage/customImage";
import navigationStyle from "./Styles/navigationStyle";

import IconButton from '@material-ui/core/IconButton';
import Avatar from '@material-ui/core/Avatar';

import Drawer from '@material-ui/core/Drawer';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';

import MenuIcon from '@material-ui/icons/Menu';
import { Collapse, Divider, Grid, Typography } from "@material-ui/core";
import { 
    Assignment, 
    Build, 
    ContactMail, 
    DirectionsBike, 
    ExpandLess, 
    ExpandMore, 
    Gavel, 
    Home, 
    MusicNote, 
    Person, 
    PersonAdd, 
    PhotoCamera, 
    Policy, 
    SportsSoccer, 
    Star, 
    ViewList 
} from "@material-ui/icons";

export default function NavigationView(props: IGetNavigationContent) 
{
    const classes = navigationStyle();
    const content = 
    {
        login: 'Login',
        register: 'Register',
        home: 'Home',
        articles: 'Articles',
        orion: 'VAT Validation',
        photography: 'Photography',
        football: 'Football',
        guitar: 'Guitar',
        bicycle: 'Bicycle',
        contact: 'Contact',
        terms: 'Terms of use',
        policy:'Privacy policy',
        avatar: 'https://maindbstorage.blob.core.windows.net/tokanpages/content/avatars/avatar-default-288.jpeg',
        material: 'https://maindbstorage.blob.core.windows.net/tokanpages/content/images/material-background-1.jpg'
    };
    
    const [drawer, setDrawer] = React.useState({ open: false});
    const toggleDrawer = (open: boolean) => (event: any) => 
    {
        if (event.type === "keydown" && (event.key === "Tab" || event.key === "Shift")) return;
        setDrawer({ ...drawer, open });
    };

    const [projects, setProjects] = React.useState(false);
    const handleProjectsClick = () => setProjects(!projects);

    const [interests, setInterests] = React.useState(false);
    const handleInterestsClick = () => setInterests(!interests);

    // TODO: use authorization feature
    const userAlias = "Dummy";
    const anonymous = "Anonymous";
    const isAnonymous = true; 

    return (
        <HideOnScroll {...props}>
            <AppBar className={classes.appBar}>
                <Toolbar className={classes.toolBar}>

                    <Grid container item xs={12} spacing={3}>
                        <Grid item xs className={classes.menu}>
                            <IconButton color="inherit" aria-label="menu" onClick={toggleDrawer(true)}>
                                <MenuIcon />
                            </IconButton>
                        </Grid>
                        <Grid item xs className={classes.link}>
                            <Link to="/">
                                <div data-aos="fade-down" className={classes.image} >
                                    {renderImage(ICONS_PATH, props.content?.logo, classes.logo)}
                                </div>
                            </Link>
                        </Grid>
                        <Grid item xs className={classes.avatar}>
                            <Typography className={classes.userAlias} component={"span"} variant={"body1"} >
                                {isAnonymous ? anonymous : userAlias}
                            </Typography>
                            <IconButton color="inherit">
                                <Avatar alt="Avatar" src={content.avatar} /> 
                            </IconButton>
                        </Grid>
                    </Grid>

                </Toolbar>

                <Drawer anchor="left" open={drawer.open} onClose={toggleDrawer(false)}>
                    <div className={classes.drawerContainer}>
                        <img src={content.material} className={classes.menuBackground} alt="" />
                        <List>
                            <ListItem button key={content.login} >
                                <ListItemIcon>
                                    <Person />
                                </ListItemIcon>
                                <ListItemText primary={content.login} />
                            </ListItem>
                            <ListItem button key={content.register} >
                                <ListItemIcon>
                                    <PersonAdd />
                                </ListItemIcon>
                                <ListItemText primary={content.register} />
                            </ListItem>
                            <Divider variant="middle" />
                            <ListItem button key={content.home} >
                                <ListItemIcon>
                                    <Home />
                                </ListItemIcon>
                                <ListItemText primary={content.home} />
                            </ListItem>
                            <ListItem button key={content.articles} >
                                <ListItemIcon>
                                    <ViewList />
                                </ListItemIcon>
                                <ListItemText primary={content.articles} />
                            </ListItem>

                            <ListItem button key="Projects" onClick={handleProjectsClick} >
                                <ListItemIcon>
                                    <Build />
                                </ListItemIcon>
                                <ListItemText primary="Projects" />
                                {projects ? <ExpandLess /> : <ExpandMore />}
                            </ListItem>
                            <Collapse in={projects} timeout="auto" unmountOnExit>
                                <List component="div" disablePadding>
                                    <ListItem button key={content.orion} className={classes.nested} >
                                        <ListItemIcon>
                                            <Assignment />
                                        </ListItemIcon>
                                    <ListItemText primary={content.orion} />
                                    </ListItem>
                                </List>
                            </Collapse>

                            <ListItem button key="Interests" onClick={handleInterestsClick} >
                                <ListItemIcon>
                                    <Star />
                                </ListItemIcon>
                                <ListItemText primary="Interests" />
                                {interests ? <ExpandLess /> : <ExpandMore />}
                            </ListItem>
                            <Collapse in={interests} timeout="auto" unmountOnExit>
                                <List component="div" disablePadding>
                                    <ListItem button key={content.photography} className={classes.nested} >
                                        <ListItemIcon>
                                            <PhotoCamera />
                                        </ListItemIcon>
                                        <ListItemText primary={content.photography} />
                                    </ListItem>
                                    <ListItem button key={content.football} className={classes.nested} >
                                        <ListItemIcon>
                                            <SportsSoccer />
                                        </ListItemIcon>
                                        <ListItemText primary={content.football} />
                                    </ListItem>
                                    <ListItem button key={content.guitar} className={classes.nested} >
                                        <ListItemIcon>
                                            <MusicNote />
                                        </ListItemIcon>
                                        <ListItemText primary={content.guitar} />
                                    </ListItem>
                                    <ListItem button key={content.bicycle} className={classes.nested} >
                                        <ListItemIcon>
                                            <DirectionsBike />
                                        </ListItemIcon>
                                        <ListItemText primary={content.bicycle} />
                                    </ListItem>
                                </List>
                            </Collapse>

                            <ListItem button key={content.contact} >
                                <ListItemIcon>
                                    <ContactMail />
                                </ListItemIcon>
                                <ListItemText primary={content.contact} />
                            </ListItem>
                            <Divider variant="middle" />
                            <ListItem button key={content.terms}>
                                <ListItemIcon>
                                    <Gavel />
                                </ListItemIcon>
                                <ListItemText primary={content.terms} />
                            </ListItem>
                            <ListItem button key={content.policy} >
                                <ListItemIcon>
                                    <Policy />
                                </ListItemIcon>
                                <ListItemText primary={content.policy} />
                            </ListItem>
                        </List>
                    </div>
                </Drawer>

            </AppBar>
        </HideOnScroll>
    );
}
