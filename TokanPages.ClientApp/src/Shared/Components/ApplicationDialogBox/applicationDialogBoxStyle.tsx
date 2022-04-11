import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../../Theme/customColours";

const ApplicationDialogBoxStyle = makeStyles(() => (
{
    icon_holder:
    {
        display: "flex", 
        alignItems: "center"
    },
    info_icon:
    {
        color: "#2196F3",
        marginRight: "15px"
    },
    warning_icon:
    {
        color: "#FFC107",
        marginRight: "15px"
    },
    error_icon:
    {
        color: "#F50057",
        marginRight: "15px"
    },
    title:
    {
        fontSize: "1.5rem",
        color: CustomColours.colours.black
    },
    description:
    {
        fontSize: "1.0rem",
        color: CustomColours.colours.gray1
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

export default ApplicationDialogBoxStyle;
