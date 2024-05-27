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
        paddingTop: 50,
        paddingBottom: 50,
    },
    logo: {
        height: 50,
        paddingLeft: "5%",
        paddingRight: "5%",
        marginBottom: theme.spacing(4),
        [theme.breakpoints.down(900)]: {
            paddingLeft: "2%",
            paddingRight: "2%",
        },
        color: Colours.colours.gray1,
    },
}));
