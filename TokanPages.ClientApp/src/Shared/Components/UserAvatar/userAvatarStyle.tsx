import { makeStyles } from "@material-ui/core";
import { CustomColours } from "../../../Theme/customColours";

const userAvatarStyle = makeStyles(() => (
{
    avatarSmall:
    {
        color: CustomColours.colours.white,
        width: "48px", 
        height: "48px"
    },
    avatarLarge:
    {
        color: CustomColours.colours.white,
        width: "96px", 
        height: "96px"
    }
}));

export default userAvatarStyle;
