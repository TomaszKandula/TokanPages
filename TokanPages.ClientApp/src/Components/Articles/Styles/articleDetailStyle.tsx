import { makeStyles } from "@material-ui/core";
import { CustomColours } from "../../../Theme/customColours";

const articleDetailStyle = makeStyles(() => (
{
    container:
    {
        maxWidth: "700px"
    },
    dividerTop:
    {
        marginTop: "20px",
        marginBottom: "20px"
    },
    dividerBottom:
    {
        marginTop: "30px",
        marginBottom: "30px"
    },
    readCount:
    {
        paddingTop: "10px"
    },
    aliasName:
    {
        paddingTop: "10px"
    },
    avatarSmall:
    {
        color: CustomColours.typography.white,
        width: "48px", 
        height: "48px"
    },
    avatarLarge:
    {
        color: CustomColours.typography.white,
        width: "96px", 
        height: "96px"
    },
    thumbsMedium:
    {
        color: CustomColours.background.gray1,
        cursor: "pointer",
        width: "24px", 
        height: "24px"
    },
    likesTip:
    {
        fontSize: "1.5em"
    },
    popover:
    {
        pointerEvents: "none"
    }
}));

export default articleDetailStyle;
