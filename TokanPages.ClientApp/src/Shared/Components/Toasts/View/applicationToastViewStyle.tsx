import { makeStyles, Theme } from "@material-ui/core";

export const ApplicationToastViewStyle = makeStyles((theme: Theme) => (
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
