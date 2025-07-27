import * as React from "react";
import { Avatar } from "../../../../Shared/Components";
import Validate from "validate.js";

interface Properties {
    isLarge: boolean;
    userLetter: string;
    avatarSource: string;
    className?: string;
}

export const UserAvatarView = (props: Properties): React.ReactElement => {
    const className = props.isLarge
        ? `bulma-is-96x96 ${props.className ?? ""}`
        : `bulma-is-48x48 ${props.className ?? ""}`;

    if (Validate.isEmpty(props.avatarSource)) {
        return (
            <Avatar className={className} alt="User avatar" title="Avatar">
                <h2 className="is-size-4 has-text-white">{props.userLetter}</h2>
            </Avatar>
        );
    }

    return <Avatar className={className} src={props.avatarSource} alt="User avatar" title="Avatar"></Avatar>;
};
