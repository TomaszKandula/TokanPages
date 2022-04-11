import { makeStyles } from "@material-ui/core";
import { CustomColours } from "../../Theme/customColours";

const cookiesStyle = makeStyles(() => (
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
        background: CustomColours.colours.lightViolet
    },
    caption:
    {
        fontSize: "1.5rem",
        color: CustomColours.colours.black
    },
    text:
    {
        fontSize: "1.2rem",
        fontWeight: 400,
        color: CustomColours.colours.darkViolet1
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
    }
}));

export default cookiesStyle;
