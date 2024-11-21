import * as React from "react";
import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";
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
import { UserSigninProps } from "../userSignin";

interface UserSigninViewProps extends ViewProperties, UserSigninProps {
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

const ActiveButton = (props: UserSigninViewProps): React.ReactElement => {
    return (
        <Button
            fullWidth
            type="submit"
            variant="contained"
            onClick={props.buttonHandler}
            className="button"
            disabled={props.progress}
        >
            {!props.progress ? props.button : <CircularProgress size={20} />}
        </Button>
    );
};

const RedirectTo = (args: { path: string; name: string }): React.ReactElement => {
    return <Link to={args.path}>{args.name}</Link>;
};

export const UserSigninView = (props: UserSigninViewProps): React.ReactElement => {
    return (
        <section className="section" style={props.background}>
            <Container className="container">
                <div style={{ paddingTop: props.pt ?? 32, paddingBottom: props.pb ?? 80 }}>
                    <Card elevation={0} className="card">
                        <CardContent className="card-content">
                            <div style={{ textAlign: "center", marginBottom: 24 }}>
                                <AccountCircle className="account" />
                                <Typography className="caption">
                                    {props.isLoading ? <Skeleton variant="text" /> : props.caption}
                                </Typography>
                            </div>
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
                                            disabled={props.progress}
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
                                            isDisabled={props.progress}
                                        />
                                    )}
                                </Grid>
                            </Grid>
                            <div style={{ marginTop: 16, marginBottom: 16 }}>
                                {props.isLoading ? (
                                    <Skeleton variant="rect" width="100%" height="40px" />
                                ) : (
                                    <ActiveButton {...props} />
                                )}
                            </div>
                            <Grid container spacing={2} className="actions">
                                <Grid item xs={12} sm={6}>
                                    {props.isLoading ? (
                                        <Skeleton variant="text" />
                                    ) : (
                                        <RedirectTo path="/signup" name={props.link1} />
                                    )}
                                </Grid>
                                <Grid item xs={12} sm={6} className="secondaryAction">
                                    {props.isLoading ? (
                                        <Skeleton variant="text" />
                                    ) : (
                                        <RedirectTo path="/resetpassword" name={props.link2} />
                                    )}
                                </Grid>
                            </Grid>
                        </CardContent>
                    </Card>
                </div>
            </Container>
        </section>
    );
};
