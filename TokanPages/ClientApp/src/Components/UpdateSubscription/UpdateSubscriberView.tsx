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
import AlertDialog from "../../Shared/Components/AlertDialog/alertDialog";
import updateSubscriberStyle from "./updateSubscriberStyle";
import { IconType } from "../../Shared/enums";

interface IBinding
{
    bind: IProperties;
}

interface IProperties
{
    modalState: boolean;
    modalHandler: any;
    modalTitle: string;
    modalMessage: string;
    modalIcon: IconType;
    isLoading: boolean;
    caption: string;
    formHandler: any;
    email: string;
    buttonHandler: any;
    buttonState: boolean;
    progress: boolean;
    buttonText: string;
}

export default function UpdateSubscriberView(props: IBinding)
{
    const classes = updateSubscriberStyle();
    return (
        <section>
            <Container maxWidth="sm">
                <AlertDialog 
                    State={props.bind?.modalState} 
                    Handle={props.bind?.modalHandler} 
                    Title={props.bind?.modalTitle} 
                    Message={props.bind?.modalMessage} 
                    Icon={props.bind?.modalIcon} 
                />
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
                                    <Grid item xs={12}>
                                        <TextField 
                                            onChange={props.bind?.formHandler} value={props.bind?.email} variant="outlined" required fullWidth 
                                            name="email" id="email" label="Email address" autoComplete="email" 
                                        />
                                    </Grid>
                                </Grid>
                                <Box my={2}>
                                    <Button onClick={props.bind?.buttonHandler} fullWidth variant="contained" color="primary" disabled={!props.bind?.buttonState}>
                                        {props.bind?.progress &&  <CircularProgress size={20} />}
                                        {!props.bind?.progress && props.bind?.buttonText}
                                    </Button>
                                </Box>
                            </Box>
                        </CardContent>
                    </Card>
                </Box>
            </Container>
        </section>
    );
}
