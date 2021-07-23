import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../../Theme/customColours";

const navigationStyle = makeStyles((theme) => (
{
    appBar:
    {
        background: CustomColours.application.navigationBlue
    },
    toolBar: 
    { 
        justifyContent: "center", 
    },
    logo:
    {        
        width: 210,
    },
    menu: 
    {
        display: "flex",
        justifyContent: "flex-start"
    },
    menuBackground:
    {
        width: 300
    },
    link:
    {
        [theme.breakpoints.down("xs")]:
        {
            justifyContent: "right"
        },
        display: "flex",
        justifyContent: "center"
    },
    image:
    {
        [theme.breakpoints.down("xs")]:
        {
            marginTop: "12px"
        },
        marginTop: "18px"
    },
    avatar:
    {
        [theme.breakpoints.down("xs")]:
        {
            display: "none"
        },
        display: "flex",
        justifyContent: "flex-end"
    },
    userAlias:
    {
        [theme.breakpoints.down("xs")]:
        {
            display: "none"
        },
        color: CustomColours.typography.white,
        alignSelf: "center"
    },
    drawerContainer: 
    {
        width: 300
    },
    nested:
    {
        paddingLeft: theme.spacing(4)
    }
}));

export default navigationStyle;
