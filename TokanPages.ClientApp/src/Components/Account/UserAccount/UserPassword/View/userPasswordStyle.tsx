import { makeStyles } from "@material-ui/core/styles";
import { Colours } from "../../../../../Theme";

export const UserPasswordStyle = makeStyles(theme => ({
    section: {
        backgroundColor: Colours.colours.white,
    },
    label_centered: {
        display: "flex",
        justifyContent: "center",
        flexDirection: "column"
    },
    label: {
        color: Colours.colours.gray1,
    },
    divider: {
        width: "100%",
        height: 1,
    },
    caption: {
        fontSize: "2.0rem",
        fontWeight: 400,
        color: Colours.colours.black,
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
    button_update: {
        [theme.breakpoints.down("xs")]: {
            width: "100%",
        },
        width: 150,
        "&:hover": {
            color: Colours.colours.white,
            background: Colours.colours.darkViolet1,
        },
        color: Colours.colours.white,
        background: Colours.colours.violet,
    },
    button_container_update: {
        [theme.breakpoints.down("xs")]: {
            marginTop: 30,
            display: "block",
            justifyContent: "normal",
        },
        width: "100%",
        display: "flex",
        flexWrap: "wrap",
        boxSizing: "border-box",
        justifyContent: "flex-end",
    },
}));
