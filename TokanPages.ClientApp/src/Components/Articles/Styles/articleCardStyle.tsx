import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../../Theme/customColours";

const articleCardStyle = makeStyles(() => (
{
    root: 
    {
        marginTop: 25,
        marginBottom: 25
    },
    link:
    {
        textDecoration: "none"
    },
    image:
    {
        margin: "auto",
        display: "block",
        objectFit: "cover",
        height: 180,
        maxWidth: "100%"
    },
    title:
    {
        fontSize: "1.5rem",
        color: CustomColours.colours.black
    },
    description:
    {
        fontSize: "1.0rem",
        color: CustomColours.colours.gray1
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

export default articleCardStyle;
