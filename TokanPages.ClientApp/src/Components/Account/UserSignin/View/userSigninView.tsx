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
import { ViewProperties } from "../../../../Shared/Abstractions";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../Shared/types";
import { TextFiedWithPassword } from "../../../../Shared/Components";
import { UserSigninStyle } from "./userSigninStyle";

interface Properties extends ViewProperties {
    caption: string;
    button: string;
    link1: string;
    link2: string;
    buttonHandler: () => void;
    progress: boolean;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    email: string;
    password: string;
    labelEmail: string;
    labelPassword: string;
}

const ActiveButton = (props: Properties): JSX.Element => {
    const classes = UserSigninStyle();
    return (
        <Button
            fullWidth
            type="submit"
            variant="contained"
            onClick={props.buttonHandler}
            className={classes.button}
            disabled={props.progress}
        >
            {!props.progress ? props.button : <CircularProgress size={20} />}
        </Button>
    );
};

const RedirectTo = (args: { path: string; name: string }): JSX.Element => {
    return <Link to={args.path}>{args.name}</Link>;
};

export const UserSigninView = (props: Properties): JSX.Element => {
    const classes = UserSigninStyle();
    return (
        <section className={classes.section}>
            <Container maxWidth="sm">
                <Box pt={18} pb={10}>
                    <Card elevation={0} className={classes.card}>
                        <CardContent className={classes.card_content}>
                            <Box mb={3} textAlign="center">
                                <AccountCircle className={classes.account} />
                                <Typography className={classes.caption}>
                                    {props.isLoading ? <Skeleton variant="text" /> : props.caption}
                                </Typography>
                            </Box>
                            <Box>
                                <Grid container spacing={2}>
                                    <Grid item xs={12}>
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="45px" />
                                        ) : (
                                            <TextField
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
                                            />
                                        )}
                                    </Grid>
                                    <Grid item xs={12}>
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="45px" />
                                        ) : (
                                            <TextFiedWithPassword
                                                uuid="password"
                                                fullWidth={true}
                                                value={props.password}
                                                label={props.labelPassword}
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                            />
                                        )}
                                    </Grid>
                                </Grid>
                                <Box my={2}>
                                    {props.isLoading ? (
                                        <Skeleton variant="rect" width="100%" height="40px" />
                                    ) : (
                                        <ActiveButton {...props} />
                                    )}
                                </Box>
                                <Grid container spacing={2} className={classes.actions}>
                                    <Grid item xs={12} sm={6}>
                                        {props.isLoading ? (
                                            <Skeleton variant="text" />
                                        ) : (
                                            <RedirectTo path="/signup" name={props.link1} />
                                        )}
                                    </Grid>
                                    <Grid item xs={12} sm={6} className={classes.tertiaryAction}>
                                        {props.isLoading ? (
                                            <Skeleton variant="text" />
                                        ) : (
                                            <RedirectTo path="/resetpassword" name={props.link2} />
                                        )}
                                    </Grid>
                                </Grid>
                            </Box>
                        </CardContent>
                    </Card>
                </Box>
            </Container>
        </section>
    );
};
