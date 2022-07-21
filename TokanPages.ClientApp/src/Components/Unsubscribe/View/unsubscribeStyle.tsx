import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../../Theme/customColours";

export const UnsubscribeStyle = makeStyles(() => (
{
    section:
    {
        backgroundColor: CustomColours.colours.white
    },
    caption:
    {
        fontSize: "2.0rem",
        fontWeight: 400,
        color: CustomColours.colours.black
    },
    text1:
    {
        fontSize: "1.2rem",
        fontWeight: 500,
        color: CustomColours.colours.gray1
    },
    text2:
    {
        fontSize: "1.2rem",
        fontWeight: 400,
        color: CustomColours.colours.gray1        
    },
    text3:
    {
        fontSize: "1.2rem",
        fontWeight: 400,
        color: CustomColours.colours.gray1        
    },
    card:
    {
        marginTop: 10,
        marginLeft: 15,
        marginRight: 15,
        marginBottom: 10
    },
    button:
    {
        "&:hover": 
        {
            color: CustomColours.colours.white,
            background: CustomColours.colours.darkViolet1,
        },
        color: CustomColours.colours.white,
        background: CustomColours.colours.violet,
    }
}));
