import { makeStyles } from "@material-ui/core";
import { CustomColours } from "Theme/customColours";

const useStyles = makeStyles(() => (
{
    typography:
    {
        fontSize: 19,
        textAlign: "left",
        color: CustomColours.typography.darkGray1,
        lineHeight: 2.2
    }
}));    

export default useStyles;
