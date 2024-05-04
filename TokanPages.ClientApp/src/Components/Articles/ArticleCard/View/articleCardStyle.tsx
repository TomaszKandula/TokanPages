import { makeStyles } from "@material-ui/core/styles";
import { Colours } from "../../../../Theme";

export const ArticleCardStyle = makeStyles(theme => ({
    card: {
        marginTop: 25,
        marginBottom: 25,
        [theme.breakpoints.down(700)]: {
            display: "block",
        },
        [theme.breakpoints.up(700)]: {
            display: "flex",
        },
        borderRadius: 15,
        boxShadow: "0 2px 20px 0 rgb(0 0 0 / 20%)",
    },
    card_link: {
        textDecoration: "none",
    },
    card_image: {
        [theme.breakpoints.down(700)]: {
            height: 180,
            width: "auto",
        },
        [theme.breakpoints.up(700)]: {
            height: "auto",
            width: 180,
        },
    },
    card_title: {
        fontSize: "1.5rem",
        color: Colours.colours.black,
    },
    card_description: {
        fontSize: "1.0rem",
        color: Colours.colours.gray1,
    },
    card_action: {
        marginTop: 25,
    },
    flag_image: {
        height: 32,
        width: 32,
    },
    button: {
        [theme.breakpoints.down(700)]: {
            marginLeft: "auto",
        },
        [theme.breakpoints.up(700)]: {
            marginLeft: 0,
        },
        "&:hover": {
            color: Colours.colours.white,
            background: Colours.colours.darkViolet1,
        },
        color: Colours.colours.white,
        background: Colours.colours.violet,
    },
}));
