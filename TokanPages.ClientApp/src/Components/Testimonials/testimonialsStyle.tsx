import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../Theme/customColours";

const testimonialsStyle = makeStyles(() => (
{
    section:
    {
        backgroundColor: CustomColours.colours.white
    },
    caption_text:
    {
        textAlign: "center",
        fontSize: "2.0rem",
        color: CustomColours.colours.darkViolet1
    },
    card:
    {
        minHeight: "340px",
        borderRadius: "15px",
        boxShadow: "0 2px 20px 0 rgb(0 0 0 / 20%)"
    },
    card_image:
    {
        height: "140px",
        width: "140px",
        borderRadius: "50%",
        top: 0,
        left: "50%",
        margin: 0,
        transform: "translate(-50%, -33%)",
        overflow: "hidden",
        position: "absolute",
        verticalAlign: "middle"
    },
    card_content:
    {
        marginTop: "70px"
    },
    card_title:
    {
        textAlign: "center",
        fontSize: "1.5rem",
        fontWeight: 700,
        lineHeight: "2.2",
        color: CustomColours.colours.black
    },
    card_subheader:
    {
        textAlign: "center",
        fontSize: "1.0rem",
        lineHeight: "2.0",
        color: CustomColours.colours.darkViolet1
    },
    card_text:
    {
        textAlign: "left",
        lineHeight: "1.8",
        marginTop: "15px",
        color: CustomColours.colours.gray1
    },
}));

export default testimonialsStyle;
