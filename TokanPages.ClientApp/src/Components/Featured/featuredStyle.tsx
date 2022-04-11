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
    media: 
    {
        height: "256px"
    },
}));

export default featuredStyle;
