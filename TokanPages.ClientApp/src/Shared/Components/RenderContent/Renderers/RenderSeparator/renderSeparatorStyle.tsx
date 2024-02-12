import { makeStyles } from "@material-ui/core";
import { Colours } from "../../../../../Theme";

export const RenderSeparatorStyle = makeStyles(() => ({
    separator: {
        textAlign: "center",
        marginTop: 50,
        marginBottom: 50,
    },
    span: {
        width: 5,
        height: 5,
        borderRadius: "50%",
        background: Colours.colours.gray1,
        display: "inline-block",
        margin: "0 10px",
    },
}));
