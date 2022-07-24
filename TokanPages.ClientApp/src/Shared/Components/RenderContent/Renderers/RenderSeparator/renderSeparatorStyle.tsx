import { makeStyles } from "@material-ui/core";
import { Colours } from "../../../../../Theme";

export const RenderSeparatorStyle = makeStyles(() => (
{
    separator:
    {
        textAlign: "center",
        marginTop: "50px",
        marginBottom: "50px"
    },
    span:
    {
        width: "5px",
        height: "5px",
        borderRadius: "50%",
        background: Colours.colours.gray1,
        display: "inline-block",
        margin: "0 10px"
    }
}));    
