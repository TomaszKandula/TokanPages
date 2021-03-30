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
        lineHeight: 0.7
    },
    subTitle:
    {
        lineHeight: 0.5
    },
    header:
    {
        lineHeight: 2.3
    },
    paragraph:
    {
        lineHeight: 2.2
    }
}));    

export default useStyles;
