import { makeStyles } from "@material-ui/core";

const useStyles = makeStyles(() => (
{
    separator:
    {
        textAlign: "center",
        marginTop: "50px",
        marginBottom: "50px"
    },
    span:
    {
        width: "5px",
        height: "5px",
        borderRadius: "50%",
        background: "gray",
        display: "inline-block",
        margin: "0 10px"
    }
}));    

export default useStyles;
