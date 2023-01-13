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
import { VioletCheckbox } from "../../../Theme";
import { ContactFormStyle } from "./contactFormStyle";

interface IBinding 
{
    bind: IProperties;
}

interface IProperties
{
    isLoading: boolean;
    caption: string;
    text: string;
    keyHandler: any;
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

const ActiveButton = (props: IBinding): JSX.Element => 
{
    const classes = ContactFormStyle();
    return(
        <Button 
            fullWidth 
            type="submit" 
            variant="contained" 
            onClick={props.bind?.buttonHandler} 
            disabled={props.bind?.progress} 
            className={classes.button}>
            {!props.bind?.progress 
            ? props.bind?.buttonText 
            : <CircularProgress size={20} />}
        </Button>
    );
}

export const ContactFormView = (props: IBinding): JSX.Element =>
{
    const classes = ContactFormStyle();
    return (
        <section className={classes.section}>
            <Container maxWidth="lg">
                <Container maxWidth="sm">
                    <Box pt={8} pb={10}>
                        <Box mb={6} textAlign="center" data-aos="fade-down">
                            <Typography gutterBottom={true} className={classes.caption}>
                                {props.bind?.isLoading 
                                ? <Skeleton variant="text" /> 
                                : props.bind?.caption?.toUpperCase()}
                            </Typography>
                        </Box>
                        <Box>
                            <Grid container spacing={2}>
                                <Grid item xs={12} sm={6}>
                                    <div data-aos="zoom-in">
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="45px" /> 
                                        : <TextField 
                                            required 
                                            fullWidth 
                                            id="firstName" 
                                            name="firstName" 
                                            autoComplete="fname" 
                                            variant="outlined"
                                            onKeyDown={props.bind?.keyHandler}
                                            onChange={props.bind?.formHandler} 
                                            value={props.bind?.firstName} 
                                            label={props.bind?.labelFirstName} 
                                        />}
                                    </div>
                                </Grid>
                                <Grid item xs={12} sm={6}>
                                    <div data-aos="zoom-in">
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="45px" /> 
                                        : <TextField 
                                            required 
                                            fullWidth 
                                            id="lastName" 
                                            name="lastName" 
                                            autoComplete="lname" 
                                            variant="outlined"
                                            onKeyDown={props.bind?.keyHandler}
                                            onChange={props.bind?.formHandler} 
                                            value={props.bind?.lastName} 
                                            label={props.bind?.labelLastName} 
                                        />}
                                    </div>
                                </Grid>
                                <Grid item xs={12}>
                                    <div data-aos="zoom-in">
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="45px" /> 
                                        : <TextField 
                                            required 
                                            fullWidth 
                                            id="email" 
                                            name="email" 
                                            autoComplete="email" 
                                            variant="outlined"
                                            onKeyDown={props.bind?.keyHandler}
                                            onChange={props.bind?.formHandler} 
                                            value={props.bind?.email} 
                                            label={props.bind?.labelEmail} 
                                        />}
                                    </div>
                                </Grid>
                                <Grid item xs={12}>
                                <div data-aos="zoom-in">
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="45px" /> 
                                        : <TextField 
                                            required 
                                            fullWidth 
                                            id="subject" 
                                            name="subject" 
                                            autoComplete="subject" 
                                            variant="outlined"
                                            onKeyDown={props.bind?.keyHandler}
                                            onChange={props.bind?.formHandler} 
                                            value={props.bind?.subject} 
                                            label={props.bind?.labelSubject} 
                                        />}
                                    </div>
                                </Grid>
                                <Grid item xs={12}>
                                <div data-aos="zoom-in">
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="45px" /> 
                                        : <TextField 
                                            required 
                                            fullWidth 
                                            multiline 
                                            minRows={6} 
                                            id="message" 
                                            name="message" 
                                            autoComplete="message" 
                                            variant="outlined"
                                            onKeyDown={props.bind?.keyHandler}
                                            onChange={props.bind?.formHandler} 
                                            value={props.bind?.message} 
                                            label={props.bind?.labelMessage} 
                                        />}
                                    </div>
                                </Grid>
                                <Grid item xs={12}>
                                    <div data-aos="zoom-in">
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="30px" /> 
                                        : <FormControlLabel 
                                            control={<VioletCheckbox 
                                                onChange={props.bind?.formHandler} 
                                                checked={props.bind?.terms} 
                                                name="terms" 
                                                id="terms" 
                                            />} 
                                            label={props.bind?.consent} 
                                        />}
                                    </div>
                                </Grid>
                            </Grid>
                            <Box my={2} data-aos="fade-up">
                                {props.bind?.isLoading 
                                ? <Skeleton variant="rect" width="100%" height="40px" /> 
                                : <ActiveButton {...props} />}
                            </Box>
                        </Box>
                    </Box>
                </Container>
            </Container>
        </section>
    );
}
