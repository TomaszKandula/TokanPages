import React from "react";
import { Link } from "react-router-dom";
import { makeStyles } from "@material-ui/core/styles";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import Grid from "@material-ui/core/Grid/Grid";

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

export default function Header(props: any) 
{

    const classes = useStyles();

    const content = 
    {
        "header": "Welcome to my web page",
        "description": "Hello, my name is Tomasz but I usually go by Tom and I do programming for a living...",
        "main-action": "Read the story",
        "image": "https://maindbstorage.blob.core.windows.net/tokanpages/images/tomek_bergen.jpg",
        ...props.content
    };

    return (
        <section>
            <Container maxWidth="lg">
                <Grid container className={classes.gridMargin}>
                    <Grid item xs={12} sm={6}>
                        <Box className={classes.imageBox}>
                            <div data-aos="fade-right">
                                <img className={classes.img} src="https://maindbstorage.blob.core.windows.net/tokanpages/images/tomek_bergen.jpg" alt="" />
                            </div>
                        </Box>
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <Box className={classes.contentBox}>
                            <div data-aos="fade-left">
                                <Typography variant="overline" component="span" gutterBottom={true}>{content['header']}</Typography>
                                <Typography variant="h5" color="textSecondary" paragraph={true}>{content['description']}</Typography>
                                <Box mt={4}>
                                    <Link to="/mystory" className={classes.mainLink}>
                                        <Button variant="contained" className={classes.mainAction}>{content['main-action']}</Button>
                                    </Link>
                                </Box>
                            </div>
                        </Box>
                    </Grid>
                </Grid>
            </Container>
        </section>
	);

}
