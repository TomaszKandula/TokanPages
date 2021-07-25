import { makeStyles } from "@material-ui/core/styles";

const menuStyle = makeStyles((theme) => (
{
    menuBackground:
    {
        width: 300
    },   
    drawerContainer: 
    {
        width: 300
    },
    nested:
    {
        paddingLeft: theme.spacing(4)
    }
}));

export default menuStyle;
