import { makeStyles } from "@material-ui/core";
import { Colours } from "../../../Theme";

export const DocumentStyle = makeStyles(() => (
{
    section:
    {
        backgroundColor: Colours.colours.white
    },
    container:
    {
        maxWidth: "700px"
    },
    divider:
    {
        marginTop: "20px",
        marginBottom: "10px"
    }
}));
