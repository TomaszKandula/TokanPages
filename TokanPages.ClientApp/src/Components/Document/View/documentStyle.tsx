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
        maxWidth: 700
    },
    divider:
    {
        marginTop: 20,
        marginBottom: 10
    }
}));
