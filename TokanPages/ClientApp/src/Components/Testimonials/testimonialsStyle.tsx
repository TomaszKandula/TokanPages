import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../Theme/customColours";

const testimonialsStyle = makeStyles((theme) => (
{
    section:
    {
        backgroundColor: CustomColours.background.white
    },
    img:
    {
        width: "120px !important",
        borderRadius: "50%"
    },
    title:
    {
        marginTop: "15px",
        marginBottom: "15px"
    },
    subtitle:
    {
        marginTop: "15px",
        marginBottom: "15px"
    },
    commendation:
    {
        marginLeft: "25px",
        marginRight: "25px",
        lineHeight: 2.0
    },
    boxPadding:
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
