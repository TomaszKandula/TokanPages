import { makeStyles } from "@material-ui/core";
import { Colours } from "../../../Theme";

export const PdfViewerStyle = makeStyles(() => ({
    section: {
        backgroundColor: Colours.colours.white,
    },
    header: {
        color: Colours.colours.black,
        fontSize: 14,
        fontWeight: 500,
        background: Colours.colours.white,
        borderStyle: "solid",
        borderWidth: 1,
        borderColor: Colours.colours.gray1,
        display: "flex",
        flexDirection: "row",
        justifyContent: "space-around"
    },
    header_pages: {
        alignSelf: "center",
    },
    header_buttons: {
        alignSelf: "center",
        verticalAlign: "middle",
        marginLeft: 5,
        marginRight: 5,
        cursor: "pointer",
    },
    canvas: {
        padding: 30,
        marginLeft: "auto",
        marginRight: "auto",
        display: "block",
        maxWidth: 800,
        height: "100%",
        width: "100%",
    },
    canvasWrapper: {
        backgroundColor: Colours.colours.gray1,
    },
}));
