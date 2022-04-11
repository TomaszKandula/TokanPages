import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../../Theme/customColours";

const headerStyle = makeStyles((theme) => (
{
    top_margin:
    {
        marginTop: "65px"
    },
    action_button: 
    {
        "&:hover": 
        {
            color: CustomColours.colours.lightViolet,
            background: CustomColours.colours.darkViolet1,
        },
        color: CustomColours.colours.darkViolet1,
        background: CustomColours.colours.lightViolet,
        [theme.breakpoints.down("xs")]: 
        {
            width: "100%",
        }
    },
    action_link:
    {
        textDecoration: "none"
    },
    content_box: 
    {
        maxWidth: theme.breakpoints.values["md"],
        paddingTop: theme.spacing(12),
        paddingBottom: theme.spacing(8),
        marginLeft: "auto",
        marginRight: "auto",
        textAlign: "left",
        [theme.breakpoints.up("lg")]: 
        {
            maxWidth: theme.breakpoints.values["lg"] / 2,
            maxHeight: 400,
            paddingTop: theme.spacing(12),
            paddingBottom: theme.spacing(16),
            paddingRight: theme.spacing(24),
            textAlign: "left",
        },
        [theme.breakpoints.down("xs")]: 
        {
            paddingTop: theme.spacing(3),
        }
    },
    content_description:
    {
        color: CustomColours.colours.gray2,
        fontWeight: 400
    },
    image_box:
    {
        position: "relative",
        height: 400, 
        display: "flex", 
        justifyContent: "center", 
        alignItems: "center"
    },
    image: 
    {
        borderRadius: "50%",
        border: "25px solid white",
        objectFit: "cover",
        height: 400,
        maxWidth: "100%"
    }
}));

export default headerStyle;
