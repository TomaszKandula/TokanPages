import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../../Theme/customColours";

const articleCardStyle = makeStyles((theme) => (
{
    root: 
    {
        marginTop: "25px",
        marginBottom: "25px",
        [theme.breakpoints.down(700)]:
        {
            display: "block"
        },
        [theme.breakpoints.up(700)]:
        {
            display: "flex"
        },
    },
    link:
    {
        textDecoration: "none"
    },
    image:
    {
        [theme.breakpoints.down(700)]:
        {
            height: "180px",
            width: "auto",
        },
        [theme.breakpoints.up(700)]:
        {
            height: "auto",
            width: "180px",
        }
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
    action:
    {
        marginTop: "25px"
    },
    button:
    {
        [theme.breakpoints.down(700)]:
        {
            marginLeft: "auto"
        },
        [theme.breakpoints.up(700)]:
        {
            marginLeft: "0px"
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
