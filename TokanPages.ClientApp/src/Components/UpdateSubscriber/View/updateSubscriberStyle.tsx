import { makeStyles } from "@material-ui/core/styles";
import { Colours } from "../../../Theme";

export const UpdateSubscriberStyle = makeStyles(() => (
{
    section:
    {
        backgroundColor: Colours.colours.white
    },
    account:
    {
        fontSize: 72,
        color: Colours.colours.violet
    },
    caption:
    {
        fontSize: "1.8rem",
        color: Colours.colours.gray1
    },
    card:
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
