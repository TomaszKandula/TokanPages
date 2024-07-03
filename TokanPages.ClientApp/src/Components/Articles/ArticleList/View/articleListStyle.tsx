import { makeStyles } from "@material-ui/core";
import { Colours } from "../../../../Theme";

export const ArticleListStyle = makeStyles((theme) => ({
    section: {
        backgroundColor: Colours.colours.white,
    },
    container: {
        maxWidth: 700,
    },
    divider: {
        marginTop: 20,
        marginBottom: 10,
    },
    back_arrow: {
        [theme.breakpoints.up(1000)]: {
            display: "none"
        },
        display: "block",
    },
}));
