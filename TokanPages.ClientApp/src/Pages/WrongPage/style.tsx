import { makeStyles } from "@material-ui/core/styles";
import { Colours } from "../../Theme";

export const Style = makeStyles(() => ({
    link: {
        textDecoration: "none",
    },
    skeleton: {
        marginLeft: "auto",
        marginRight: "auto",
    },
    button: {
        "&:hover": {
            color: Colours.colours.white,
            background: Colours.colours.darkViolet1,
        },
        color: Colours.colours.white,
        background: Colours.colours.violet,
    },
}));
