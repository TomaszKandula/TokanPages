import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "./Theme/customColours";

const appStyle = makeStyles(() => (
{
    button:
    {
        "&:hover": 
        {
            color: CustomColours.colours.lightViolet,
            background: CustomColours.colours.darkViolet1,
        },
        color: CustomColours.colours.darkViolet1,
        background: CustomColours.colours.lightViolet
    }
}));

export default appStyle;
