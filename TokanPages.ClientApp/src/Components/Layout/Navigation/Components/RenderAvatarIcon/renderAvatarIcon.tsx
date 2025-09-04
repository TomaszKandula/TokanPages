import * as React from "react";
import { Avatar, IconButton, Media } from "../../../../../Shared/Components";
import { FigoureSize } from "../../../../../Shared/enums";
import { BaseProperties } from "../../Abstractions";
import Validate from "validate.js";

const ANONYMOUS_LETTER = "A";

const RenderAvatar = (props: BaseProperties): React.ReactElement => {
    if (props.isAnonymous) {
        return (
            <Avatar alt="User avatar" title="Avatar" size={FigoureSize.m}>
                {ANONYMOUS_LETTER}
            </Avatar>
        );
    }

    if (Validate.isEmpty(props.avatarName)) {
        return (
            <Avatar alt="User avatar" title="Avatar" size={FigoureSize.m}>
                {props.aliasName?.charAt(0).toUpperCase()}
            </Avatar>
        );
    } else {
        return <Avatar alt="User avatar" title="Avatar" src={props.avatarSource} size={FigoureSize.m} />;
    }
};

export const RenderAvatarIcon = (props: BaseProperties): React.ReactElement => (
    <Media.DesktopOnly>
        <IconButton onClick={props.infoHandler} className="mr-4">
            <RenderAvatar {...props} />
        </IconButton>
    </Media.DesktopOnly>
);
