import React from "react";
import Icon from "@material-ui/core/Icon";
import { MEDIUM_ICON } from "../../Shared/constants";
import useStyles from "./stylesIcon";

export const MediumIcon = () => 
{
    const classes = useStyles();
    return (
        <Icon classes={{root: classes.iconRoot}} color="primary">
            <img className={classes.imageIcon} src={MEDIUM_ICON} alt="Medium"/>
        </Icon>
    );
}
