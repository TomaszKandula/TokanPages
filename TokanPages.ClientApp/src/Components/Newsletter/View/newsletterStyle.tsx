import { makeStyles } from "@material-ui/core/styles";
import { Colours } from "../../../Theme";

export const NewsletterStyle = makeStyles(() => (
{
    section:
    {
        backgroundColor: Colours.colours.lightGray3
    },
    caption:
    {
        fontSize: "2.0rem"
    },
    text:
    {
        fontSize: "1.2rem",
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
        background: Colours.colours.violet
    }
}));
