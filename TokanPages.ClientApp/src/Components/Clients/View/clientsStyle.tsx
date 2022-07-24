import { makeStyles } from "@material-ui/core/styles";
import { Colours } from "../../../Theme";

export const ClientsStyle = makeStyles((theme) => (
{
    divider:
    {
        marginTop: "42px",
        height: "2px",
        backgroundColor: Colours.colours.lightGray1
    },
    caption:
    {
        textAlign: "center",
        fontSize: "2.0rem",
        color: Colours.colours.darkViolet1
    },
    section:
    {
        backgroundColor: Colours.colours.white,
        marginTop: "42px",
        marginBottom: "42px"
    },
    logo:
    {
        height: "48px",
        paddingLeft: theme.spacing(4),
        paddingRight: theme.spacing(4),
        marginBottom: theme.spacing(4),
        [theme.breakpoints.down("md")]: 
        {
            paddingLeft: theme.spacing(2),
            paddingRight: theme.spacing(2)
        },
        color: Colours.colours.gray1   
    }
}
));
