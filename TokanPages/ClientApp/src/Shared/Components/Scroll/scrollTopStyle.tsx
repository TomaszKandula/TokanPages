import { makeStyles } from "@material-ui/core/styles";

const scrollTopStyle = makeStyles((theme) => (
{
    scrollToTop: 
    {
        position: "fixed",
        bottom: theme.spacing(2),
        right: theme.spacing(2),
    }
}));

export default scrollTopStyle;
