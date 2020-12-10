import { makeStyles } from "@material-ui/core/styles";

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
        color: "#757575",
        [theme.breakpoints.down("md")]: 
        {
            width: "100%",
            order: 12,
        }
    },
    links:
    {
        color: "#757575",
        textDecoration: "none"
    }
}));

export default useStyles;
