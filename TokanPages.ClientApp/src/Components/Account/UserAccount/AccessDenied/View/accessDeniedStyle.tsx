import { makeStyles } from "@material-ui/core/styles";
import { Colours } from "../../../../../Theme";

export const AccessDeniedStyle = makeStyles(() => (
{
    section: 
    {
        backgroundColor: Colours.colours.white
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
        color: Colours.colours.black
    },
    card:
    {
        borderRadius: 15,
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
        color: Colours.colours.gray1
    },
    home_link:
    {
        textDecoration: "none"
    },
    home_button:
    {
        "&:hover": 
        {
            color: Colours.colours.white,
            background: Colours.colours.darkViolet1,
        },
        color: Colours.colours.white,
        background: Colours.colours.violet
    }
}));
