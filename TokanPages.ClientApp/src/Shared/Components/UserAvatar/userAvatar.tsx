import * as React from "react";
import { Avatar } from "@material-ui/core";
import userAvatarStyle from "./userAvatarStyle";
import Validate from "validate.js";

export interface IBinding
{
    isLargeScale: boolean;
    avatarName: string; 
    userLetter: string;
}

const UserAvatar = (props: IBinding): JSX.Element =>
{
    const classes = userAvatarStyle();
    const className = props.isLargeScale ? classes.avatarLarge : classes.avatarSmall;

    if (Validate.isEmpty(props.avatarName))
    {
        return(
            <Avatar 
                className={className}>{props.userLetter}
            </Avatar>
        );
    }

    return(
        <>
            <Avatar 
                className={className} 
                src={props.avatarName} 
                alt="" 
            />
        </>
    );
};

export default UserAvatar;
