import { makeStyles } from "@material-ui/core/styles";
import { Colours } from "../../../../Theme";

export const ActivateAccountStyle = makeStyles(() => (
{
    section:
    {
        backgroundColor: Colours.colours.white
    },
    caption:
    {
        fontSize: "2.0rem",
        fontWeight: 400,
        color: Colours.colours.black
    },
    text1:
    {
        fontSize: "1.2rem",
        fontWeight: 500,
        color: Colours.colours.gray1
    },
    text2:
    {
        fontSize: "1.2rem",
        fontWeight: 400,
        color: Colours.colours.gray1
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
    button:
    {
        "&:hover": 
        {
            color: Colours.colours.white,
            background: Colours.colours.darkViolet1,
        },
        color: Colours.colours.white,
        background: Colours.colours.violet,
    }
}));
