import { makeStyles } from "@material-ui/core/styles";
import { Colours } from "../../../Theme";

export const BusinessFormStyle = makeStyles(() => ({
    section: {
        backgroundColor: Colours.colours.white,
    },
    container: {
        maxWidth: 900,
    },
    large_caption: {
        textAlign: "center",
        fontSize: "2.0rem",
        color: Colours.colours.violet,
    },
    small_caption: {
        fontSize: "1.8rem",
        color: Colours.colours.gray1,
    },
    button: {
        "&:hover": {
            color: Colours.colours.white,
            background: Colours.colours.darkViolet1,
        },
        color: Colours.colours.white,
        background: Colours.colours.violet,
    },
    main_icon: {
        fontSize: 72,
        color: Colours.colours.violet,
    },
    card: {
        borderRadius: 15,
        boxShadow: "0 2px 20px 0 rgb(0 0 0 / 20%)",
    },
    card_content: {
        marginTop: 10,
        marginLeft: 15,
        marginRight: 15,
        marginBottom: 10,
    },
    header: {
        fontSize: 20,
    },
    list_item: {
        paddingTop: 0,
        paddingBottom: 0,
    },
    unselected: {
        border: "solid",
        borderRadius: 10,
        borderWidth: 1,
        borderColor: Colours.colours.lightGray2,
        backgroundColor: Colours.colours.white,
    },
    selected: {
        border: "solid",
        borderRadius: 10,
        borderWidth: 1.5,
        borderColor: Colours.colours.violet,
        backgroundColor: Colours.colours.white,
    },
    disabled: {
        cursor: "wait",
    },
    enabled: {
        cursor: "pointer",
    },
    paper: {
        minHeight: 120,
        padding: 15,
        "&:hover": {
            boxShadow: "0 4px 4px 0 rgb(0 0 0 / 10%)",
        },
    },
    pricing_text: {
        color: Colours.colours.gray2,
    },
    info_box: {
        display: "flex",
        justifyContent: "left",
        flexDirection: "row",
    },
    info_icon: {
        marginRight: 10,
        color: Colours.colours.violet,
    },
}));
