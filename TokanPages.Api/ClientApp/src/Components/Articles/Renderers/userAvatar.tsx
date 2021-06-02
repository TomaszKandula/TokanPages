import * as React from "react";
import { Avatar } from "@material-ui/core";
import articleDetailStyle from "../Styles/articleDetailStyle";
import { AVATARS_PATH } from "../../../Shared/constants";
import Validate from "validate.js";

export const UserAvatar = (isLargeScale: boolean, avatarName: string, userLetter: string) =>
{
    const classes = articleDetailStyle();
    const className = isLargeScale ? classes.avatarLarge : classes.avatarSmall;

    if (Validate.isEmpty(avatarName))
    {
        return(<Avatar className={className}>{userLetter}</Avatar>);
    }

    const avatarUrl = AVATARS_PATH + avatarName;
    return(<><Avatar className={className} src={avatarUrl} alt="" /></>);
};
