import * as React from "react";
import { Button, CircularProgress, Grid, Typography } from "@material-ui/core";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import Container from "@material-ui/core/Container";
import Skeleton from "@material-ui/lab/Skeleton";
import { SectionAccountPassword } from "../../../../../Api/Models";
import { ViewProperties } from "../../../../../Shared/Abstractions";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../../Shared/types";
import { CustomDivider, TextFieldWithPassword } from "../../../../../Shared/Components";
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

export const UserPasswordView = (props: UserPasswordViewProps): React.ReactElement => {
    return (
        <section className={`section ${props.background ?? ""}`}>
            <Container className="container-wide">
                <div className="pb-40">
                    <Card elevation={0} className="card">
                        <CardContent className="card-content">
                            <Typography component="span" className="caption black">
                                {props.isLoading ? <Skeleton variant="text" /> : props.sectionAccountPassword?.caption}
                            </Typography>

                            <CustomDivider mt={15} mb={8} />

                            <div className="pt-40 pb-8">
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
                                            <TextFieldWithPassword
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
                                            <TextFieldWithPassword
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
                                            <TextFieldWithPassword
                                                uuid="confirmPassword"
                                                fullWidth={true}
                                                value={props.confirmPassword}
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                            />
                                        )}
                                    </Grid>
                                </Grid>

                                <CustomDivider mt={40} mb={15} />

                                <Grid className="button-container-update">
                                    <div className="mt-15 mb-15">
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
