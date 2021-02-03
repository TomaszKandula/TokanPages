import { makeStyles } from "@material-ui/core";

const useStyles = makeStyles(() => (
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
        color: "#FFFFFF",
        width: "48px", 
        height: "48px"
    },
    avatarLarge:
    {
        color: "#FFFFFF",
        width: "96px", 
        height: "96px"
    },
    thumbsMedium:
    {
        color: "#9E9E9E",
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

export default useStyles;
