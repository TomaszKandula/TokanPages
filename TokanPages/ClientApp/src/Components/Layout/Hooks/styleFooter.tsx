import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../../Theme/customColours";

const useStyles = makeStyles((theme) => (
{
    root: 
    {
        [theme.breakpoints.down("md")]: 
        {
            textAlign: "center"
        },
    },
    iconsBoxRoot: 
    {
        [theme.breakpoints.down("md")]: 
        {
            width: "100%",
            marginBottom: theme.spacing(0),
        }
    }, 
    copy: 
    {
        color: CustomColours.typography.gray2,
        [theme.breakpoints.down("md")]: 
        {
            width: "100%",
            order: 12,
        }
    },
    links:
    {
        color: CustomColours.typography.gray2,
        textDecoration: "none"
    }
}));

export default useStyles;
