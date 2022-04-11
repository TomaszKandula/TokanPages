import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../../Theme/customColours";

const articleCardStyle = makeStyles((theme) => (
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
    large_screen:
    {
        [theme.breakpoints.down(700)]:
        {
            display: "none"
        },
        [theme.breakpoints.up(700)]:
        {
            display: "flex"
        }
    },
    small_screen:
    {
        [theme.breakpoints.up(700)]:
        {
            display: "none"
        }
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
        [theme.breakpoints.down(700)]:
        {
            marginTop: "0px"
        },
        [theme.breakpoints.up(700)]:
        {
            marginTop: "18px"
        },
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
