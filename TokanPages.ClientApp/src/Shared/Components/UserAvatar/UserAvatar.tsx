import * as React from "react";
import { GET_USER_MEDIA } from "../../../Api/Request";
import { UserAvatarView } from "./View/userAvatarView";
import Validate from "validate.js";

export interface Properties
{
    userId: string;
    isLarge: boolean;
    userLetter: string;
    avatarName: string; 
}

export const UserAvatar = (props: Properties): JSX.Element => 
{
    let baseUrl = "";
    let source = "";
    
    if (!Validate.isEmpty(props.userId) && !Validate.isEmpty(props.avatarName))
    {
        baseUrl = GET_USER_MEDIA.replace("{id}", props.userId);
        source = baseUrl.replace("{name}", props.avatarName);
    }

    return (
        <UserAvatarView
            isLarge={props.isLarge}
            userLetter={props.userLetter}
            avatarSource={source}
        />
    );
}
