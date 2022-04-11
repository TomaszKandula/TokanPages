import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../../Theme/customColours";

const footerStyle = makeStyles((theme) => (
{
    root: 
    {
        [theme.breakpoints.down("md")]: 
        {
            textAlign: "center"
        },
        background: CustomColours.colours.violet,
    },
    icon_box: 
    {
        [theme.breakpoints.down("md")]: 
        {
            width: "100%",
            marginBottom: theme.spacing(0),
        }
    },
    icon:
    {
        color: CustomColours.colours.white
    },
    copy: 
    {
        fontSize: "1.2rem",
        color: CustomColours.colours.white,
        [theme.breakpoints.down("md")]: 
        {
            width: "100%",
            order: 12,
        }
    },
    links:
    {
        color: CustomColours.colours.white,
        textDecoration: "none"
    },
    version:
    {
        color: CustomColours.colours.white,
    }
}));

export default footerStyle;
