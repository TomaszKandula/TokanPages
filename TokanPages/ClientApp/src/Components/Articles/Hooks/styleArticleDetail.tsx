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
        color: "#1975D2",
        width: "48px", 
        height: "48px"
    },
    avatarLarge:
    {
        color: "#1975D2",
        width: "96px", 
        height: "96px"
    },
    popover:
    {
        pointerEvents: "none"
    }
}));

export default useStyles;
