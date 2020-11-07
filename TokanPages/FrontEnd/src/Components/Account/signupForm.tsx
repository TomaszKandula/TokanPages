import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import { Link } from "react-router-dom";
import Button from "@material-ui/core/Button";
import { Card, CardContent } from "@material-ui/core";
import TextField from "@material-ui/core/TextField";
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Checkbox from '@material-ui/core/Checkbox';
import { AccountCircle } from "@material-ui/icons";

const useStyles = makeStyles((theme) => (
{
    tertiaryAction: 
    {
        [theme.breakpoints.up("sm")]: 
        {
            textAlign: "right"
        }
    },
    actions: 
    {
        [theme.breakpoints.down("sm")]: 
        {
            marginTop: theme.spacing(3)
        },
    },
    card:
    {
        marginTop: 10,
        marginLeft: 15,
        marginRight: 15,
        marginBottom: 10
    }
}));
   
export default function SignupForm() 
{

    const classes = useStyles();

    return (
        <section>
            <Container maxWidth="sm">
                <Box pt={18} pb={10}>
                    <Card elevation={4}>
                        <CardContent className={classes.card}>
                            <Box mb={3} textAlign="center">
                            <AccountCircle color="primary" style={{ fontSize: 72 }} />
                                <Typography variant="h5" component="h2" color="textSecondary">
                                    Create a new account
                                </Typography>
                            </Box>
                            <Box>
                                <Grid container spacing={2}>
                                    <Grid item xs={12} sm={6}>
                                        <TextField variant="outlined" required fullWidth autoComplete="fname" name="firstName" id="firstName" label="First name" />
                                    </Grid>
                                    <Grid item xs={12} sm={6}>
                                        <TextField variant="outlined" required fullWidth name="lastName" id="lastName" label="Last name" autoComplete="lname" />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <TextField variant="outlined" required fullWidth name="email" id="email" label="Email address" autoComplete="email" />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <TextField variant="outlined" required fullWidth name="password" id="password" label="Password" type="password" autoComplete="current-password" />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <FormControlLabel control={<Checkbox name="terms" value="1" color="primary" />} label="I agree to the terms of use and privacy policy." />
                                    </Grid>
                                </Grid>
                                <Box my={2}>
                                    <Button fullWidth variant="contained" color="primary">
                                        Sign up
                                    </Button>
                                </Box>
                                <Box textAlign="right">
                                    <Link to="/signin">
                                        Already have an account? Sign in
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
