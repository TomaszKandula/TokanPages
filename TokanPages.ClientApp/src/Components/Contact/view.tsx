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
import VioletCheckbox from "../../Theme/customCheckboxes";
import contactFormStyle from "./style";

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

export const ContactFormView = (props: IBinding): JSX.Element =>
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
                    <Box pt={8} pb={10}>
                        <Box mb={6} textAlign="center" data-aos="fade-down">
                            <Typography gutterBottom={true} className={classes.caption}>
                                {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.caption?.toUpperCase()}
                            </Typography>
                        </Box>
                        <Box>
                            <Grid container spacing={2}>
                                <Grid item xs={12} sm={6}>
                                    <div data-aos="zoom-in">
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="45px" /> 
                                        : <TextField required fullWidth onChange={props.bind?.formHandler} value={props.bind?.firstName} label={props.bind?.labelFirstName} 
                                            name="firstName" id="firstName" autoComplete="fname" variant="outlined" />}
                                    </div>
                                </Grid>
                                <Grid item xs={12} sm={6}>
                                    <div data-aos="zoom-in">
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="45px" /> 
                                        : <TextField required fullWidth onChange={props.bind?.formHandler} value={props.bind?.lastName} label={props.bind?.labelLastName} 
                                            name="lastName" id="lastName" autoComplete="lname" variant="outlined" />}
                                    </div>
                                </Grid>
                                <Grid item xs={12}>
                                    <div data-aos="zoom-in">
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="45px" /> 
                                        : <TextField required fullWidth onChange={props.bind?.formHandler} value={props.bind?.email} label={props.bind?.labelEmail} 
                                            name="email" id="email" autoComplete="email" variant="outlined" />}
                                    </div>
                                </Grid>
                                <Grid item xs={12}>
                                <div data-aos="zoom-in">
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="45px" /> 
                                        : <TextField required fullWidth onChange={props.bind?.formHandler} value={props.bind?.subject} label={props.bind?.labelSubject} 
                                            name="subject" id="subject" autoComplete="subject" variant="outlined" />}
                                    </div>
                                </Grid>
                                <Grid item xs={12}>
                                <div data-aos="zoom-in">
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="45px" /> 
                                        : <TextField required multiline onChange={props.bind?.formHandler} value={props.bind?.message} label={props.bind?.labelMessage} 
                                            minRows={6} fullWidth autoComplete="message" name="message" id="message" variant="outlined" />}
                                    </div>
                                </Grid>
                                <Grid item xs={12}>
                                    <div data-aos="zoom-in">
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="30px" /> 
                                        : <FormControlLabel control={<VioletCheckbox onChange={props.bind?.formHandler} checked={props.bind?.terms} name="terms" id="terms" />} 
                                            label={props.bind?.consent} />}
                                    </div>
                                </Grid>
                            </Grid>
                            <Box my={2} data-aos="fade-up">
                                {props.bind?.isLoading ? <Skeleton variant="rect" width="100%" height="40px" /> : <ActiveButton />}
                            </Box>
                        </Box>
                    </Box>
                </Container>
            </Container>
        </section>
    );
}
