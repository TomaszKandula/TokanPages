import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../Theme/customColours";

const featuredStyle = makeStyles(() => (
{
    section:
    {
        backgroundColor: CustomColours.colours.lightGray3
    },
    caption_text:
    {
        textAlign: "center",
        fontSize: "2.0rem",
        color: CustomColours.colours.darkViolet1
    },
    card:
    {
        borderRadius: "15px",
        boxShadow: "0 2px 20px 0 rgb(0 0 0 / 20%)"
    },
    card_header:
    {
        minHeight: "150px",
    },
    media: 
    {
        height: "256px"
    },
}));

export default featuredStyle;
