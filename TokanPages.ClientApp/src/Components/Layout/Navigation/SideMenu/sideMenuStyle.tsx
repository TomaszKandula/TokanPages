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
    logo: {
        height: 120,
        marginLeft: "auto",
        marginRight: "auto",
        alignSelf: "center",
    },
}));
