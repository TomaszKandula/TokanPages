import * as React from "react";
import { Avatar } from "@material-ui/core";
import { UserAvatarStyle } from "./userAvatarViewStyle";
import Validate from "validate.js";

interface Properties {
    isLarge: boolean;
    userLetter: string;
    avatarSource: string;
}

export const UserAvatarView = (props: Properties): JSX.Element => {
    const classes = UserAvatarStyle();
    const className = props.isLarge ? classes.avatarLarge : classes.avatarSmall;

    if (Validate.isEmpty(props.avatarSource)) {
        return <Avatar className={className}>{props.userLetter}</Avatar>;
    }

    return <Avatar className={className} src={props.avatarSource} alt="Avatar"></Avatar>;
};
