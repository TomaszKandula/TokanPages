import * as React from "react";
import { Avatar } from "@material-ui/core";
import userAvatarStyle from "./userAvatarStyle";
import { AVATARS_PATH } from "../../../Shared/constants";
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

    const avatarUrl = `${AVATARS_PATH}${props.avatarName}`;
    return(
        <>
            <Avatar 
                className={className} 
                src={avatarUrl} 
                alt="" 
            />
        </>
    );
};

export default UserAvatar;
