import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../../../Theme/customColours";

const subitemsStyle = makeStyles((theme) => (
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

export default subitemsStyle;
