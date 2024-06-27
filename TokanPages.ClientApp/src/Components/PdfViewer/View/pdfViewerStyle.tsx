import { makeStyles } from "@material-ui/core";
import { Colours } from "../../../Theme";

export const PdfViewerStyle = makeStyles(() => ({
    section: {
        backgroundColor: Colours.colours.gray1,
    },
    header: {
        color: Colours.colours.lightGray2,
        fontSize: 14,
        fontWeight: 500,
        borderStyle: "solid",
        borderWidth: 1,
        borderColor: Colours.colours.gray1,
        textAlign: "center",
    },
    canvas: {
        paddingLeft: 15,
        paddingRight: 15,
        marginLeft: "auto",
        marginRight: "auto",
        display: "block",
        maxWidth: 800,
        height: "100%",
        width: "100%",
    },
}));
