import { makeStyles } from "@material-ui/core/styles";
import { Colours } from "../../../Theme";

export const BusinessFormStyle = makeStyles(() => ({
    section: {
        backgroundColor: Colours.colours.white,
    },
    container: {
        maxWidth: 900,
    },
    caption: {
        textAlign: "center",
        fontSize: "2.0rem",
        color: Colours.colours.violet,
    },
    small_caption: {
        fontSize: "1.8rem",
        color: Colours.colours.gray1,
    },
    button: {
        "&:hover": {
            color: Colours.colours.white,
            background: Colours.colours.darkViolet1,
        },
        color: Colours.colours.white,
        background: Colours.colours.violet,
    },
    icon: {
        fontSize: 72,
        color: Colours.colours.violet,
    },
    card: {
        borderRadius: 15,
        boxShadow: "0 2px 20px 0 rgb(0 0 0 / 20%)",
    },
    card_content: {
        marginTop: 10,
        marginLeft: 15,
        marginRight: 15,
        marginBottom: 10,
    },
    header: {
        fontSize: 20,
    },
    pricing_text: {
        color: Colours.colours.gray2,
    },
    paper: {
        minHeight: 110,
        padding: 15,
    },
}));
