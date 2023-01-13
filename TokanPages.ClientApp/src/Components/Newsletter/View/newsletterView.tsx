import * as React from "react";
import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import TextField from "@material-ui/core/TextField";
import { CircularProgress } from "@material-ui/core";
import Skeleton from "@material-ui/lab/Skeleton";
import { NewsletterStyle } from "./newsletterStyle";

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
    email: string;
    buttonHandler: any;
    progress: boolean;
    buttonText: string;
    labelEmail: string;
}

const ActiveButton = (props: IBinding): JSX.Element => 
{
    const classes = NewsletterStyle();
    return(
        <Button 
            fullWidth 
            type="submit" 
            variant="contained" 
            onClick={props.bind?.buttonHandler} 
            className={classes.button} 
            disabled={props.bind?.progress}>
            {!props.bind?.progress 
            ? props.bind?.buttonText 
            : <CircularProgress size={20} />}
        </Button>
    );
}

export const NewsletterView = (props: IBinding): JSX.Element =>
{
    const classes = NewsletterStyle();
    return (
        <section className={classes.section}>
            <Container maxWidth="lg">
                <Box py={8} textAlign="center">
                    <Grid container spacing={2}>
                        <Grid item xs={12} md={5}>
                            <Typography className={classes.caption} data-aos="fade-down">
                                {props.bind?.isLoading 
                                ? <Skeleton variant="text" /> 
                                : props.bind?.caption}
                            </Typography>
                            <div data-aos="zoom-in">
                                <Typography className={classes.text}>
                                    {props.bind?.isLoading 
                                    ? <Skeleton variant="text" /> 
                                    : props.bind?.text}
                                </Typography>
                            </div>
                        </Grid>
                        <Grid item xs={12} md={7}>
                            <div data-aos="zoom-in">
                                <Box display="flex" height="100%">
                                    <Box my="auto" width="100%">
                                        <Grid container spacing={2}>
                                            <Grid item xs={12} sm={7}>
                                                {props.bind?.isLoading 
                                                ? <Skeleton variant="rect" width="100%" height="45px" /> 
                                                : <TextField 
                                                    required 
                                                    fullWidth 
                                                    id="email_newletter" 
                                                    name="email" 
                                                    variant="outlined" 
                                                    size="small" 
                                                    autoComplete="email"
                                                    onKeyDown={props.bind?.keyHandler}
                                                    onChange={props.bind?.formHandler} 
                                                    value={props.bind?.email} 
                                                    label={props.bind?.labelEmail}
                                                />}
                                            </Grid>
                                            <Grid item xs={12} sm={5}>
                                                {props.bind?.isLoading 
                                                ? <Skeleton variant="rect" width="100%" height="40px" /> 
                                                : <ActiveButton {...props} />}
                                            </Grid>
                                        </Grid>
                                    </Box>
                                </Box>
                            </div>
                        </Grid>
                    </Grid>
                </Box>
            </Container>
        </section>
    );
}
