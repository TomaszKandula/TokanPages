import { makeStyles } from "@material-ui/core";
import { CustomColours } from "../../../../Theme/customColours";

export const ArticleDetailStyle = makeStyles(() => (
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
        color: CustomColours.colours.white,
        width: "48px", 
        height: "48px"
    },
    avatarLarge:
    {
        color: CustomColours.colours.white,
        width: "96px", 
        height: "96px"
    },
    thumbsMedium:
    {
        color: CustomColours.colours.gray1,
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