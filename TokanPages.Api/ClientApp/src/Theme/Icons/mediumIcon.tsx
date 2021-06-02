import React from "react";
import Icon from "@material-ui/core/Icon";
import { MEDIUM_ICON } from "../../Shared/constants";
import mediumIconStyle from "./mediumIconStyle";

export const MediumIcon = () => 
{
    const classes = mediumIconStyle();
    return (
        <Icon classes={{root: classes.iconRoot}} color="primary">
            <img className={classes.imageIcon} src={MEDIUM_ICON} alt="Medium"/>
        </Icon>
    );
}
