import * as React from "react";
import { Button, CircularProgress, Divider, Grid, Typography } from "@material-ui/core";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import Container from "@material-ui/core/Container";
import Skeleton from "@material-ui/lab/Skeleton";
import { SectionAccountPassword } from "../../../../../Api/Models";
import { ViewProperties } from "../../../../../Shared/Abstractions";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../../Shared/types";
import { TextFiedWithPassword } from "../../../../../Shared/Components";
import { UserPasswordProps } from "../userPassword";

interface UserPasswordViewProps extends ViewProperties, UserPasswordProps {
    oldPassword: string;
    newPassword: string;
    confirmPassword: string;
    formProgress: boolean;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    buttonHandler: () => void;
    sectionAccountPassword: SectionAccountPassword;
}

interface CustomDividerProps {
    marginTop: number; 
    marginBottom: number;
}

const UpdatePasswordButton = (props: UserPasswordViewProps): React.ReactElement => {
    return (
        <Button
            fullWidth
            type="submit"
            variant="contained"
            onClick={props.buttonHandler}
            disabled={props.formProgress}
            className="button-update"
        >
            {!props.formProgress ? props.sectionAccountPassword?.updateButtonText : <CircularProgress size={20} />}
        </Button>
    );
};

const CustomDivider = (props: CustomDividerProps): React.ReactElement => {
    return (
        <div style={{ marginTop: props.marginTop, marginBottom: props.marginBottom }}>
            <Divider className="divider" />
        </div>
    );
};

export const UserPasswordView = (props: UserPasswordViewProps): React.ReactElement => {
    return (
        <section className="section" style={props.background}>
            <Container className="container-wide">
                <div style={{ paddingBottom: 40 }}>
                    <Card elevation={0} className="card">
                        <CardContent className="card-content">
                            <div style={{ paddingTop: 0, paddingBottom: 0 }}>
                                <Typography component="span" className="caption black">
                                    {props.isLoading ? (
                                        <Skeleton variant="text" />
                                    ) : (
                                        props.sectionAccountPassword?.caption
                                    )}
                                </Typography>
                            </div>
                            <CustomDivider marginTop={16} marginBottom={8} />
                            <div style={{ paddingTop: 40, paddingBottom: 8 }}>
                                <Grid container spacing={2}>
                                    <Grid item xs={12} sm={3} className="label-centered">
                                        <Typography component="span" className="label">
                                            {props.isLoading ? (
                                                <Skeleton variant="text" />
                                            ) : (
                                                props.sectionAccountPassword?.labelOldPassword
                                            )}
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="40px" />
                                        ) : (
                                            <TextFiedWithPassword
                                                uuid="oldPassword"
                                                fullWidth={true}
                                                value={props.oldPassword}
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                            />
                                        )}
                                    </Grid>
                                    <Grid item xs={12} sm={3} className="label-centered">
                                        <Typography component="span" className="label">
                                            {props.isLoading ? (
                                                <Skeleton variant="text" />
                                            ) : (
                                                props.sectionAccountPassword?.labelNewPassword
                                            )}
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="40px" />
                                        ) : (
                                            <TextFiedWithPassword
                                                uuid="newPassword"
                                                fullWidth={true}
                                                value={props.newPassword}
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                            />
                                        )}
                                    </Grid>
                                    <Grid item xs={12} sm={3} className="label-centered">
                                        <Typography component="span" className="label">
                                            {props.isLoading ? (
                                                <Skeleton variant="text" />
                                            ) : (
                                                props.sectionAccountPassword?.labelConfirmPassword
                                            )}
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="40px" />
                                        ) : (
                                            <TextFiedWithPassword
                                                uuid="confirmPassword"
                                                fullWidth={true}
                                                value={props.confirmPassword}
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                            />
                                        )}
                                    </Grid>
                                </Grid>
                                <CustomDivider marginTop={40} marginBottom={16} />
                                <Grid className="button-container-update">
                                    <div style={{ marginTop: 16, marginBottom: 16 }}>
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="150px" height="40px" />
                                        ) : (
                                            <UpdatePasswordButton {...props} />
                                        )}
                                    </div>
                                </Grid>
                            </div>
                        </CardContent>
                    </Card>
                </div>
            </Container>
        </section>
    );
};
