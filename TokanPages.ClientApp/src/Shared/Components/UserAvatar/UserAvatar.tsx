import * as React from "react";
import { GET_USER_IMAGE } from "../../../Api";
import { UserAvatarView } from "./View/userAvatarView";
import { UserAvatarProps } from "./Types";
import Validate from "validate.js";

export const UserAvatar = (props: UserAvatarProps): React.ReactElement => {
    let baseUrl = "";
    let source = "";

    const hasAltSource = !Validate.isEmpty(props.altSource);
    const hasUserId = !Validate.isEmpty(props.userId);
    const hasAvatarName = !Validate.isEmpty(props.avatarName);

    if (hasUserId && hasAvatarName) {
        baseUrl = GET_USER_IMAGE.replace("{id}", props.userId ?? "");
        source = baseUrl.replace("{name}", props.avatarName ?? "");
    } else if (hasAltSource) {
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
