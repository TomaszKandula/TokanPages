import * as React from "react";
import { Link } from "react-router-dom";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import HideOnScroll from "../../Shared/Scroll/hideOnScroll";
import { ICONS_PATH } from "../../Shared/constants";
import { renderImage } from "../../Shared/Components/renderImage";
import { INavigationContentDto } from "../../Api/Models/";
import useStyles from "./Hooks/styleNavigation";

export default function Navigation(props: { navigation: INavigationContentDto, isLoading: boolean }) 
{
    const classes = useStyles();
    return (
        <HideOnScroll {...props}>
            <AppBar className={classes.appBar}>
                <Toolbar className={classes.toolBar}>
                    <Link to="/" className={classes.mainLink}>
                        <div data-aos="fade-down">
                            {renderImage(ICONS_PATH, props.navigation.content.logo, classes.mainLogo)}
                        </div>
                    </Link>
                </Toolbar>
            </AppBar>
        </HideOnScroll>
    );
}
