import { makeStyles } from "@material-ui/core";
import { Colours } from "../../../Theme";

export const PdfViewerStyle = makeStyles(() => ({
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
    header: {
        borderStyle: "solid",
        borderWidth: 1,
        borderColor: Colours.colours.gray1,
        textAlign: "center",
        marginTop: 25,
        padding: 5,
    },
}));
