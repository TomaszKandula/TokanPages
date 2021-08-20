import * as React from "react";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import { Card, CardContent, CircularProgress } from "@material-ui/core";
import TextField from "@material-ui/core/TextField";
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Checkbox from '@material-ui/core/Checkbox';
import { AccountCircle } from "@material-ui/icons";
import Skeleton from "@material-ui/lab/Skeleton";
import userSignupStyle from "./Styles/userSignupStyle";

interface IBinding 
{
    bind: IProperties;
}

interface IProperties
{
    isLoading: boolean;
    caption: string;
    label: string;
    button: string;
    link: string;
    buttonHandler: any;
    formHandler: any;
    progress: boolean;
    firstName: string;
    lastName: string;
    email: string;
    password: string;
}

const UserSignupView = (props: IBinding) =>
{
    const classes = userSignupStyle();
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
                                    <Grid item xs={12} sm={6}>
                                        <TextField 
                                            onChange={props.bind?.formHandler} value={props.bind?.firstName} variant="outlined" required fullWidth 
                                            autoComplete="fname" name="firstName" id="firstName" label="First name" 
                                        />
                                    </Grid>
                                    <Grid item xs={12} sm={6}>
                                        <TextField 
                                            onChange={props.bind?.formHandler} value={props.bind?.lastName} variant="outlined" required fullWidth 
                                            name="lastName" id="lastName" label="Last name" autoComplete="lname" 
                                        />
                                    </Grid>
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
                                    <Grid item xs={12}>
                                        <FormControlLabel 
                                            control={<Checkbox name="terms" value="1" color="primary" />} 
                                            label={props.bind?.label} 
                                        />
                                    </Grid>
                                </Grid>
                                <Box my={2}>
                                    <Button onClick={props.bind?.buttonHandler} type="submit" fullWidth variant="contained" color="primary" disabled={props.bind?.progress}>
                                        {props.bind?.progress &&  <CircularProgress size={20} />}
                                        {!props.bind?.progress && props.bind?.button}
                                    </Button>
                                </Box>
                                <Box textAlign="right">
                                    <Link to="/signin">
                                        {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.link}
                                    </Link>
                                </Box>
                            </Box>
                        </CardContent>
                    </Card>
                </Box>
            </Container>
        </section>  
    );
}

export default UserSignupView;
