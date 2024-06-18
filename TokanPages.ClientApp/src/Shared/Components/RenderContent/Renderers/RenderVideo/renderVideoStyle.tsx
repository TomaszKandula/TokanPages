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
        fontSize: 14,
        lineHeight: 1.8,
        color: Colours.colours.gray1,
    },
}));
