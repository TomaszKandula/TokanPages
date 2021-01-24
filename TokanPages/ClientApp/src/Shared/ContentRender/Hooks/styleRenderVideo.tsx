import { makeStyles } from "@material-ui/core";

const useStyles = makeStyles(() => (
{
    card:
    {
        borderRadius: 0,
        marginTop: "40px",
        marginBottom: "40px"
    },
    image:
    {
        cursor: "pointer"
    },
    text:
    {
        color: "#9E9E9E",
        paddingTop: "1px",
        paddingBottom: "1px",
        paddingLeft: "10px",
        paddingRight: "10px",
        lineHeight: 1.8
    }
}));

export default useStyles;
