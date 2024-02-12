import { makeStyles } from "@material-ui/core/styles";
import { Colours } from "../../../Theme";

export const ContactFormStyle = makeStyles(() => ({
    section: {
        backgroundColor: Colours.colours.white,
    },
    caption: {
        textAlign: "center",
        fontSize: "2.0rem",
        color: Colours.colours.darkViolet1,
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
