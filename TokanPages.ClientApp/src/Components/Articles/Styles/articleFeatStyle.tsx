import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../../Theme/customColours";

const articleFeatStyle = makeStyles((theme) => (
{
    section:
    {
        backgroundColor: CustomColours.colours.white
    },
    info: 
    {
        height: "100%",
        minHeight: "128px",
    },
    media: 
    {
        height: "128px",
    },
    firstColumn: 
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
    }
}));

export default articleFeatStyle;
