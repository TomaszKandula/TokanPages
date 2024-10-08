import { makeStyles } from "@material-ui/core";
import { Colours } from "../../../../../Theme";

export const RenderSuperTitleStyle = makeStyles(theme => ({
    container: {
        display: "flex",
        flexDirection: "row",
        flexWrap: "wrap",
        justifyContent: "space-between",
    },
    contentText: {
        alignContent: "center",
    },
    contentImage: {
        alignContent: "center",
    },
    common: {
        fontSize: 19,
        textAlign: "left",
        color: Colours.colours.gray3,
    },
    wrapper: {
        display: "flex",
        alignItems: "center",
    },
    card: {
        borderRadius: 0,
        marginTop: 40,
        marginBottom: 40,
    },
    image: {
        display: "block",
        cursor: "cursor",
        maxWidth: 300,
        maxHeight: 60,
        [theme.breakpoints.down(500)]: {
            display: "none",
        },
    },
    title: {
        fontSize: 28,
        fontWeight: "bold",
        lineHeight: 1.0,
    },
    subTitle: {
        lineHeight: 1.0,
    },
}));
