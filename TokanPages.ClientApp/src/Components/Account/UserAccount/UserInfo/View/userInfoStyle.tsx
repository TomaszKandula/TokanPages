import { withStyles, makeStyles } from "@material-ui/core/styles";
import { purple } from "@material-ui/core/colors";
import Switch from "@material-ui/core/Switch";
import { Colours } from "../../../../../Theme";

export const UserInfoStyle = makeStyles(theme => ({
    section: {
        backgroundColor: Colours.colours.white,
    },
    container: {
        maxWidth: 900,
    },
    backdrop: {
        zIndex: theme.zIndex.drawer + 1,
        color: "#FFFFFF",
    },
    label_centered: {
        display: "flex",
        justifyContent: "center",
        flexDirection: "column",
    },
    label: {
        color: Colours.colours.gray1,
    },
    divider: {
        width: "100%",
        height: "1px",
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
    user_id: {
        [theme.breakpoints.down("xs")]: {
            marginBottom: 15,
        },
        marginBottom: 30,
    },
    user_alias: {
        [theme.breakpoints.down("xs")]: {
            marginBottom: 15,
        },
        marginBottom: 30,
    },
    user_email_status: {
        [theme.breakpoints.down("xs")]: {
            marginBottom: 4,
        },
        marginBottom: 0,
    },
    user_email_verification: {
        cursor: "pointer",
        color: Colours.colours.red,
        textDecoration: "underline",
    },
    user_avatar_text: {
        marginLeft: 15,
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
    button_upload: {
        "&:hover": {
            color: Colours.colours.white,
            background: Colours.colours.darkViolet1,
        },
        color: Colours.colours.white,
        background: Colours.colours.violet,
    },
}));

export const CustomSwitchStyle = withStyles({
    switchBase: {
        color: purple[300],
        "&$checked": {
            color: purple[500],
        },
        "&$checked + $track": {
            backgroundColor: purple[500],
        },
    },
    checked: {},
    track: {},
})(Switch);
