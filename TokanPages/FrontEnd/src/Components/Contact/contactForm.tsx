import * as React from "react";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import TextField from "@material-ui/core/TextField";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import Checkbox from "@material-ui/core/Checkbox";
import Button from "@material-ui/core/Button";
import useStyles from "./styleContactForm";
import { ValidateInputs } from "./validateInputs";

export default function ContactForm() 
{

    const classes = useStyles();

    const [Form, setForm] = React.useState({ firstName: "", lastName: "", email: "", subject: "", message: "", terms: false });
    const FormHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    {

        if (event.currentTarget.name !== "terms")
        {
            setForm({ ...Form, [event.currentTarget.name]: event.currentTarget.value});
        }
        else
        {
            setForm({ ...Form, [event.currentTarget.name]: event.currentTarget.checked});
        }

    }

    const onClickEvent = () => 
    {

        let Results = ValidateInputs( 
        { 
            FirstName: Form.firstName,
            LastName:  Form.lastName, 
            Email:     Form.email, 
            Subject:   Form.subject, 
            Message:   Form.message, 
            Terms:     Form.terms 
        });

        // if valid, then call API to send message (display busy screen)
        // if not valid, display modal window with error

        console.log(Results);

    }

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
                                        <TextField onChange={FormHandler} variant="outlined" required fullWidth name="firstName" id="firstName" label="First name" autoComplete="fname" />
                                    </Grid>
                                    <Grid item xs={12} sm={6}>
                                        <TextField onChange={FormHandler} variant="outlined" required fullWidth name="lastName" id="lastName" label="Last name" autoComplete="lname" />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <TextField onChange={FormHandler} variant="outlined" required fullWidth name="email" id="email" label="Email address" autoComplete="email" />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <TextField onChange={FormHandler} variant="outlined" required fullWidth name="subject" id="subject" label="Subject" autoComplete="subject" />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <TextField onChange={FormHandler} variant="outlined" required multiline rows={6} fullWidth autoComplete="message" name="message" id="message" label="Message" />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <FormControlLabel control={<Checkbox onChange={FormHandler} name="terms" id="terms" color="primary" />} label="I agree to the terms of use and privacy policy." />
                                    </Grid>
                                </Grid>
                                <Box my={2}>
                                    <Button onClick={onClickEvent} type="submit" fullWidth variant="contained" color="primary">Submit</Button>
                                </Box>
                            </Box>
                        </Box>
                    </div>
                </Container>
            </Container>
        </section>
    );

}
