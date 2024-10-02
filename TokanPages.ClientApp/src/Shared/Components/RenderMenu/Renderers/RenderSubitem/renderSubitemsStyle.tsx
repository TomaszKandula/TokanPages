import { makeStyles } from "@material-ui/core/styles";
import { Colours } from "../../../../../Theme";

export const RenderSubitemsStyle = makeStyles(theme => ({
    href: {
        color: Colours.colours.black,
    },
    list_icon: {
        minWidth: 40,
    },
    list_item: {
        "&:hover": {
            backgroundColor: Colours.colours.white,
        },
    },
    list_item_indent: {
        paddingLeft: theme.spacing(4),
    },
    list_item_base: {
        fontSize: 16,
    },
    list_item_text: {
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
        "&:hover": {
            color: Colours.colours.darkViolet1,
            transform: "scale(1.05)",
            transition: "transform 0.3s ease 0s",
        },
    },
}));
