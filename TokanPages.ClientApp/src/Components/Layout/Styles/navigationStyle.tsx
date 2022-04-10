import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../../Theme/customColours";

const navigationStyle = makeStyles((theme) => (
{
    app_bar:
    {
        background: CustomColours.colours.white
    },
    tool_bar: 
    { 
        justifyContent: "center"
    },
    app_full_logo:
    {
        [theme.breakpoints.down(700)]:
        {
            display: "none"
        },
        marginLeft: "auto",
        marginRight: "auto",
        fontSize: "1.5rem",
        fontWeight: 500,
        color: CustomColours.colours.violet,
        cursor: "default",
        alignSelf: "center"
    },
    app_just_logo:
    {
        [theme.breakpoints.down(700)]:
        {
            display: "block"
        },
        [theme.breakpoints.up(700)]:
        {
            display: "none"
        },
        marginLeft: "auto",
        marginRight: "auto",
        fontSize: "1.5rem",
        fontWeight: 500,
        color: CustomColours.colours.violet,
        cursor: "default",
        alignSelf: "center"
    },
    nav_menu: 
    {
        color: CustomColours.colours.violet,
        display: "flex",
        justifyContent: "flex-start"
    },
    app_link:
    {
        [theme.breakpoints.down(700)]:
        {
            justifyContent: "right"
        },
        display: "flex",
        justifyContent: "center"
    },
    user_avatar:
    {
        display: "flex",
        justifyContent: "flex-end"
    },
    user_alias:
    {
        [theme.breakpoints.down(700)]:
        {
            display: "none"
        },
        color: CustomColours.colours.gray1,
        alignSelf: "center"
    }
}));

export default navigationStyle;
