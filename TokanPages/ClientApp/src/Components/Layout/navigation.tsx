import * as React from "react";
import { Link } from "react-router-dom";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import HideOnScroll from "../../Shared/Scroll/hideOnScroll";
import useStyles from "./Hooks/styleNavigation";
import { ICONS_PATH } from "../../Shared/constants";

export interface INavigation
{
    content: any;
}

export default function Navigation(props: INavigation) 
{
    const classes = useStyles();
    const content = 
    {
        logo: "main_logo.svg"
    };

    const mainIcon = ICONS_PATH + content.logo;
    
    return (
        <HideOnScroll {...props}>
            <AppBar className={classes.appBar}>
                <Toolbar className={classes.toolBar}>
                    <Link to="/" className={classes.mainLink}>
                        <img className={classes.mainLogo} src={mainIcon} alt="" />
                    </Link>
                </Toolbar>
            </AppBar>
        </HideOnScroll>
    );
}
