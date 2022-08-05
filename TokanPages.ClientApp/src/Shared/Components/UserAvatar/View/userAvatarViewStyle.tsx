import { makeStyles } from "@material-ui/core";
import { Colours } from "../../../../Theme";

export const UserAvatarStyle = makeStyles(() => (
{
    avatarSmall:
    {
        color: Colours.colours.white,
        width: "48px", 
        height: "48px"
    },
    avatarLarge:
    {
        color: Colours.colours.white,
        width: "96px", 
        height: "96px"
    }
}));
