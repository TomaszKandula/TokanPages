import { makeStyles } from "@material-ui/core/styles";
import { Colours } from "../../../../Theme";

export const NavigationStyle = makeStyles(theme => ({
    app_bar: {
        background: Colours.colours.white,
    },
    tool_bar: {
        justifyContent: "center",
    },
    app_full_logo: {
        [theme.breakpoints.down(700)]: {
            display: "none",
        },
        marginLeft: "auto",
        marginRight: "auto",
        fontSize: "1.5rem",
        fontWeight: 500,
        color: Colours.colours.violet,
        cursor: "default",
        alignSelf: "center",
    },
    app_just_logo: {
        [theme.breakpoints.down(700)]: {
            display: "block",
        },
        [theme.breakpoints.up(700)]: {
            display: "none",
        },
        marginLeft: "auto",
        marginRight: "auto",
        fontSize: "1.5rem",
        fontWeight: 500,
        color: Colours.colours.violet,
        cursor: "default",
        alignSelf: "center",
    },
    nav_menu: {
        color: Colours.colours.gray1,
        display: "flex",
        justifyContent: "flex-start",
    },
    nav_icon: {
        height: 48,
        marginTop: 6,
        marginBottom: 6,
    },
    app_link: {
        [theme.breakpoints.down(700)]: {
            justifyContent: "right",
        },
        display: "flex",
        justifyContent: "center",
    },
    flag_image: {
        height: 14,
        width: 14,
        marginRight: 5,
    },
    languagesBox: {
        [theme.breakpoints.down(700)]: {
            marginRight: 0,
        },
        marginRight: 30,
        alignSelf: "center",
    },
    languages_control: {
    },
    languages_selection: {
        color: Colours.colours.gray1,
        width: 120,
    },
    content_right_side: {
        display: "flex",
        justifyContent: "flex-end",
    },
    user_avatar: {
        [theme.breakpoints.down(700)]: {
            display: "none",
        },
        display: "flex",
    },
}));
