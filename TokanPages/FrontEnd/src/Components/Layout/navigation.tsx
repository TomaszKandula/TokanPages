import * as React from "react";
import { Link } from "react-router-dom";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import HideOnScroll from "../../Shared/Scroll/hideOnScroll";
import useStyles from "./Hooks/styleNavigation";
import { IMG_LOGO } from "../../Shared/constants";

export default function Navigation(props: { content: any; }) 
{

    const classes = useStyles();

    return (
        <HideOnScroll {...props}>
            <AppBar className={classes.appBar}>
                <Toolbar className={classes.toolBar}>
                    <Link to="/" className={classes.mainLink}>
                        <img className={classes.mainLogo} src={IMG_LOGO} alt="" />
                    </Link>
                </Toolbar>
            </AppBar>
        </HideOnScroll>
    );

}
