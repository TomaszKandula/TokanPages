import * as React from "react";
import { Link } from "react-router-dom";
import { Box, Button, Container, Typography } from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";

const useStyles = makeStyles(() => (
{
    mainLink:
    {
        textDecoration: "none"
    }
}));

export default function NotFoundPage()
{
    const classes = useStyles();
    const content = 
    {
        "code": "404",
        "header": "Page not found",
        "description": "The requested page could not be located. Checkout for any URL misspelling.",
        "primary-action": "Return to the homepage"
      };
    
    return (
        <section>
            <Container maxWidth="md">
                <Box pt={8} pb={10} textAlign="center">
                    <Typography variant="h1">{content['code']}</Typography>
                    <Typography variant="h4" component="h2" gutterBottom={true}>{content['header']}</Typography>
                    <Typography variant="subtitle1" color="textSecondary">{content['description']}</Typography>
                    <Box mt={4}>
                        <Link to="/" className={classes.mainLink}>
                            <Button variant="contained" color="primary">{content['primary-action']}</Button>
                        </Link>
                    </Box>
                </Box>
            </Container>
      </section> 
    );

}
