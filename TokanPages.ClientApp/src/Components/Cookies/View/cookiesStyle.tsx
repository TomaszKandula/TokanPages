import { makeStyles } from "@material-ui/core";
import { Colours } from "../../../Theme";

export const CookiesStyle = makeStyles(() => (
{
    open:
    {
        opacity: 1,
        visibility: "visible",
    },
    close:
    {
        opacity: 0,
        transition: "0.3s all ease",
        visibility: "hidden"
    },
    container:
    {
        borderRadius: "15px",
        boxShadow: "0 2px 20px 0 rgb(0 0 0 / 20%)",
        padding: "8px",
        background: Colours.colours.white
    },
    caption:
    {
        fontSize: "1.5rem",
        paddingTop: "5px",
        paddingBottom: "15px",
        color: Colours.colours.black
    },
    text:
    {
        fontSize: "1.2rem",
        fontWeight: 400,
        color: Colours.colours.gray2
    },
    button:
    {
        "&:hover": 
        {
            color: Colours.colours.white,
            background: Colours.colours.darkViolet1,
        },
        color: Colours.colours.white,
        background: Colours.colours.violet,
        display: "block",
        marginLeft: "auto",
        marginRight: 0,
        marginBottom: "10px"
    }
}));
