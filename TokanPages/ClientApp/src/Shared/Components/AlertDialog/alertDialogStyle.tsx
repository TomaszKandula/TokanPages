import { makeStyles } from "@material-ui/core/styles";

const alertDialogStyle = makeStyles(() => (
{
    InfoIcon:
    {
        color: "#2196F3",
        marginRight: "15px"
    },
    WarningIcon:
    {
        color: "#FFC107",
        marginRight: "15px"
    },
    ErrorIcon:
    {
        color: "#F50057",
        marginRight: "15px"
    },
    Typography:
    {
        color: "#757575",
    }
}));

export default alertDialogStyle;
