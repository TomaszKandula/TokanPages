import { makeStyles } from "@material-ui/core";
import { Colours } from "../../../../Theme";

export const UserAvatarStyle = makeStyles(() => (
{
    avatarSmall:
    {
        color: Colours.colours.white,
        width: 48, 
        height: 48
    },
    avatarLarge:
    {
        color: Colours.colours.white,
        width: 96, 
        height: 96
    }
}));
