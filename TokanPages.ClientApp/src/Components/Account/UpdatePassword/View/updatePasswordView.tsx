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
import { UpdatePasswordStyle } from "./updatePasswordStyle";

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
    newPassword: string;
    verifyPassword: string;
    formHandler: any;
    buttonHandler: any;
    disableForm: boolean;
    labelNewPassword: string;
    labelVerifyPassword: string;
}

export const UpdatePasswordView = (props: IBinding): JSX.Element =>
{
    const classes = UpdatePasswordStyle();

    const ActiveButton = (): JSX.Element => 
    {
        return(
            <Button fullWidth onClick={props.bind?.buttonHandler} type="submit" variant="contained" 
                className={classes.button} disabled={props.bind?.progress || props.bind?.disableForm}>
                {props.bind?.progress &&  <CircularProgress size={20} />}
                {!props.bind?.progress && props.bind?.button}
            </Button>
        );
    }

    return (
        <section className={classes.section}>
            <Container maxWidth="sm">
                <Box pt={18} pb={10}>             
                    <Card elevation={0} className={classes.card}>
                        <CardContent className={classes.card_content}>
                            <Box mb={3} textAlign="center">
                                <AccountCircle className={classes.account} />
                                <Typography className={classes.caption}>
                                    {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.caption}
                                </Typography>
                            </Box>
                            <Box>
                                <Grid container spacing={2}>
                                    <Grid item xs={12}>
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="45px" /> 
                                        : <TextField required fullWidth onChange={props.bind?.formHandler} value={props.bind?.newPassword} label={props.bind?.labelNewPassword}
                                            variant="outlined" name="newPassword" id="newPassword" type="password" autoComplete="password" disabled={props.bind?.disableForm} />}
                                    </Grid>
                                    <Grid item xs={12}>
                                       {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="45px" /> 
                                        : <TextField required fullWidth onChange={props.bind?.formHandler} value={props.bind?.verifyPassword} label={props.bind?.labelVerifyPassword}
                                            variant="outlined" name="verifyPassword" id="verifyPassword" type="password" autoComplete="password" disabled={props.bind?.disableForm} />}
                                    </Grid>
                                </Grid>
                                <Box my={2}>
                                    {props.bind?.isLoading ? <Skeleton variant="rect" width="100%" height="40px" /> : <ActiveButton />}
                                </Box>
                            </Box>
                        </CardContent>   
                    </Card>                    
                </Box>
            </Container>
        </section>
    );
}
