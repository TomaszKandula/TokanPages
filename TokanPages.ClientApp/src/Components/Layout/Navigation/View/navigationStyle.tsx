import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../../../Theme/customColours";

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
        color: CustomColours.colours.gray1,
        display: "flex",
        justifyContent: "flex-start"
    },
    nav_icon:
    {
        height: "48px",
        marginTop: "6px",
        marginBottom: "6px"
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
    languagesBox:
    {
        [theme.breakpoints.down(700)]:
        {
            marginRight: "0px",
        },
        marginRight: "30px",
        alignSelf: "center"
    },
    languages_selection:
    {
        color: CustomColours.colours.gray1
    },
    languages_menu:
    {
        color: CustomColours.colours.violet
    },
    content_right_side:
    {
        display: "flex",
        justifyContent: "flex-end"
    },
    user_avatar:
    {
        [theme.breakpoints.down(700)]:
        {
            display: "none"
        },
        display: "flex"
    }
}));

export default navigationStyle;
