import React from "react";
import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import TextField from "@material-ui/core/TextField";
import useStyles from "./styledNewsletter";

export default function Newsletter()
{

    const classes = useStyles();
    const content = 
    {
        caption: "Join the newsletter!",
        text: "We will never share your email address.",
        button: "Subscribe",
    };
    
    return (
        <section className={classes.section}>
            <Container maxWidth="lg">
                <div data-aos="fade-up">
                    <Box py={8} textAlign="center">
                        <Grid container spacing={2}>
                            <Grid item xs={12} md={5}>
                                <Typography variant="h4" component="h2">
                                    {content.caption}
                                </Typography>
                                <Typography variant="subtitle1" color="textSecondary">
                                    {content.text}
                                </Typography>
                            </Grid>
                            <Grid item xs={12} md={7}>
                                <Box display="flex" height="100%">
                                    <Box my="auto" width="100%">              
                                        <Grid container spacing={2}>
                                            <Grid item xs={12} sm={7}>
                                                <TextField variant="outlined" required fullWidth size="small" name="email" id="email" label="Email address" autoComplete="email" />
                                            </Grid>
                                            <Grid item xs={12} sm={5}>
                                                <Button type="button" fullWidth variant="contained" color="primary">
                                                    {content.button}
                                                </Button>
                                            </Grid>
                                        </Grid>
                                    </Box>
                                </Box>
                            </Grid>
                        </Grid>
                    </Box>
                </div>
            </Container>
        </section>
    );

}
