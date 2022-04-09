import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../../Theme/customColours";

const navigationStyle = makeStyles((theme) => (
{
    appBar:
    {
        background: CustomColours.application.navigation
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
        color: CustomColours.colours.white,
        alignSelf: "center"
    }
}));

export default navigationStyle;
