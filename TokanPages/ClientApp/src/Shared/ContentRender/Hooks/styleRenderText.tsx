import { makeStyles } from "@material-ui/core";
import { CustomColours } from "Theme/customColours";

const useStyles = makeStyles(() => (
{
    common:
    {
        fontSize: 19,
        textAlign: "left",
        color: CustomColours.typography.darkGray1
    },
    title:
    {
        fontSize: 28,
        fontWeight: "bold",
        lineHeight: 1.0
    },
    subTitle:
    {
        lineHeight: 1.0
    },
    header:
    {
        fontSize: 25,
        fontWeight: "bold",
        lineHeight: 1.0
    },
    paragraph:
    {
        lineHeight: 2.2
    }
}));    

export default useStyles;
