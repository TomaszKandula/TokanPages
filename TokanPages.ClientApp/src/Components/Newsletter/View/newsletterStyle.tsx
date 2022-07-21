import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../../Theme/customColours";

const newsletterStyle = makeStyles(() => (
{
    section:
    {
        backgroundColor: CustomColours.colours.lightGray3
    },
    caption:
    {
        fontSize: "2.0rem"
    },
    text:
    {
        fontSize: "1.2rem",
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
        background: CustomColours.colours.violet
    }
}));

export default newsletterStyle;
