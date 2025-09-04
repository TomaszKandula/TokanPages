import * as React from "react";
import { GET_USER_IMAGE } from "../../../Api";
import { FigoureSize } from "../../../Shared/enums";
import { UserAvatarView } from "./View/userAvatarView";
import Validate from "validate.js";

export interface Properties {
    size: FigoureSize;
    userId?: string;
    userLetter?: string;
    avatarName?: string;
    altSource?: string;
    className?: string;
}

export const UserAvatar = (props: Properties): React.ReactElement => {
    let baseUrl = "";
    let source = "";

    const hasAltSource = !Validate.isEmpty(props.altSource);
    const hasUserId = !Validate.isEmpty(props.userId);
    const hasAvatarName = !Validate.isEmpty(props.avatarName);

    if (!hasAltSource && hasUserId && hasAvatarName) {
        baseUrl = GET_USER_IMAGE.replace("{id}", props.userId ?? "");
        source = baseUrl.replace("{name}", props.avatarName ?? "");
    }

    if (hasAltSource) {
        source = props.altSource ?? "";
    }

    return (
        <UserAvatarView
            size={props.size}
            userLetter={props.userLetter ?? "A"}
            avatarSource={source}
            className={props.className}
        />
    );
};
