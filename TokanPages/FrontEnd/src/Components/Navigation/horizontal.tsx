import React from "react";
import { Link } from "react-router-dom";
import { makeStyles } from "@material-ui/core/styles";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import HideOnScroll from "../../Shared/Scroll/hideOnScroll";

const useStyles = makeStyles(() => (
{
    appBar:
    {
        background: "#1976D2"
    },
    toolBar: 
    { 
        justifyContent: "center", 
    },
    mainLogo:
    {
        width: 210,
    },
    mainLink:
    {
        marginTop: "10px",
        variant:"h5", 
        color: "inherit", 
        underline: "none"
    }
}));

export default function HorizontalNav(props: { content: any; }) 
{

    const classes = useStyles();
    const imageUrl = "https://maindbstorage.blob.core.windows.net/tokanpages/icons/main_logo.svg";

    return (

        <HideOnScroll {...props}>
            <AppBar className={classes.appBar}>
                <Toolbar className={classes.toolBar}>
                    <Link to="/" className={classes.mainLink}>
                        <img className={classes.mainLogo} src={imageUrl} alt="" />
                    </Link>
                </Toolbar>
            </AppBar>
        </HideOnScroll>

    );

}
