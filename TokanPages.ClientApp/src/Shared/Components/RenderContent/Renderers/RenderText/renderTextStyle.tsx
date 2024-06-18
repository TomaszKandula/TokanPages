import { makeStyles } from "@material-ui/core";
import { Colours } from "../../../../../Theme";

export const RenderTextStyle = makeStyles(() => ({
    common: {
        fontSize: 19,
        textAlign: "left",
        color: Colours.colours.gray3,
    },
    wrapper: {
        display: "flex", 
        alignItems: "center"
    },
    link: {
        cursor: "pointer",
        textAlign: "center",
        verticalAlign: "middle",
        "&:hover": {
            color: Colours.colours.darkViolet1,
        }
    },
    title: {
        fontSize: 28,
        fontWeight: "bold",
        lineHeight: 1.0,
    },
    subTitle: {
        lineHeight: 1.0,
    },
    header: {
        fontSize: 25,
        fontWeight: "bold",
        lineHeight: 1.0,
    },
    paragraph: {
        lineHeight: 2.2,
    },
}));
