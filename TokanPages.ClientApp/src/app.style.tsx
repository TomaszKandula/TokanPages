import { makeStyles } from "@material-ui/core/styles";
import { Colours } from "./Theme";

export const AppStyle = makeStyles(() => (
{
    button:
    {
        "&:hover": 
        {
            color: Colours.colours.lightViolet,
            background: Colours.colours.darkViolet1,
        },
        color: Colours.colours.darkViolet1,
        background: Colours.colours.lightViolet
    }
}));
