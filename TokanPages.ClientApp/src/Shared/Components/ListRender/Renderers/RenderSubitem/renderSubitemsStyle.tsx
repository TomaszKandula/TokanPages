import { makeStyles } from "@material-ui/core/styles";
import { Colours } from "../../../../../Theme";

export const RenderSubitemsStyle = makeStyles(theme => ({
    nested: {
        paddingLeft: theme.spacing(4),
    },
    href: {
        color: Colours.colours.black,
    },
}));
