import * as React from "react";
import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import { Card, CardContent, CircularProgress } from "@material-ui/core";
import { AccountCircle } from "@material-ui/icons";
import Skeleton from "@material-ui/lab/Skeleton";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../Shared/types";
import { TextFiedWithPassword } from "../../../../Shared/Components";
import { UpdatePasswordProps } from "../updatePassword";

interface Properties extends ViewProperties, UpdatePasswordProps {
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

const ActiveButton = (props: Properties): React.ReactElement => {
    return (
        <Button
            fullWidth
            type="submit"
            variant="contained"
            onClick={props.buttonHandler}
            className="button"
            disabled={props.progress || props.disableForm}
        >
            {!props.progress ? props.button : <CircularProgress size={20} />}
        </Button>
    );
};

export const UpdatePasswordView = (props: Properties): React.ReactElement => {
    return (
        <section className="section" style={props.background}>
            <Container className="container">
                <div style={{ paddingTop: props.pt ?? 96, paddingBottom: props.pb ?? 80 }}>
                    <Card elevation={0} className="card">
                        <CardContent className="card-content">
                            <div style={{ textAlign: "center", marginBottom: 24 }}>
                                <AccountCircle className="account" />
                                <Typography className="caption">
                                    {props.isLoading ? <Skeleton variant="text" /> : props.caption}
                                </Typography>
                            </div>
                            <div>
                                <Grid container spacing={2}>
                                    <Grid item xs={12}>
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="45px" />
                                        ) : (
                                            <TextFiedWithPassword
                                                uuid="newPassword"
                                                fullWidth={true}
                                                value={props.newPassword}
                                                label={props.labelNewPassword}
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                                isDisabled={props.disableForm || props.progress}
                                            />
                                        )}
                                    </Grid>
                                    <Grid item xs={12}>
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="45px" />
                                        ) : (
                                            <TextFiedWithPassword
                                                uuid="verifyPassword"
                                                fullWidth={true}
                                                value={props.verifyPassword}
                                                label={props.labelVerifyPassword}
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                                isDisabled={props.disableForm || props.progress}
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
                            </div>
                        </CardContent>
                    </Card>
                </div>
            </Container>
        </section>
    );
};
