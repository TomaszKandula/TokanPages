import { makeStyles } from "@material-ui/core/styles";

const userSigninStyle = makeStyles((theme) => (
{
    tertiaryAction: 
    {
        [theme.breakpoints.up("sm")]: 
        {
            textAlign: "right"
        }
    },
    actions: 
    {
        [theme.breakpoints.down("sm")]: 
        {
            marginTop: theme.spacing(3)
        },
    },
    card:
    {
        marginTop: 10,
        marginLeft: 15,
        marginRight: 15,
        marginBottom: 10
    }
}));

export default userSigninStyle;
