import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../Theme/customColours";

const featuresStyle = makeStyles((theme) => (
{
    section:
    {
        backgroundColor: CustomColours.colours.lightGray3
    },
    icon: 
    {
        width: "32px",
        height: "32px",
        color: CustomColours.colours.violet,
        marginRight: theme.spacing(1),
    },
    caption_text:
    {
        textAlign: "center",
        fontSize: "2.0rem",
        color: CustomColours.colours.darkViolet1
    },
    feature_title:
    {
        fontSize: "1.5rem",
        color: CustomColours.colours.black
    },
    feature_text:
    {
        fontSize: "1.0rem",
        color: CustomColours.colours.gray2
    },
}
));

export default featuresStyle;
