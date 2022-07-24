import { makeStyles } from "@material-ui/core";
import { Colours } from "../../../../../Theme";

export const RenderVideoStyle = makeStyles(() => (
{
    card:
    {
        borderRadius: 0,
        marginTop: "40px",
        marginBottom: "40px"
    },
    image:
    {
        cursor: "pointer"
    },
    text:
    {
        color: Colours.colours.gray1,
        paddingTop: "1px",
        paddingBottom: "1px",
        paddingLeft: "10px",
        paddingRight: "10px",
        lineHeight: 1.8
    }
}));
