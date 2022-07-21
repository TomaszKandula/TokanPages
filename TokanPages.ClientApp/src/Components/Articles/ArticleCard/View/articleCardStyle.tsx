import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../../../Theme/customColours";

const articleCardStyle = makeStyles((theme) => (
{
    card: 
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
        borderRadius: "15px",
        boxShadow: "0 2px 20px 0 rgb(0 0 0 / 20%)"
    },
    card_link:
    {
        textDecoration: "none"
    },
    card_image:
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
    card_title:
    {
        fontSize: "1.5rem",
        color: CustomColours.colours.black
    },
    card_description:
    {
        fontSize: "1.0rem",
        color: CustomColours.colours.gray1
    },
    card_action:
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
