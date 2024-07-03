import { makeStyles } from "@material-ui/core/styles";
import { Colours } from "../../../../../Theme";

export const RenderNavbarItemSpanStyle = makeStyles((theme) => ({
    href: {
        color: Colours.colours.black,
    },
    button: {
        textTransform: "none",
        fontWeight: 400,
    },
    text: {
        [theme.breakpoints.down(1100)]: {
            fontSize: 14,
        },
        [theme.breakpoints.up(1100)]: {
            fontSize: 16,
        },
        color: Colours.colours.black,
        whiteSpace: "nowrap",
        overflow: "hidden",
        textOverflow: "ellipsis",
    },
}));
