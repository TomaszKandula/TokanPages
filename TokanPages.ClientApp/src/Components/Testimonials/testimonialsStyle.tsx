import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../Theme/customColours";

const testimonialsStyle = makeStyles((theme) => (
{
    section:
    {
        backgroundColor: CustomColours.colours.white
    },
    image:
    {
        width: "150px !important",
        borderRadius: "50%"
    },
    caption_text:
    {
        textAlign: "center",
        fontSize: "2.0rem",
        color: CustomColours.colours.darkViolet1
    },
    title:
    {
        fontSize: "1.2rem",
        fontWeight: 400,
        textAlign: "center",
        marginTop: "15px",
        marginBottom: "15px"
    },
    subtitle:
    {
        fontSize: "1.2rem",
        fontWeight: 400,
        textAlign: "center",
        marginTop: "15px",
        marginBottom: "15px"
    },
    commendation:
    {
        fontSize: "1.2rem",
        fontWeight: 400,
        lineHeight: 2.0,
        marginLeft: "25px",
        marginRight: "25px",
        color: CustomColours.colours.gray2
    },
    box_padding:
    {
        [theme.breakpoints.up("xl")]: 
        {
            paddingLeft: "125px",
            paddingRight: "125px"
        },
        [theme.breakpoints.up("lg")]: 
        {
            paddingLeft: "125px",
            paddingRight: "125px"
        },
        [theme.breakpoints.up("md")]: 
        {
            paddingLeft: "125px",
            paddingRight: "125px"
        },
        [theme.breakpoints.up("sm")]: 
        {
            paddingLeft: "75px",
            paddingRight: "75px"
        }
    }
}));

export default testimonialsStyle;
