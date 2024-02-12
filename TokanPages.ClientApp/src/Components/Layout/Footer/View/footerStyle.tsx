import { makeStyles } from "@material-ui/core/styles";
import { Colours } from "../../../../Theme";

export const FooterStyle = makeStyles(theme => ({
    page_footer: {
        [theme.breakpoints.down("md")]: {
            textAlign: "center",
        },
        background: Colours.colours.violet,
    },
    icon_box: {
        [theme.breakpoints.down("md")]: {
            width: "100%",
            marginBottom: theme.spacing(0),
        },
    },
    icon: {
        color: Colours.colours.white,
    },
    copyright_box: {
        display: "flex",
        flexWrap: "wrap",
        alignItems: "center",
    },
    copyright: {
        fontSize: "1.2rem",
        color: Colours.colours.white,
        [theme.breakpoints.down("md")]: {
            width: "100%",
            order: 12,
        },
    },
    links: {
        color: Colours.colours.white,
        textDecoration: "none",
    },
    version: {
        color: Colours.colours.white,
    },
}));
