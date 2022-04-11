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
import userSigninStyle from "./Styles/userSigninStyle";

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

const UserSigninView = (props: IBinding): JSX.Element =>
{
    const classes = userSigninStyle();

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

    return (
        <section>
            <Container maxWidth="sm">
                <Box pt={18} pb={10}>             
                    <Card elevation={4}>
                        <CardContent className={classes.card}>
                            <Box mb={3} textAlign="center">
                                <AccountCircle className={classes.account} />
                                <Typography className={classes.caption} >
                                    {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.caption}
                                </Typography>
                            </Box>
                            <Box>
                                <Grid container spacing={2}>
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
                                </Grid>
                                <Box my={2}>
                                    {props.bind?.isLoading ? <Skeleton variant="rect" /> : <ActiveButton />}
                                </Box>
                                <Grid container spacing={2} className={classes.actions}>
                                    <Grid item xs={12} sm={6}>
                                        <Link to="/signup">
                                            {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.link1}
                                        </Link>
                                    </Grid>
                                    <Grid item xs={12} sm={6} className={classes.tertiaryAction}>
                                        <Link to="/resetpassword">
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

export default UserSigninView;
