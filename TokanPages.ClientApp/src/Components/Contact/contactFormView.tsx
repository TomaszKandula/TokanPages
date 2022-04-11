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
    labelFirstName: string;
    labelLastName: string;
    labelEmail: string;
    labelSubject: string;
    labelMessage: string;
}

const ContactFormView = (props: IBinding): JSX.Element =>
{
    const classes = contactFormStyle();

    const ActiveButton = (): JSX.Element => 
    {
        return(
            <Button fullWidth onClick={props.bind?.buttonHandler} type="submit" variant="contained" 
                disabled={props.bind?.progress} className={classes.button}>
                {props.bind?.progress &&  <CircularProgress size={20} />}
                {!props.bind?.progress && props.bind?.buttonText}
            </Button>
        );
    }

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
                                            onChange={props.bind?.formHandler} value={props.bind?.firstName} label={props.bind?.labelFirstName} 
                                            required fullWidth name="firstName" id="firstName" autoComplete="fname" variant="outlined"
                                        />
                                    </Grid>
                                    <Grid item xs={12} sm={6}>
                                        <TextField 
                                            onChange={props.bind?.formHandler} value={props.bind?.lastName} label={props.bind?.labelLastName} 
                                            required fullWidth name="lastName" id="lastName" autoComplete="lname" variant="outlined" 
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <TextField 
                                            onChange={props.bind?.formHandler} value={props.bind?.email} label={props.bind?.labelEmail} 
                                            required fullWidth name="email" id="email" autoComplete="email" variant="outlined" 
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <TextField 
                                            onChange={props.bind?.formHandler} value={props.bind?.subject} label={props.bind?.labelSubject} 
                                            required fullWidth name="subject" id="subject" autoComplete="subject" variant="outlined" 
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <TextField 
                                            onChange={props.bind?.formHandler} value={props.bind?.message} label={props.bind?.labelMessage} 
                                            required multiline minRows={6} fullWidth autoComplete="message" name="message" id="message" variant="outlined" 
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
                                    {props.bind?.isLoading ? <Skeleton variant="rect" /> : <ActiveButton />}
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
