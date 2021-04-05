import { makeStyles, Theme } from "@material-ui/core";

const useStyles = makeStyles((theme: Theme) => (
{
    root: 
    {
        width: "100%",
        "& > * + *": 
        {
            marginTop: theme.spacing(2),
        },
    }
}));

export default useStyles;
