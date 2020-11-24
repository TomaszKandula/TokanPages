import * as React from "react";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import Grid from "@material-ui/core/Grid/Grid";
import useStyles from "./Hooks/styleHeader";
import { IMG_TOMEK } from "../../Shared/constants";

export default function Header() 
{

    const classes = useStyles();
    const content = 
    {
        header: "Welcome to my web page",
        description: "Hello, my name is Tomasz but I usually go by Tom and I do programming for a living...",
        mainAction: "Read the story"
    };

    return (
        <section>
            <Container maxWidth="lg">
                <Grid container className={classes.gridMargin}>
                    <Grid item xs={12} sm={6}>
                        <Box className={classes.imageBox}>
                            <div data-aos="fade-right">
                                <img className={classes.img} src={IMG_TOMEK} alt="" />
                            </div>
                        </Box>
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <Box className={classes.contentBox}>
                            <div data-aos="fade-left">
                                <Typography variant="overline" component="span" gutterBottom={true}>
                                    {content.header}
                                </Typography>
                                <Typography variant="h5" color="textSecondary" paragraph={true}>
                                    {content.description}
                                </Typography>
                                <Box mt={4}>
                                    <Link to="/mystory" className={classes.mainLink}>
                                        <Button variant="contained" className={classes.mainAction}>{content.mainAction}</Button>
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
