import { makeStyles } from "@material-ui/core/styles";
import { Colours } from "../../../Theme";

export const ClientsStyle = makeStyles(theme => ({
    caption: {
        textAlign: "center",
        fontSize: "2.0rem",
        color: Colours.colours.darkViolet1,
    },
    section: {
        backgroundColor: Colours.colours.white,
        paddingTop: 42,
        paddingBottom: 42,
        [theme.breakpoints.down(900)]: {
            marginTop: 42,
        },
    },
    logo: {
        height: 48,
        paddingLeft: theme.spacing(4),
        paddingRight: theme.spacing(4),
        marginBottom: theme.spacing(4),
        [theme.breakpoints.down("md")]: {
            paddingLeft: theme.spacing(2),
            paddingRight: theme.spacing(2),
        },
        color: Colours.colours.gray1,
    },
}));
