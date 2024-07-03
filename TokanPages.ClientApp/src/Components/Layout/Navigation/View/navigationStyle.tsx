import { makeStyles } from "@material-ui/core/styles";
import { Colours } from "../../../../Theme";

const smallScreenWeb = 1000;
const smallScreenMobile = 600;
const logoHeightSmall = 30;
const logoHeightLarge = 40;

export const NavigationStyle = makeStyles(theme => ({
    app_logo_small: {
        height: logoHeightSmall,
    },
    app_logo_large: {
        height: logoHeightLarge,
    },
    app_bar: {
        background: Colours.colours.white,
    },
    app_left_logo: {
        height: logoHeightSmall,
        alignSelf: "center",
    },
    app_full_logo: {
        [theme.breakpoints.down(smallScreenMobile)]: {
            display: "none",
        },
        height: logoHeightSmall,
        marginLeft: "auto",
        marginRight: "auto",
        alignSelf: "center",
    },
    app_just_logo: {
        [theme.breakpoints.down(smallScreenMobile)]: {
            display: "block",
        },
        [theme.breakpoints.up(smallScreenMobile)]: {
            display: "none",
        },
        height: logoHeightLarge,
        marginLeft: "auto",
        marginRight: "auto",
        alignSelf: "center",
    },
    app_link: {
        [theme.breakpoints.down(smallScreenMobile)]: {
            justifyContent: "right",
        },
        display: "flex",
        justifyContent: "center",
    },
    tool_bar: {
        display: "flex",
        flexDirection: "row",
        flexWrap: "nowrap",
        justifyContent: "space-between",
    },
    nav_large_screen: {
        [theme.breakpoints.down(smallScreenWeb)]: {
            display: "none",
        },
        [theme.breakpoints.up(smallScreenWeb)]: {
            display: "block",
        },
    },
    nav_small_screen: {
        [theme.breakpoints.down(smallScreenWeb)]: {
            display: "block",
        },
        [theme.breakpoints.up(smallScreenWeb)]: {
            display: "none",
        },
    },
    nav_menu: {
        color: Colours.colours.black,
        display: "flex",
    },
    nav_icon: {
        height: 48,
        marginTop: 6,
        marginBottom: 6,
    },
    nav_items: {
        display: "flex",
        alignItems: "center",
    },
    nav_left: {
        justifyContent: "flex-start",
    },
    nav_right: {
        justifyContent: "flex-end",
    },
    nav_centre: {
        justifyContent: "center",
    },
    languages_wrapper: {
        display: "flex",
        alignItems: "center",
    },
    languagesBox: {
        [theme.breakpoints.down(smallScreenMobile)]: {
            marginRight: 0,
        },
        marginRight: 30,
        alignSelf: "center",
    },
    languages_control: {},
    languages_selection: {
        color: Colours.colours.black,
        width: 75,
    },
    languages_check: {
        paddingLeft: 5,
        color: Colours.colours.darkViolet1,
    },
    user_avatar: {
        [theme.breakpoints.down(smallScreenWeb)]: {
            display: "none",
        },
        display: "flex",
    },
    flag_image: {
        height: 14,
        width: 14,
        marginRight: 5,
    },
}));
