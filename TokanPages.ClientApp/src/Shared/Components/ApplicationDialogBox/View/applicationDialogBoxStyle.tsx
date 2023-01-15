import { makeStyles } from "@material-ui/core/styles";
import { Colours } from "../../../../Theme";

export const ApplicationDialogBoxStyle = makeStyles(() => (
{
    icon_holder:
    {
        display: "flex", 
        alignItems: "center"
    },
    info_icon:
    {
        color: "#2196F3",
        marginRight: 15
    },
    warning_icon:
    {
        color: "#FFC107",
        marginRight: 15
    },
    error_icon:
    {
        color: "#F50057",
        marginRight: 15
    },
    title:
    {
        fontSize: "1.5rem",
        color: Colours.colours.black
    },
    description:
    {
        fontSize: "1.0rem",
        color: Colours.colours.gray1
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
