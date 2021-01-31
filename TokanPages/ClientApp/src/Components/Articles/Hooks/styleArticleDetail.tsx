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
    avatar:
    {
        color: "#1975D2",
        width: "48px", 
        height: "48px"
    },
    popover:
    {
        pointerEvents: "none"
    }
}));

export default useStyles;
