import * as React from "react";
import { AccountCircle } from "@material-ui/icons";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import TextField from "@material-ui/core/TextField";
import Button from "@material-ui/core/Button";
import CircularProgress from "@material-ui/core/CircularProgress";
import Skeleton from "@material-ui/lab/Skeleton";
import { UpdateSubscriberStyle } from "./updateSubscriberStyle";

interface IBinding
{
    bind: IProperties;
}

interface IProperties
{
    isLoading: boolean;
    caption: string;
    formHandler: any;
    email: string;
    buttonHandler: any;
    buttonState: boolean;
    progress: boolean;
    buttonText: string;
    labelEmail: string;
}

export const UpdateSubscriberView = (props: IBinding): JSX.Element =>
{
    const classes = UpdateSubscriberStyle();

    const ActiveButton = (): JSX.Element => 
    {
        return(
            <Button fullWidth onClick={props.bind?.buttonHandler} variant="contained" 
                className={classes.button} disabled={props.bind?.progress || !props.bind?.buttonState}>
                {props.bind?.progress &&  <CircularProgress size={20} />}
                {!props.bind?.progress && props.bind?.buttonText}
            </Button>
        );
    }

    return (
        <section className={classes.section}>
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
                                    <Grid item xs={12}>
                                        <TextField 
                                            required fullWidth onChange={props.bind?.formHandler} value={props.bind?.email} label={props.bind?.labelEmail}
                                            variant="outlined" name="email" id="email" autoComplete="email" 
                                        />
                                    </Grid>
                                </Grid>
                                <Box my={2}>
                                    {props.bind?.isLoading ? <Skeleton variant="rect" /> : <ActiveButton />}
                                </Box>
                            </Box>
                        </CardContent>
                    </Card>
                </Box>
            </Container>
        </section>
    );
}
