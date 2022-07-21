import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../../../Theme/customColours";

export const UserAccountStyle = makeStyles((theme) => (
{
    section: 
    {
        backgroundColor: CustomColours.colours.white
    },
    label:
    {
        color: CustomColours.colours.gray1
    },
    divider:
    {
        width: "100%",
        height: "1px"
    },
    caption:
    {
        fontSize: "2.0rem",
        fontWeight: 400,
        color: CustomColours.colours.black
    },
    card:
    {
        borderRadius: "15px",
        boxShadow: "0 2px 20px 0 rgb(0 0 0 / 20%)"
    },
    card_content:
    {
        marginTop: 10,
        marginLeft: 15,
        marginRight: 15,
        marginBottom: 10
    },
    access_denied_prompt:
    {
        color: CustomColours.colours.gray1
    },
    user_id:
    {
        [theme.breakpoints.down("xs")]: 
        {
            marginBottom: "15px"
        },
        marginBottom: "30px"
    },
    user_alias: 
    {
        [theme.breakpoints.down("xs")]: 
        {
            marginBottom: "15px"
        },
        marginBottom: "30px"
    },
    user_avatar_box:
    {
        [theme.breakpoints.down("xs")]: 
        {
            marginBottom: "15px"
        },
        marginBottom: "30px"

    },
    user_avatar:
    {
        marginRight: "15px"
    },
    button_update:
    {
        [theme.breakpoints.down("xs")]: 
        {
            width: "100%",
        },
        width: "150px",
        "&:hover": 
        {
            color: CustomColours.colours.white,
            background: CustomColours.colours.darkViolet1,
        },
        color: CustomColours.colours.white,
        background: CustomColours.colours.violet
    },
    delete_update:
    {
        [theme.breakpoints.down("xs")]: 
        {
            width: "100%",
        },
        width: "150px",
        "&:hover": 
        {
            color: CustomColours.colours.white,
            background: CustomColours.colours.red,
        },
        color: CustomColours.colours.white,
        background: CustomColours.colours.redDark
    },
    button_container_update:
    {
        [theme.breakpoints.down("xs")]: 
        {
            marginTop: "30px",
            display: "block",
            justifyContent: "normal"
        },
        width: "100%",
        display: "flex",
        flexWrap: "wrap",
        boxSizing: "border-box",
        justifyContent: "flex-end"
    },
    button_upload:
    {
        "&:hover": 
        {
            color: CustomColours.colours.white,
            background: CustomColours.colours.darkViolet1,
        },
        color: CustomColours.colours.white,
        background: CustomColours.colours.violet
    },
    home_link:
    {
        textDecoration: "none"
    },
    home_button:
    {
        "&:hover": 
        {
            color: CustomColours.colours.white,
            background: CustomColours.colours.darkViolet1,
        },
        color: CustomColours.colours.white,
        background: CustomColours.colours.violet
    }
}));
