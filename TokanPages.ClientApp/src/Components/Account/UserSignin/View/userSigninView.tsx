import * as React from "react";
import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import { Link } from "react-router-dom";
import Button from "@material-ui/core/Button";
import TextField from "@material-ui/core/TextField";
import { Card, CardContent, CircularProgress } from "@material-ui/core";
import { AccountCircle } from "@material-ui/icons";
import Skeleton from "@material-ui/lab/Skeleton";
import { UserSigninStyle } from "./userSigninStyle";

interface IBinding 
{
    bind: IProperties;
}

interface IProperties
{
    isLoading: boolean;
    caption: string;
    button: string;
    link1: string;
    link2: string;
    buttonHandler: any;
    progress: boolean;
    formHandler: any;
    email: string;
    password: string;
    labelEmail: string;
    labelPassword: string;
}

export const UserSigninView = (props: IBinding): JSX.Element =>
{
    const classes = UserSigninStyle();

    const ActiveButton = (): JSX.Element => 
    {
        return(
            <Button fullWidth onClick={props.bind?.buttonHandler} type="submit" variant="contained" 
                className={classes.button} disabled={props.bind?.progress}>
                {props.bind?.progress &&  <CircularProgress size={20} />}
                {!props.bind?.progress && props.bind?.button}
            </Button>
        );
    }

    const RedirectTo = (args: { path: string, name: string }): JSX.Element => 
    {
        return(<Link to={args.path}>{args.name}</Link>);
    }

    return (
        <section>
            <Container maxWidth="sm">
                <Box pt={18} pb={10}>             
                    <Card elevation={0} className={classes.card}>
                        <CardContent className={classes.card_content}>
                            <Box mb={3} textAlign="center">
                                <AccountCircle className={classes.account} />
                                <Typography className={classes.caption} >
                                    {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.caption}
                                </Typography>
                            </Box>
                            <Box>
                                <Grid container spacing={2}>
                                    <Grid item xs={12}>
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="45px" /> 
                                        : <TextField required fullWidth onChange={props.bind?.formHandler} value={props.bind?.email} label={props.bind?.labelEmail}
                                            variant="outlined" name="email" id="email" autoComplete="email" />}
                                    </Grid>
                                    <Grid item xs={12}>
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="45px" /> 
                                        : <TextField required fullWidth onChange={props.bind?.formHandler} value={props.bind?.password} label={props.bind?.labelPassword}
                                            variant="outlined" name="password" id="password" type="password" autoComplete="current-password" />}
                                    </Grid>
                                </Grid>
                                <Box my={2}>
                                    {props.bind?.isLoading ? <Skeleton variant="rect" width="100%" height="40px" /> : <ActiveButton />}
                                </Box>
                                <Grid container spacing={2} className={classes.actions}>
                                    <Grid item xs={12} sm={6}>
                                        {props.bind?.isLoading ? <Skeleton variant="text" /> : <RedirectTo path="/signup" name={props.bind?.link1} />}
                                    </Grid>
                                    <Grid item xs={12} sm={6} className={classes.tertiaryAction}>
                                        {props.bind?.isLoading ? <Skeleton variant="text" /> : <RedirectTo path="/resetpassword" name={props.bind?.link2} />}
                                    </Grid>
                                </Grid>
                            </Box>
                        </CardContent>   
                    </Card>                    
                </Box>
            </Container>
        </section>
    );
}
