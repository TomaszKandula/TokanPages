import * as React from "react";
import { useSelector } from "react-redux";
import { IApplicationState } from "../../../Store/Configuration";
import { GET_USER_MEDIA } from "../../../Api/Request";
import { UserAvatarView } from "./View/userAvatarView";
import Validate from "validate.js";

export interface IUserAvatar
{
    isLarge: boolean;
    userLetter: string;
    avatarName: string; 
}

export const UserAvatar = (props: IUserAvatar): JSX.Element => 
{
    const store = useSelector((state: IApplicationState) => state.userDataStore.userData);
    const source = !Validate.isEmpty(props.avatarName) 
    ? GET_USER_MEDIA.replace("{id}", store.userId).replace("{name}", props.avatarName) 
    : "";

    return (
        <UserAvatarView bind={{
            isLarge: props.isLarge,
            userLetter: props.userLetter,
            avatarSource: source
        }}/>
    );
}
