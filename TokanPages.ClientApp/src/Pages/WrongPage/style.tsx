import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../Theme/customColours";

export const Style = makeStyles(() => 
({
    link:
    {
        textDecoration: "none"
    },
    skeleton:
    {
        marginLeft: "auto",
        marginRight: "auto"
    },
    button:
    {
        "&:hover": 
        {
            color: CustomColours.colours.white,
            background: CustomColours.colours.darkViolet1,
        },
        color: CustomColours.colours.white,
        background: CustomColours.colours.violet
    }
}));
