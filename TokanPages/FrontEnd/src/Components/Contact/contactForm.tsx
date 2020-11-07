import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import TextField from "@material-ui/core/TextField";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import Checkbox from "@material-ui/core/Checkbox";
import Button from "@material-ui/core/Button";

const useStyles = makeStyles(() => (
{
    section:
    {
        backgroundColor: "#FFFFFF"
    }
}
));

export default function ContactForm() 
{

    const classes = useStyles();

    return (
        <section className={classes.section}>
            <Container maxWidth="lg">
                <Container maxWidth="sm">
                    <div data-aos="fade-up">
                        <Box pt={8} pb={10}>
                            <Box mb={6} textAlign="center">
                                <Typography variant="h4" component="h2" gutterBottom={true}>Contact me</Typography>
                                <Typography variant="subtitle1" color="textSecondary" paragraph={true}>
                                    If you have any questions or you believe that I can do some work for you in technologies I currently work with, send me a message.
                                </Typography>
                            </Box>
                            <Box>
                                <Grid container spacing={2}>
                                    <Grid item xs={12} sm={6}>
                                        <TextField variant="outlined" required fullWidth autoComplete="fname" name="firstName" id="firstName" label="First name" />
                                    </Grid>
                                    <Grid item xs={12} sm={6}>
                                        <TextField variant="outlined" required fullWidth name="lastName" id="lastName" label="Last name" autoComplete="lname" />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <TextField variant="outlined" required fullWidth name="email" id="email" label="Email address" autoComplete="email" />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <TextField variant="outlined" required multiline rows={5} fullWidth autoComplete="message" name="message" id="message" label="Message" />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <FormControlLabel control={<Checkbox name="terms" value="1" color="primary" />} label="I agree to the terms of use and privacy policy." />
                                    </Grid>
                                </Grid>
                                <Box my={2}>
                                    <Button type="submit" fullWidth variant="contained" color="primary">Submit</Button>
                                </Box>
                            </Box>
                        </Box>
                    </div>
                </Container>
            </Container>
        </section>
    );

}
