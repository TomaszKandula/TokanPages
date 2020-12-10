import { makeStyles } from "@material-ui/core/styles";

const useStyles = makeStyles((theme) => (
{

    gridMargin:
    {
        marginTop: "65px"
    },
    mainAction: 
    {
        [theme.breakpoints.down("xs")]: 
        {
            width: "100%",
        }
    },
    mainLink:
    {
        textDecoration: "none"
    },
    contentBox: 
    {
        maxWidth: theme.breakpoints.values["md"],
        paddingTop: theme.spacing(12),
        paddingBottom: theme.spacing(8),
        marginLeft: "auto",
        marginRight: "auto",
        textAlign: "left",
        [theme.breakpoints.up("lg")]: 
        {
            maxWidth: theme.breakpoints.values["lg"] / 2,
            maxHeight: 400,
            paddingTop: theme.spacing(12),
            paddingBottom: theme.spacing(16),
            marginRight: 0,
            textAlign: "left",
        },
        [theme.breakpoints.down("xs")]: 
        {
            paddingTop: theme.spacing(3),
        }
    },
    imageBox:
    {
        position: "relative",
        height: 400, 
        display: "flex", 
        justifyContent: "center", 
        alignItems: "center"
    },
    img: 
    {
        borderRadius: "50%",
        border: "25px solid white",
        top: 0,
        left: 0,
        right: 0,
        bottom: 0,
        objectFit: "cover",
        height: 400,
        maxWidth: "100%"
    }
}
));

export default useStyles;
