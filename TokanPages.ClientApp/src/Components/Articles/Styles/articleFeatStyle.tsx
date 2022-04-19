import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../../Theme/customColours";

const articleFeatStyle = makeStyles((theme) => (
{
    section:
    {
        backgroundColor: CustomColours.colours.white
    },
    card: 
    {
        height: "100%",
        minHeight: "128px",
        borderRadius: "15px",
        boxShadow: "0 2px 20px 0 rgb(0 0 0 / 20%)"
    },
    card_content:
    {
        height: "100%",
        minHeight: "128px",
    },
    card_image:
    {
        borderRadius: "15px",
        boxShadow: "0 2px 20px 0 rgb(0 0 0 / 20%)"
    },
    title:
    {
        textAlign: "center",
        fontSize: "2.0rem",
        color: CustomColours.colours.darkViolet1
    },
    text1:
    {
        fontSize: "1.5rem"
    },
    text2:
    {
        fontSize: "1.0rem",
        lineHeight: "1.8",
        color: CustomColours.colours.gray2
    },
    media: 
    {
        height: "128px",
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
            color: CustomColours.colours.white,
            background: CustomColours.colours.darkViolet1,
        },
        color: CustomColours.colours.white,
        background: CustomColours.colours.violet,
    }
}));

export default articleFeatStyle;
