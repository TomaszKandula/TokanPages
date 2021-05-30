import * as React from "react";
import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import TextField from "@material-ui/core/TextField";
import { CircularProgress } from "@material-ui/core";
import Skeleton from "@material-ui/lab/Skeleton";
import AlertDialog from "../../Shared/Components/AlertDialog/alertDialog";
import { IconType } from "../../Shared/enums";
import newsletterStyle from "./newsletterStyle";

interface IBinding
{
    bind: IProperties;
}

interface IProperties
{
    modalState: boolean;
    modalHandler: any;
    modalTitle: string;
    modalMessage: string;
    modalIcon: IconType;
    isLoading: boolean;
    caption: string;
    text: string;
    formHandler: any;
    email: string;
    buttonHandler: any;
    progress: boolean;
    buttonText: string;
}

export default function NewsletterView(props: IBinding)
{
    const classes = newsletterStyle();
    return (
        <section className={classes.section}>
            <Container maxWidth="lg">
                <AlertDialog 
                    State={props.bind?.modalState} 
                    Handle={props.bind?.modalHandler} 
                    Title={props.bind?.modalTitle} 
                    Message={props.bind?.modalMessage} 
                    Icon={props.bind?.modalIcon} 
                />
                <div data-aos="fade-up">
                    <Box py={8} textAlign="center">
                        <Grid container spacing={2}>
                            <Grid item xs={12} md={5}>
                                <Typography variant="h4" component="h2">
                                    {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.caption}
                                </Typography>
                                <Typography variant="subtitle1" color="textSecondary">
                                    {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.text}
                                </Typography>
                            </Grid>
                            <Grid item xs={12} md={7}>
                                <Box display="flex" height="100%">
                                    <Box my="auto" width="100%">
                                        <Grid container spacing={2}>
                                            <Grid item xs={12} sm={7}>
                                                <TextField 
                                                    onChange={props.bind?.formHandler} value={props.bind?.email} variant="outlined" 
                                                    required fullWidth size="small" name="email" id="email_newletter" 
                                                    label="Email address" autoComplete="email" 
                                                />
                                            </Grid>
                                            <Grid item xs={12} sm={5}>
                                                <Button onClick={props.bind?.buttonHandler} type="submit" fullWidth variant="contained" color="primary" disabled={props.bind?.progress}>
                                                    {props.bind?.progress &&  <CircularProgress size={20} />}
                                                    {!props.bind?.progress && props.bind?.buttonText}
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
