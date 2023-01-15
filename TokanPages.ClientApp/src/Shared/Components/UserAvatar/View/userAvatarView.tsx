import * as React from "react";
import { Avatar } from "@material-ui/core";
import { UserAvatarStyle } from "./userAvatarViewStyle";
import Validate from "validate.js";

interface IBinding
{
    bind: IProperties;
}

interface IProperties
{
    isLarge: boolean;
    userLetter: string;
    avatarSource: string;
}

export const UserAvatarView = (props: IBinding): JSX.Element =>
{
    const classes = UserAvatarStyle();
    const className = props.bind?.isLarge ? classes.avatarLarge : classes.avatarSmall;

    if (Validate.isEmpty(props.bind?.avatarSource))
    {
        return(<Avatar className={className}>{props.bind?.userLetter}</Avatar>);
    }

    return(
        <Avatar 
            className={className} 
            src={props.bind?.avatarSource}
            alt="Avatar" >
        </Avatar>
    );
};
