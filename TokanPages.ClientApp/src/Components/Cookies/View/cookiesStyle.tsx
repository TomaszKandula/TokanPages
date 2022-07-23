import { makeStyles } from "@material-ui/core";
import { CustomColours } from "../../../Theme/customColours";

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
        background: CustomColours.colours.white
    },
    caption:
    {
        fontSize: "1.5rem",
        paddingTop: "5px",
        paddingBottom: "15px",
        color: CustomColours.colours.black
    },
    text:
    {
        fontSize: "1.2rem",
        fontWeight: 400,
        color: CustomColours.colours.gray2
    },
    button:
    {
        "&:hover": 
        {
            color: CustomColours.colours.white,
            background: CustomColours.colours.darkViolet1,
        },
        color: CustomColours.colours.white,
        background: CustomColours.colours.violet,
        display: "block",
        marginLeft: "auto",
        marginRight: 0,
        marginBottom: "10px"
    }
}));
