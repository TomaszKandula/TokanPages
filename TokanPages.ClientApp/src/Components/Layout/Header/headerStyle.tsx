import { makeStyles } from "@material-ui/core/styles";
import { Colours } from "../../../Theme";

export const HeaderStyle = makeStyles(theme => ({
    section: {
        backgroundColor: Colours.colours.white,
    },
    section_container: {
        display: "flex",
        flexDirection: "column",
    },
    content_box: {
        marginTop: "auto",
        marginRight: 30,
        paddingTop: 60,
        paddingBottom: 60,
        paddingLeft: 80,
        backgroundColor: Colours.colours.white,
        [theme.breakpoints.down(1700)]: {
            maxWidth: 600,
            marginLeft: "-33%",
        },
        [theme.breakpoints.up(1700)]: {
            maxWidth: 600,
            marginLeft: "-33%",
        },
        [theme.breakpoints.down(900)]: {
            maxWidth: 900,
            padding: 15,
            marginLeft: 0,
            marginRight: 0,
        },
    },
    content_caption: {
        color: Colours.colours.gray3,
        fontSize: "3.4rem",
        fontWeight: 700,
        lineHeight: 1.5,
    },
    content_subtitle: {
        color: Colours.colours.gray3,
        fontSize: "1.4rem",
        fontWeight: 700,
        lineHeight: 1.5,
    },
    content_description: {
        color: Colours.colours.gray3,
        fontSize: "1.125rem",
        fontWeight: 400,
        lineHeight: 1.5,
    },
    image_skeleton: {
        height: 300,
        width: 300,
    },
    image_box: {
        position: "relative",
        height: 400,
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
    },
    image_card: {
        height: 700,
        display: "flex",
        flexDirection: "column",
    },
    action_button: {
        "&:hover": {
            color: Colours.colours.white,
            background: Colours.colours.darkViolet1,
        },
        color: Colours.colours.white,
        background: Colours.colours.violet,
        [theme.breakpoints.down("xs")]: {
            width: "100%",
        },
    },
    action_link: {
        textDecoration: "none",
    },
    resume_button: {
        "&:hover": {
            color: Colours.colours.white,
            background: Colours.colours.gray3,
        },
        color: Colours.colours.white,
        background: Colours.colours.gray1,
        marginTop: 0,
        marginLeft: 15,        
        [theme.breakpoints.down("xs")]: {
            width: "100%",
            marginTop: 15,
            marginLeft: 0,
        },
    },
}));
