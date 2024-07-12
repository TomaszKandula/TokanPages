import { makeStyles } from "@material-ui/core/styles";
import { Colours } from "../../../../../Theme";

export const RenderNavbarItemStyle = makeStyles(theme => ({
    href: {
        color: Colours.colours.black,
    },
    list_item: {
        "&:hover": {
            backgroundColor: Colours.colours.white,
        },
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
            transform: "scale(1.1)",
            transition: "transform 0.3s ease 0s",
        },
    },
    list_item_text_selected: {
        color: Colours.colours.darkViolet1,
        transform: "scale(1.1)",
    },
}));
