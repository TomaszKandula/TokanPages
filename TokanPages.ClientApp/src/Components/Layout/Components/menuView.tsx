import * as React from "react";
import { List, ListItem, ListItemIcon, ListItemText } from '@material-ui/core';
import { Collapse, Divider, Drawer } from "@material-ui/core";
import Assignment from "@material-ui/icons/Assignment";
import Build from "@material-ui/icons/Build";
import ContactMail from "@material-ui/icons/ContactMail";
import DirectionsBike from "@material-ui/icons/DirectionsBike";
import ExpandLess from "@material-ui/icons/ExpandLess";
import ExpandMore from "@material-ui/icons/ExpandMore";
import Gavel from "@material-ui/icons/Gavel";
import Home from "@material-ui/icons/Home";
import MusicNote from "@material-ui/icons/MusicNote";
import Person from "@material-ui/icons/Person";
import PersonAdd from "@material-ui/icons/PersonAdd";
import PhotoCamera from "@material-ui/icons/PhotoCamera";
import Policy from "@material-ui/icons/Policy";
import SportsSoccer from "@material-ui/icons/SportsSoccer";
import Star from "@material-ui/icons/Star";
import Subject from "@material-ui/icons/Subject";
import ViewList  from "@material-ui/icons/ViewList";
import menuStyle from "./menuStyle";

interface IBinding
{
    bind: IProperties;
}

interface IProperties
{
    drawerState: { open: boolean };
    closeHandler: any;
    projectsHandler: any;
    projectsState: boolean;
    interestsHandler: any;
    interestState: boolean;
    content: any;// add model
}

export default function MenuView(props: IBinding)
{
    const classes = menuStyle();
    return (
        <Drawer anchor="left" open={props.bind.drawerState.open} onClose={props.bind.closeHandler}>
            <div className={classes.drawerContainer}>
                <img src={props.bind.content.material} className={classes.menuBackground} alt="" />
                <List>
                    <ListItem button key={props.bind.content.login} >
                        <ListItemIcon>
                            <Person />
                        </ListItemIcon>
                        <ListItemText primary={props.bind.content.login} />
                    </ListItem>
                    <ListItem button key={props.bind.content.register} >
                        <ListItemIcon>
                            <PersonAdd />
                        </ListItemIcon>
                        <ListItemText primary={props.bind.content.register} />
                    </ListItem>
                    <Divider variant="middle" />
                    <ListItem button key={props.bind.content.home} >
                        <ListItemIcon>
                            <Home />
                        </ListItemIcon>
                        <ListItemText primary={props.bind.content.home} />
                    </ListItem>
                    <ListItem button key={props.bind.content.articles} >
                        <ListItemIcon>
                            <ViewList />
                        </ListItemIcon>
                        <ListItemText primary={props.bind.content.articles} />
                    </ListItem>
                    <ListItem button key={props.bind.content.story} >
                        <ListItemIcon>
                            <Subject />
                        </ListItemIcon>
                        <ListItemText primary={props.bind.content.story} />
                    </ListItem>

                    <ListItem button key={props.bind.content.projects} onClick={props.bind.projectsHandler} >
                        <ListItemIcon>
                            <Build />
                        </ListItemIcon>
                        <ListItemText primary={props.bind.content.projects} />
                        {props.bind.projectsState ? <ExpandLess /> : <ExpandMore />}
                    </ListItem>
                    <Collapse in={props.bind.projectsState} timeout="auto" unmountOnExit>
                        <List component="div" disablePadding>
                            <ListItem button key={props.bind.content.orion} className={classes.nested} >
                                <ListItemIcon>
                                    <Assignment />
                                </ListItemIcon>
                            <ListItemText primary={props.bind.content.orion} />
                            </ListItem>
                        </List>
                    </Collapse>

                    <ListItem button key={props.bind.content.interests} onClick={props.bind.interestsHandler} >
                        <ListItemIcon>
                            <Star />
                        </ListItemIcon>
                        <ListItemText primary={props.bind.content.interests} />
                        {props.bind.interestState ? <ExpandLess /> : <ExpandMore />}
                    </ListItem>
                    <Collapse in={props.bind.interestState} timeout="auto" unmountOnExit>
                        <List component="div" disablePadding>
                            <ListItem button key={props.bind.content.photography} className={classes.nested} >
                                <ListItemIcon>
                                    <PhotoCamera />
                                </ListItemIcon>
                                <ListItemText primary={props.bind.content.photography} />
                            </ListItem>
                            <ListItem button key={props.bind.content.football} className={classes.nested} >
                                <ListItemIcon>
                                    <SportsSoccer />
                                </ListItemIcon>
                                <ListItemText primary={props.bind.content.football} />
                            </ListItem>
                            <ListItem button key={props.bind.content.guitar} className={classes.nested} >
                                <ListItemIcon>
                                    <MusicNote />
                                </ListItemIcon>
                                <ListItemText primary={props.bind.content.guitar} />
                            </ListItem>
                            <ListItem button key={props.bind.content.bicycle} className={classes.nested} >
                                <ListItemIcon>
                                    <DirectionsBike />
                                </ListItemIcon>
                                <ListItemText primary={props.bind.content.bicycle} />
                            </ListItem>
                            <ListItem button key={props.bind.content.diy} className={classes.nested} >
                                <ListItemIcon>
                                    <Build />
                                </ListItemIcon>
                                <ListItemText primary={props.bind.content.diy} />
                            </ListItem>
                        </List>
                    </Collapse>

                    <ListItem button key={props.bind.content.contact} >
                        <ListItemIcon>
                            <ContactMail />
                        </ListItemIcon>
                        <ListItemText primary={props.bind.content.contact} />
                    </ListItem>
                    <Divider variant="middle" />
                    <ListItem button key={props.bind.content.terms}>
                        <ListItemIcon>
                            <Gavel />
                        </ListItemIcon>
                        <ListItemText primary={props.bind.content.terms} />
                    </ListItem>
                    <ListItem button key={props.bind.content.policy} >
                        <ListItemIcon>
                            <Policy />
                        </ListItemIcon>
                        <ListItemText primary={props.bind.content.policy} />
                    </ListItem>
                </List>
            </div>
        </Drawer>
    );
}
