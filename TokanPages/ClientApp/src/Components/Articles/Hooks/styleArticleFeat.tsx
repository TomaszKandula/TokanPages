import { makeStyles } from "@material-ui/core/styles";

const useStyles = makeStyles((theme) => (
{
    section:
    {
        backgroundColor: "#FFFFFF"
    },
    info: 
    {
        height: "100%",
        minHeight: "128px",
    },
    media: 
    {
        height: "128px",
    },
    firstColumn: 
    {
        paddingRight: theme.spacing(2),
        [theme.breakpoints.down("md")]: 
        {
            marginBottom: theme.spacing(2),
            paddingRight: theme.spacing(0),
        }
    },
    link:
    {
        textDecoration: "none"
    }
}));

export default useStyles;
