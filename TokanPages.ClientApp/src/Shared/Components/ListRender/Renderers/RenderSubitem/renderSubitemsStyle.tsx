import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../../../../Theme/customColours";

export const RenderSubitemsStyle = makeStyles((theme) => (
{
    nested:
    {
        paddingLeft: theme.spacing(4)
    },
    href:
    {
        color: CustomColours.colours.black
    }
}));
