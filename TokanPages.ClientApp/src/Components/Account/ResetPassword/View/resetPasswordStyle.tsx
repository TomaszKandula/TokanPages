import { makeStyles } from "@material-ui/core/styles";
import { Colours } from "../../../../Theme";

export const ResetPasswordStyle = makeStyles(theme => ({
    section: {
        backgroundColor: Colours.colours.white,
    },
    container: {
        maxWidth: 700,
    },
    back_arrow: {
        [theme.breakpoints.up(1000)]: {
            display: "none",
        },
        display: "block",
    },
    account: {
        fontSize: 72,
        color: Colours.colours.violet,
    },
    caption: {
        fontSize: "1.8rem",
        color: Colours.colours.gray1,
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
    button: {
        "&:hover": {
            color: Colours.colours.white,
            background: Colours.colours.darkViolet1,
        },
        color: Colours.colours.white,
        background: Colours.colours.violet,
    },
}));
