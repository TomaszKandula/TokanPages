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
import { AccountCircle } from "@material-ui/icons";
import Skeleton from "@material-ui/lab/Skeleton";
import VioletCheckbox from "../../Theme/customCheckboxes";
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
    terms?: boolean;
}

const UserSignupView = (props: IBinding): JSX.Element =>
{
    const classes = userSignupStyle();
    return (
        <section>
            <Container maxWidth="sm">
                <Box pt={18} pb={10}>
                    <Card elevation={4}>
                        <CardContent className={classes.card}>
                            <Box mb={3} textAlign="center">
                            <AccountCircle className={classes.account} />
                                <Typography className={classes.caption}>
                                    {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.caption}
                                </Typography>
                            </Box>
                            <Box>
                                <Grid container spacing={2}>
                                    <Grid item xs={12} sm={6}>
                                        <TextField 
                                            required fullWidth onChange={props.bind?.formHandler} value={props.bind?.firstName}
                                            variant="outlined" autoComplete="fname" name="firstName" id="firstName" label="First name"
                                        />
                                    </Grid>
                                    <Grid item xs={12} sm={6}>
                                        <TextField 
                                            required fullWidth onChange={props.bind?.formHandler} value={props.bind?.lastName}
                                            variant="outlined" name="lastName" id="lastName" label="Last name" autoComplete="lname"
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <TextField 
                                            required fullWidth onChange={props.bind?.formHandler} value={props.bind?.email} 
                                            variant="outlined" name="email" id="email" label="Email address" autoComplete="email"
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <TextField 
                                            required fullWidth onChange={props.bind?.formHandler} value={props.bind?.password}
                                            variant="outlined" name="password" id="password" label="Password" type="password" autoComplete="current-password" 
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <FormControlLabel 
                                            control={<VioletCheckbox onChange={props.bind?.formHandler} checked={props.bind?.terms} name="terms" id="terms" />} 
                                            label={props.bind?.label} 
                                        />
                                    </Grid>
                                </Grid>
                                <Box my={2}>
                                    <Button fullWidth onClick={props.bind?.buttonHandler} type="submit" variant="contained" className={classes.button} disabled={props.bind?.progress}>
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
