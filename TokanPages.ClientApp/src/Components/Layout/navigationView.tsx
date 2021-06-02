import * as React from "react";
import { Link } from "react-router-dom";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import { IGetNavigationContent } from "../../Redux/States/getNavigationContentState";
import HideOnScroll from "../../Shared/Components/Scroll/hideOnScroll";
import { ICONS_PATH } from "../../Shared/constants";
import { renderImage } from "../../Shared/Components/CustomImage/customImage";
import navigationStyle from "./Styles/navigationStyle";

export default function NavigationView(props: IGetNavigationContent) 
{
    const classes = navigationStyle();
    return (
        <HideOnScroll {...props}>
            <AppBar className={classes.appBar}>
                <Toolbar className={classes.toolBar}>
                    <Link to="/" className={classes.mainLink}>
                        <div data-aos="fade-down">
                            {renderImage(ICONS_PATH, props.content?.logo, classes.mainLogo)}
                        </div>
                    </Link>
                </Toolbar>
            </AppBar>
        </HideOnScroll>
    );
}
