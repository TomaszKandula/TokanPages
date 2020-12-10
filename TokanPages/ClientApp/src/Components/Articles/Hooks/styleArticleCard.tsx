import { makeStyles } from "@material-ui/core/styles";

const useStyles = makeStyles(() => (
{
    root: 
    {
        marginTop: 25,
        marginBottom: 25
    },
    media: 
    {
        height: 140,
    },
    link:
    {
        textDecoration: "none"
    },
    img:
    {
        margin: "auto",
        display: "block",
        objectFit: "cover",
        height: 150,
        maxWidth: "100%"
    }
}));

export default useStyles;
