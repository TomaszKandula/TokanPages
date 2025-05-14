import * as React from "react";
import Validate from "validate.js";
import { Avatar, IconButton } from "../../../../Shared/Components";
import { BaseProperties } from "../Abstractions";

export const RenderAvatarIcon = (props: BaseProperties): React.ReactElement => {
    const ANONYMOUS_LETTER = "A";
    return (
        <div className="navigation-user-avatar">
            <IconButton onClick={props.infoHandler}>
                {props.isAnonymous
                ? <Avatar alt="User avatar" title="Avatar">
                    {ANONYMOUS_LETTER}
                </Avatar>
                : Validate.isEmpty(props.avatarName) 
                ? <Avatar alt="User avatar" title="Avatar">
                    {props.userAliasText?.charAt(0).toUpperCase()}
                </Avatar> 
                : <Avatar alt="User avatar" title="Avatar" src={props.avatarSource} />}
            </IconButton>
        </div>
    );
};
