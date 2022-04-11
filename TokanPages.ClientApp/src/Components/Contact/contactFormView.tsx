import * as React from "react";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import TextField from "@material-ui/core/TextField";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import Button from "@material-ui/core/Button";
import { CircularProgress } from "@material-ui/core";
import Skeleton from "@material-ui/lab/Skeleton";
import contactFormStyle from "./contactFormStyle";
import VioletCheckbox from "../../Theme/customCheckboxes";

interface IBinding 
{
    bind: IProperties;
}

interface IProperties
{
    isLoading: boolean;
    caption: string;
    text: string;
    formHandler: any;
    firstName: string;
    lastName: string;
    email: string;
    subject: string;
    message: string;
    terms?: boolean;
    buttonHandler: any;
    progress: boolean;
    buttonText: string;
    consent: string;
}

const ContactFormView = (props: IBinding): JSX.Element =>
{
    const classes = contactFormStyle();
    return (
        <section className={classes.section}>
            <Container maxWidth="lg">
                <Container maxWidth="sm">
                    <div data-aos="fade-up">
                        <Box pt={8} pb={10}>
                            <Box mb={6} textAlign="center">
                                <Typography gutterBottom={true} className={classes.caption}>
                                    {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.caption?.toUpperCase()}
                                </Typography>
                            </Box>
                            <Box>
                                <Grid container spacing={2}>
                                    <Grid item xs={12} sm={6}>
                                        <TextField 
                                            onChange={props.bind?.formHandler} value={props.bind?.firstName} variant="outlined" 
                                            required fullWidth name="firstName" id="firstName" label="First name" autoComplete="fname" 
                                        />
                                    </Grid>
                                    <Grid item xs={12} sm={6}>
                                        <TextField 
                                            onChange={props.bind?.formHandler} value={props.bind?.lastName} variant="outlined" 
                                            required fullWidth name="lastName" id="lastName" label="Last name" autoComplete="lname" 
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <TextField 
                                            onChange={props.bind?.formHandler} value={props.bind?.email} variant="outlined" 
                                            required fullWidth name="email" id="email" label="Email address" autoComplete="email" 
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <TextField 
                                            onChange={props.bind?.formHandler} value={props.bind?.subject} variant="outlined" 
                                            required fullWidth name="subject" id="subject" label="Subject" autoComplete="subject" 
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <TextField 
                                            onChange={props.bind?.formHandler} value={props.bind?.message} variant="outlined" 
                                            required multiline minRows={6} fullWidth autoComplete="message" name="message" id="message" label="Message" 
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <FormControlLabel 
                                            control={<VioletCheckbox onChange={props.bind?.formHandler} checked={props.bind?.terms} name="terms" id="terms" />} 
                                            label={props.bind?.consent} 
                                        />
                                    </Grid>
                                </Grid>
                                <Box my={2}>
                                    <Button fullWidth onClick={props.bind?.buttonHandler} type="submit" variant="contained" disabled={props.bind?.progress} className={classes.button}>
                                        {props.bind?.progress &&  <CircularProgress size={20} />}
                                        {!props.bind?.progress && props.bind?.buttonText}
                                    </Button>
                                </Box>
                            </Box>
                        </Box>
                    </div>
                </Container>
            </Container>
        </section>
    );
}

export default ContactFormView;
