import * as React from "react";
import { Link } from "react-router-dom";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import HideOnScroll from "../../Shared/Scroll/hideOnScroll";
import useStyles from "./Hooks/styleNavigation";

export default function Navigation(props: { content: any; }) 
{

    const classes = useStyles();
    const content = 
    {
        mainLogo: "https://maindbstorage.blob.core.windows.net/tokanpages/icons/main_logo.svg"
    };

    return (
        <HideOnScroll {...props}>
            <AppBar className={classes.appBar}>
                <Toolbar className={classes.toolBar}>
                    <Link to="/" className={classes.mainLink}>
                        <img className={classes.mainLogo} src={content.mainLogo} alt="" />
                    </Link>
                </Toolbar>
            </AppBar>
        </HideOnScroll>
    );

}
