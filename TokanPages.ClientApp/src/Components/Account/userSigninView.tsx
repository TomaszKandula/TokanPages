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
import signinFormStyle from "./Styles/userSigninStyle";

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
}

export default function UserSigninView(props: IBinding) 
{
    const classes = signinFormStyle();
    return (
        <section>
            <Container maxWidth="sm">
                <Box pt={18} pb={10}>             
                    <Card elevation={4}>
                        <CardContent className={classes.card}>
                            <Box mb={3} textAlign="center">
                                <AccountCircle color="primary" style={{ fontSize: 72 }} />
                                <Typography variant="h5" component="h2" color="textSecondary">
                                    {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.caption}
                                </Typography>
                            </Box>
                            <Box>
                                <Grid container spacing={2}>
                                    <Grid item xs={12}>
                                        <TextField 
                                            onChange={props.bind?.formHandler} value={props.bind?.email} variant="outlined" required fullWidth 
                                            name="email" id="email" label="Email address" autoComplete="email" 
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <TextField 
                                            onChange={props.bind?.formHandler} value={props.bind?.password} variant="outlined" required fullWidth 
                                            name="password" id="password" label="Password" type="password" autoComplete="current-password" 
                                        />
                                    </Grid>
                                </Grid>
                                <Box my={2}>
                                    <Button onClick={props.bind?.buttonHandler} type="submit" fullWidth variant="contained" color="primary" disabled={props.bind?.progress}>
                                        {props.bind?.progress &&  <CircularProgress size={20} />}
                                        {!props.bind?.progress && props.bind?.button}
                                    </Button>
                                </Box>
                                <Grid container spacing={2} className={classes.actions}>
                                    <Grid item xs={12} sm={6}>
                                        <Link to="/signup">
                                            {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.link1}
                                        </Link>
                                    </Grid>
                                    <Grid item xs={12} sm={6} className={classes.tertiaryAction}>
                                        <Link to="/reset">
                                            {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.link2}
                                        </Link>
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
