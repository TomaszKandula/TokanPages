import * as React from "react";
import { Avatar } from "../../../../Shared/Components";
import { FigoureSize } from "../../../../Shared/Enums";
import Validate from "validate.js";

interface Properties {
    size: FigoureSize;
    userLetter: string;
    avatarSource: string;
    className?: string;
}

export const UserAvatarView = (props: Properties): React.ReactElement => {
    if (Validate.isEmpty(props.avatarSource)) {
        return (
            <Avatar className={props.className} alt="User avatar" title="Avatar" size={props.size}>
                <p className="is-size-4 has-text-white">{props.userLetter}</p>
            </Avatar>
        );
    }

    return (
        <Avatar
            className={props.className}
            size={props.size}
            src={props.avatarSource}
            alt="User avatar"
            title="Avatar"
        ></Avatar>
    );
};
