import * as React from "react";
import { Avatar } from "@material-ui/core";
import Validate from "validate.js";

interface Properties {
    isLarge: boolean;
    userLetter: string;
    avatarSource: string;
}

export const UserAvatarView = (props: Properties): React.ReactElement => {
    const className = props.isLarge ? "user-avatar-avatar-large" : "user-avatar-avatar-small";

    if (Validate.isEmpty(props.avatarSource)) {
        return <Avatar className={className}>{props.userLetter}</Avatar>;
    }

    return <Avatar className={className} src={props.avatarSource} alt="Avatar"></Avatar>;
};
