import * as React from "react";
import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import { Card, CardContent, CircularProgress } from "@material-ui/core";
import { AccountCircle } from "@material-ui/icons";
import Skeleton from "@material-ui/lab/Skeleton";
import { ViewProperties } from "../../../../Shared/interfaces";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../Shared/types";
import { TextFiedWithPassword } from "../../../../Shared/Components";
import { UpdatePasswordStyle } from "./updatePasswordStyle";

interface Properties extends ViewProperties
{
    progress: boolean;
    caption: string;
    button: string;
    newPassword: string;
    verifyPassword: string;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    buttonHandler: () => void;
    disableForm: boolean;
    labelNewPassword: string;
    labelVerifyPassword: string;
}

const ActiveButton = (props: Properties): JSX.Element => 
{
    const classes = UpdatePasswordStyle();
    return(
        <Button 
            fullWidth 
            type="submit" 
            variant="contained" 
            onClick={props.buttonHandler} 
            className={classes.button} 
            disabled={props.progress || props.disableForm}>
            {!props.progress 
            ? props.button 
            : <CircularProgress size={20} />}
        </Button>
    );
}

export const UpdatePasswordView = (props: Properties): JSX.Element =>
{
    const classes = UpdatePasswordStyle();
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
                                    <Grid item xs={12}>
                                        {props.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="45px" /> 
                                        : <TextFiedWithPassword 
                                            uuid="newPassword" 
                                            fullWidth={true}
                                            value={props.newPassword} 
                                            label={props.labelNewPassword}
                                            onKeyUp={props.keyHandler} 
                                            onChange={props.formHandler}
                                            isDisabled={props.disableForm} 
                                        />}
                                    </Grid>
                                    <Grid item xs={12}>
                                       {props.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="45px" /> 
                                        : <TextFiedWithPassword 
                                            uuid="verifyPassword" 
                                            fullWidth={true}
                                            value={props.verifyPassword} 
                                            label={props.labelVerifyPassword}
                                            onKeyUp={props.keyHandler} 
                                            onChange={props.formHandler} 
                                            isDisabled={props.disableForm}
                                        />}
                                    </Grid>
                                </Grid>
                                <Box my={2}>
                                    {props.isLoading 
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
