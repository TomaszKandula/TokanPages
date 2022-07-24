import { makeStyles } from "@material-ui/core/styles";
import { Colours } from "../../../Theme";

export const TechnologiesStyle = makeStyles((theme) => (
{
    section:
    {
        backgroundColor: Colours.colours.lightGray3
    },
    icon: 
    {
        width: "32px",
        height: "32px",
        color: Colours.colours.violet,
        marginRight: theme.spacing(1),
    },
    skeleton_circle:
    {
        width: "42px", 
        height: "42px",
        margin: "0px 15px 0px 0px"
    },
    caption_text:
    {
        textAlign: "center",
        fontSize: "2.0rem",
        color: Colours.colours.darkViolet1
    },
    feature_title:
    {
        fontSize: "1.5rem",
        color: Colours.colours.black
    },
    feature_text:
    {
        fontSize: "1.0rem",
        lineHeight: "1.8",
        color: Colours.colours.gray2
    },
}
));
