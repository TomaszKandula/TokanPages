import * as React from "react";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import { Card, CardContent, CircularProgress } from "@material-ui/core";
import TextField from "@material-ui/core/TextField";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import { AccountCircle } from "@material-ui/icons";
import Skeleton from "@material-ui/lab/Skeleton";
import { VioletCheckbox } from "../../../../Theme";
import { ViewProperties } from "../../../../Shared/interfaces";
import { UserSignupStyle } from "./userSignupStyle";

interface IProperties extends ViewProperties
{
    caption: string;
    consent: string;
    button: string;
    link: string;
    buttonHandler: any;
    keyHandler: any;
    formHandler: any;
    progress: boolean;
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    terms?: boolean;
    labelFirstName: string;
    labelLastName: string;
    labelEmail: string;
    labelPassword: string;
}

const ActiveButton = (props: IProperties): JSX.Element => 
{
    const classes = UserSignupStyle();
    return(
        <Button 
            fullWidth 
            type="submit" 
            variant="contained" 
            onClick={props.buttonHandler} 
            className={classes.button} 
            disabled={props.progress}>
            {!props.progress 
            ? props.button 
            : <CircularProgress size={20} />}
        </Button>
    );
}

const RedirectTo = (args: { path: string, name: string }): JSX.Element => 
{
    return(<Link to={args.path}>{args.name}</Link>);
}

export const UserSignupView = (props: IProperties): JSX.Element =>
{
    const classes = UserSignupStyle();
    return (
        <section className={classes.section}>
            <Container maxWidth="sm">
                <Box pt={18} pb={10}>
                    <Card elevation={0} className={classes.card}>
                        <CardContent className={classes.card_content}>
                            <Box mb={3} textAlign="center">
                            <AccountCircle className={classes.account} />
                                <Typography className={classes.caption}>
                                    {props.isLoading 
                                    ? <Skeleton variant="text" /> 
                                    : props.caption}
                                </Typography>
                            </Box>
                            <Box>
                                <Grid container spacing={2}>
                                    <Grid item xs={12} sm={6}>
                                        {props.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="45px" /> 
                                        : <TextField 
                                            required 
                                            fullWidth 
                                            id="firstName" 
                                            name="firstName" 
                                            variant="outlined" 
                                            autoComplete="fname" 
                                            onKeyUp={props.keyHandler}
                                            onChange={props.formHandler} 
                                            value={props.firstName} 
                                            label={props.labelFirstName}
                                        />}
                                    </Grid>
                                    <Grid item xs={12} sm={6}>
                                        {props.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="40px" />
                                        : <TextField 
                                            required 
                                            fullWidth 
                                            id="lastName" 
                                            name="lastName" 
                                            variant="outlined" 
                                            autoComplete="lname"
                                            onKeyUp={props.keyHandler}
                                            onChange={props.formHandler} 
                                            value={props.lastName} 
                                            label={props.labelLastName}
                                        />}
                                    </Grid>
                                    <Grid item xs={12}>
                                        {props.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="45px" />
                                        : <TextField 
                                            required 
                                            fullWidth 
                                            id="email" 
                                            name="email" 
                                            variant="outlined" 
                                            autoComplete="email"
                                            onKeyUp={props.keyHandler}
                                            onChange={props.formHandler} 
                                            value={props.email} 
                                            label={props.labelEmail}
                                        />}
                                    </Grid>
                                    <Grid item xs={12}>
                                        {props.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="45px" />
                                        : <TextField 
                                            required 
                                            fullWidth 
                                            id="password" 
                                            name="password" 
                                            variant="outlined" 
                                            type="password" 
                                            autoComplete="current-password"
                                            onKeyUp={props.keyHandler}
                                            onChange={props.formHandler} 
                                            value={props.password} 
                                            label={props.labelPassword}
                                        />}
                                    </Grid>
                                    <Grid item xs={12}>
                                        {props.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="30px" />
                                        : <FormControlLabel 
                                            control={<VioletCheckbox 
                                                id="terms" 
                                                name="terms" 
                                                onChange={props.formHandler} 
                                                checked={props.terms} 
                                            />} 
                                            label={props.consent}
                                        />}
                                    </Grid>
                                </Grid>
                                <Box my={2}>
                                    {props.isLoading 
                                    ? <Skeleton variant="rect" width="100%" height="40px" /> 
                                    : <ActiveButton {...props} />}
                                </Box>
                                <Box textAlign="right">
                                    {props.isLoading 
                                    ? <Skeleton variant="text" /> 
                                    : <RedirectTo path="/signin" name={props.link} />}
                                </Box>
                            </Box>
                        </CardContent>
                    </Card>
                </Box>
            </Container>
        </section>  
    );
}
