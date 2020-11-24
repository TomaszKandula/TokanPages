import * as React from "react";
import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import { Link } from "react-router-dom";
import Button from "@material-ui/core/Button";
import TextField from "@material-ui/core/TextField";
import { Card, CardContent } from "@material-ui/core";
import { AccountCircle } from "@material-ui/icons";
import useStyles from "./Hooks/styleSigninForm";

export default function SigninForm() 
{

    const classes = useStyles();
    const content = 
    {
        caption: "Sign in",
        button: "Sign in",
        link1: "Don't have an account?",
        link2: "Forgot password?"
    }

    return (
        <section>
            <Container maxWidth="sm">
                <Box pt={18} pb={10}>             
                    <Card elevation={4}>
                        <CardContent className={classes.card}>
                            <Box mb={3} textAlign="center">
                                <AccountCircle color="primary" style={{ fontSize: 72 }} />
                                <Typography variant="h5" component="h2" color="textSecondary">
                                    {content.caption}
                                </Typography>
                            </Box>
                            <Box>
                                <Grid container spacing={2}>
                                    <Grid item xs={12}>
                                        <TextField variant="outlined" required fullWidth name="email" id="email" label="Email address" autoComplete="email" />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <TextField variant="outlined" required fullWidth name="password" id="password" label="Password" type="password" autoComplete="current-password" />
                                    </Grid>
                                </Grid>
                                <Box my={2}>
                                    <Button fullWidth variant="contained" color="primary">
                                        {content.button}
                                    </Button>
                                </Box>
                                <Grid container spacing={2} className={classes.actions}>
                                    <Grid item xs={12} sm={6}>
                                        <Link to="/signup">
                                            {content.link1}
                                        </Link>
                                    </Grid>
                                    <Grid item xs={12} sm={6} className={classes.tertiaryAction}>
                                        <Link to="/reset">
                                            {content.link2}
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
