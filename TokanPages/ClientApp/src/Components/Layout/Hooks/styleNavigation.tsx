import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../../Theme/customColours";

const useStyles = makeStyles(() => (
{
    appBar:
    {
        background: CustomColours.application.navigationBlue
    },
    toolBar: 
    { 
        justifyContent: "center", 
    },
    mainLogo:
    {
        width: 210,
    },
    mainLink:
    {
        marginTop: "10px",
        variant:"h5", 
        color: "inherit", 
        underline: "none"
    }
}));

export default useStyles;
