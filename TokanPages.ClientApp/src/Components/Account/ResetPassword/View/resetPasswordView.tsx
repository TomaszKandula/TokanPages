import * as React from "react";
import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import TextField from "@material-ui/core/TextField";
import { Card, CardContent, CircularProgress } from "@material-ui/core";
import { AccountCircle } from "@material-ui/icons";
import Skeleton from "@material-ui/lab/Skeleton";
import { ResetPasswordStyle } from "./resetPasswordStyle";

interface IBinding 
{
    bind: IProperties;
}

interface IProperties
{
    isLoading: boolean;
    progress: boolean;
    caption: string;
    button: string;
    email: string;
    keyHandler: any;
    formHandler: any;
    buttonHandler: any;
    labelEmail: string;
}

const ActiveButton = (props: IBinding): JSX.Element => 
{
    const classes = ResetPasswordStyle();
    return(
        <Button 
            fullWidth 
            type="submit" 
            variant="contained" 
            onClick={props.bind?.buttonHandler} 
            className={classes.button} 
            disabled={props.bind?.progress}>
            {!props.bind?.progress 
            ? props.bind?.button 
            : <CircularProgress size={20} />}
        </Button>
    );
}

export const ResetPasswordView = (props: IBinding): JSX.Element =>
{
    const classes = ResetPasswordStyle();
    return (
        <section className={classes.section}>
            <Container maxWidth="sm">
                <Box pt={18} pb={10}>             
                    <Card elevation={0} className={classes.card}>
                        <CardContent className={classes.card_content}>
                            <Box mb={3} textAlign="center">
                                <AccountCircle className={classes.account} />
                                <Typography className={classes.caption}>
                                    {props.bind?.isLoading 
                                    ? <Skeleton variant="text" /> 
                                    : props.bind?.caption}
                                </Typography>
                            </Box>
                            <Box>
                                <Grid container spacing={2}>
                                    <Grid item xs={12}>
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="45px" /> 
                                        : <TextField 
                                            required 
                                            fullWidth
                                            id="email" 
                                            name="email" 
                                            variant="outlined" 
                                            autoComplete="email" 
                                            onKeyUp={props.bind?.keyHandler} 
                                            onChange={props.bind?.formHandler} 
                                            value={props.bind?.email} 
                                            label={props.bind?.labelEmail}
                                        />}
                                    </Grid>
                                </Grid>
                                <Box my={2}>
                                    {props.bind?.isLoading 
                                    ? <Skeleton variant="rect" width="100%" height="40px" /> 
                                    : <ActiveButton {...props} />}
                                </Box>
                            </Box>
                        </CardContent>   
                    </Card>                    
                </Box>
            </Container>
        </section>
    );
}
