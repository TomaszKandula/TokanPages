import * as React from "react";
import { Avatar } from "@material-ui/core";
import { UserAvatarStyle } from "./userAvatarViewStyle";
import { GET_AVATARS_URL } from "../../../../Api/Request";
import Validate from "validate.js";

export interface IBinding
{
    isLargeScale: boolean;
    avatarName: string; 
    userLetter: string;
}

export const UserAvatarView = (props: IBinding): JSX.Element =>
{
    const classes = UserAvatarStyle();
    const className = props.isLargeScale ? classes.avatarLarge : classes.avatarSmall;

    if (Validate.isEmpty(props.avatarName))
    {
        return(<Avatar className={className}>{props.userLetter}</Avatar>);
    }

    return(
        <Avatar 
            className={className} 
            src={`${GET_AVATARS_URL}/${props.avatarName}`}
            alt="Avatar" >
        </Avatar>
    );
};
