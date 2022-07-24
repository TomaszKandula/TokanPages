import { makeStyles } from "@material-ui/core/styles";
import { Colours } from "../../../Theme";

export const HeaderStyle = makeStyles((theme) => (
{
    top_margin:
    {
        marginTop: "85px"
    },
    action_button: 
    {
        "&:hover": 
        {
            color: Colours.colours.lightViolet,
            background: Colours.colours.darkViolet1,
        },
        color: Colours.colours.darkViolet1,
        background: Colours.colours.lightViolet,
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
        color: Colours.colours.gray2,
        fontWeight: 400
    },
    image_skeleton:
    {
        height: "300px",
        width: "300px"
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
