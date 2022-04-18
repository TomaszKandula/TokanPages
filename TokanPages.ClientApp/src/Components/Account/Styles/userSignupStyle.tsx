import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../../Theme/customColours";

const userSignupStyle = makeStyles(() => (
{
    account:
    {
        fontSize: 72,
        color: CustomColours.colours.violet
    },
    caption:
    {
        fontSize: "1.8rem",
        color: CustomColours.colours.gray1
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
    button:
    {
        "&:hover": 
        {
            color: CustomColours.colours.white,
            background: CustomColours.colours.darkViolet1,
        },
        color: CustomColours.colours.white,
        background: CustomColours.colours.violet,
    }
}));

export default userSignupStyle;
