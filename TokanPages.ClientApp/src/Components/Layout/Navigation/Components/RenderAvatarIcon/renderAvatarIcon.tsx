import * as React from "react";
import { Avatar, IconButton, Media } from "../../../../../Shared/Components";
import { FigoureSize } from "../../../../../Shared/Enums";
import { NavigationViewBaseProps } from "../../Types";
import Validate from "validate.js";

const ANONYMOUS_LETTER = "A";

const RenderAvatar = (props: NavigationViewBaseProps): React.ReactElement => {
    if (props.isAnonymous) {
        return (
            <Avatar alt="User avatar" title="Avatar" size={FigoureSize.medium}>
                {ANONYMOUS_LETTER}
            </Avatar>
        );
    }

    if (Validate.isEmpty(props.avatarName)) {
        return (
            <Avatar alt="User avatar" title="Avatar" size={FigoureSize.medium}>
                {props.aliasName?.charAt(0).toUpperCase()}
            </Avatar>
        );
    } else {
        return <Avatar alt="User avatar" title="Avatar" src={props.avatarSource} size={FigoureSize.medium} />;
    }
};

export const RenderAvatarIcon = (props: NavigationViewBaseProps): React.ReactElement => (
    <Media.DesktopOnly>
        <IconButton onClick={props.infoHandler} className="mr-4 no-select">
            <RenderAvatar {...props} />
        </IconButton>
    </Media.DesktopOnly>
);
