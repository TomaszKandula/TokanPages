import { makeStyles } from "@material-ui/core/styles";
import { Colours } from "../../../Theme";

export const FeaturesStyle = makeStyles((theme) => (
{
    section:
    {
        backgroundColor: Colours.colours.white
    },
    card: 
    {
        height: "100%",
        minHeight: 128,
        borderRadius: 15,
        boxShadow: "0 2px 20px 0 rgb(0 0 0 / 20%)"
    },
    card_content:
    {
        height: "100%",
        minHeight: 128,
    },
    card_image:
    {
        borderRadius: 15,
        boxShadow: "0 2px 20px 0 rgb(0 0 0 / 20%)"
    },
    title:
    {
        textAlign: "center",
        fontSize: "2.0rem",
        color: Colours.colours.darkViolet1
    },
    text1:
    {
        fontSize: "1.5rem"
    },
    text2:
    {
        fontSize: "1.0rem",
        lineHeight: "1.8",
        color: Colours.colours.gray2
    },
    media: 
    {
        height: 128,
    },
    content: 
    {
        paddingRight: theme.spacing(2),
        [theme.breakpoints.down("md")]: 
        {
            marginBottom: theme.spacing(2),
            paddingRight: theme.spacing(0),
        }
    },
    link:
    {
        textDecoration: "none"
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
    }
}));
