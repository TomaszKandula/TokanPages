import { makeStyles } from "@material-ui/core/styles";
import { Colours } from "../../../../Theme";

export const SideMenuStyle = makeStyles(() => ({
    menu_background: {
        width: 300,
    },
    drawer_container: {
        width: 300,
    },
    drawer_hero: {
        height: 200,
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        backgroundColor: Colours.colours.lightViolet,
    },
    drawer_logo: {
        fontSize: "3.5rem",
        fontWeight: 900,
        color: Colours.colours.violet,
        textAlign: "center",
        cursor: "default",
    },
}));
