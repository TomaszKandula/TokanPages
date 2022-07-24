import { makeStyles } from "@material-ui/core";
import { Colours } from "../../../../../Theme";

export const RenderTextStyle = makeStyles(() => (
{
    common:
    {
        fontSize: 19,
        textAlign: "left",
        color: Colours.colours.gray3
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
