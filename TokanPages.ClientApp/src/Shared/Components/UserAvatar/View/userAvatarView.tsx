import * as React from "react";
import { Avatar } from "@material-ui/core";
import Validate from "validate.js";

interface Properties {
    isLarge: boolean;
    userLetter: string;
    avatarSource: string;
    className?: string;
}

export const UserAvatarView = (props: Properties): React.ReactElement => {
    const className = props.isLarge
        ? `user-avatar-avatar-large ${props.className ?? ""}`
        : `user-avatar-avatar-small ${props.className ?? ""}`;

    if (Validate.isEmpty(props.avatarSource)) {
        return (
            <Avatar className={className} alt="User avatar" title="Avatar">
                {props.userLetter}
            </Avatar>
        );
    }

    return <Avatar className={className} src={props.avatarSource} alt="User avatar" title="Avatar"></Avatar>;
};
