import * as React from "react";
import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import TextField from "@material-ui/core/TextField";
import { CircularProgress } from "@material-ui/core";
import Skeleton from "@material-ui/lab/Skeleton";
import newsletterStyle from "./newsletterStyle";

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
    email: string;
    buttonHandler: any;
    progress: boolean;
    buttonText: string;
    labelEmail: string;
}

const NewsletterView = (props: IBinding): JSX.Element =>
{
    const classes = newsletterStyle();

    const ActiveButton = (): JSX.Element => 
    {
        return(
            <Button fullWidth onClick={props.bind?.buttonHandler} type="submit" variant="contained" 
                className={classes.button} disabled={props.bind?.progress}>
                {props.bind?.progress &&  <CircularProgress size={20} />}
                {!props.bind?.progress && props.bind?.buttonText}
            </Button>
        );
    }

    return (
        <section className={classes.section}>
            <Container maxWidth="lg">
                <Box py={8} textAlign="center">
                    <Grid container spacing={2}>
                        <Grid item xs={12} md={5}>
                            <Typography className={classes.caption} data-aos="fade-down">
                                {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.caption}
                            </Typography>
                            <div data-aos="zoom-in">
                                <Typography className={classes.text}>
                                    {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.text}
                                </Typography>
                            </div>
                        </Grid>
                        <Grid item xs={12} md={7}>
                            <div data-aos="fade-left">
                                <Box display="flex" height="100%">
                                    <Box my="auto" width="100%">
                                        <Grid container spacing={2}>
                                            <Grid item xs={12} sm={7}>
                                                <TextField 
                                                    required fullWidth onChange={props.bind?.formHandler} value={props.bind?.email} label={props.bind?.labelEmail}
                                                    variant="outlined" size="small" name="email" id="email_newletter" autoComplete="email" 
                                                />
                                            </Grid>
                                            <Grid item xs={12} sm={5}>
                                                {props.bind?.isLoading ? <Skeleton variant="rect" width="100%" height="40px" /> : <ActiveButton />}
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

export default NewsletterView;
