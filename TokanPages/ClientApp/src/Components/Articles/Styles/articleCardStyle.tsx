import { makeStyles } from "@material-ui/core/styles";

const articleCardStyle = makeStyles((theme) => (
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
    },
    title:
    {
        [theme.breakpoints.down("md")]:
        {
            fontSize: "1.2rem"
        },
        [theme.breakpoints.down("sm")]:
        {
            fontSize: "1.0rem"
        }
    },
    description:
    {
        [theme.breakpoints.down("md")]:
        {
            fontSize: "0.85rem"
        },
        [theme.breakpoints.down("sm")]:
        {
            fontSize: "0.75rem"
        }
    }
}));

export default articleCardStyle;
