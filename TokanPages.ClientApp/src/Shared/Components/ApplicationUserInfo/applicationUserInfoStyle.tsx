import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../../Theme/customColours";

const ApplicationUserInfoStyle = makeStyles(() => (
{
    fullname:
    {
        fontSize: "1.5rem",
        color: CustomColours.colours.black
    },
    item:
    {
        fontSize: "1.1rem",
        color: CustomColours.colours.black
    },
    value:
    {
        fontSize: "1.1rem",
        color: CustomColours.colours.gray2
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

export default ApplicationUserInfoStyle;
