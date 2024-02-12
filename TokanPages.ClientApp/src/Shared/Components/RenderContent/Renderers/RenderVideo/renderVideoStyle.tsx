import { makeStyles } from "@material-ui/core";
import { Colours } from "../../../../../Theme";

export const RenderVideoStyle = makeStyles(() => ({
    card: {
        borderRadius: 0,
        marginTop: 40,
        marginBottom: 40,
    },
    image: {
        cursor: "pointer",
    },
    text: {
        color: Colours.colours.gray1,
        paddingTop: 1,
        paddingBottom: 1,
        paddingLeft: 10,
        paddingRight: 10,
        lineHeight: 1.8,
    },
}));
