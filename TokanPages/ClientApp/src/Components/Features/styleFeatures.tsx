import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../Theme/customColours";

const useStyles = makeStyles((theme) => (
{
    section:
    {
        backgroundColor: CustomColours.background.lightGray1
    },
    icon: 
    {
        marginRight: theme.spacing(1),
    }
}
));

export default useStyles;
