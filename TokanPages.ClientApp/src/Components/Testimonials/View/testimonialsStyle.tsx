import { makeStyles } from "@material-ui/core/styles";
import { Colours } from "../../../Theme";

export const TestimonialsStyle = makeStyles(theme => ({
    section: {
        backgroundColor: Colours.colours.white,
    },
    caption_text: {
        textAlign: "center",
        fontSize: "2.0rem",
        color: Colours.colours.darkViolet1,
    },
    card: {
        minHeight: 340,
        borderRadius: 15,
        boxShadow: "0 2px 20px 0 rgb(0 0 0 / 20%)",
    },
    card_space: {
        [theme.breakpoints.down("md")]: {
            marginTop: 45,
        },
        [theme.breakpoints.up("md")]: {
            marginTop: 0,
        },
    },
    card_image: {
        height: "140px",
        width: "140px",
        borderRadius: "50%",
        top: 0,
        left: "50%",
        margin: 0,
        transform: "translate(-50%, -33%)",
        overflow: "hidden",
        position: "absolute",
        verticalAlign: "middle",
    },
    card_content: {
        marginTop: 70,
    },
    card_title: {
        textAlign: "center",
        fontSize: "1.5rem",
        fontWeight: 700,
        lineHeight: "2.2",
        color: Colours.colours.black,
    },
    card_subheader: {
        textAlign: "center",
        fontSize: "1.0rem",
        lineHeight: "2.0",
        color: Colours.colours.darkViolet1,
    },
    card_text: {
        textAlign: "left",
        lineHeight: "1.8",
        marginTop: 15,
        color: Colours.colours.gray1,
    },
}));
