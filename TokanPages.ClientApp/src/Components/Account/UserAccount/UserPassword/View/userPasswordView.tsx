import * as React from "react";
import { Button, CircularProgress, Divider, Grid, Typography } from "@material-ui/core";
import Box from "@material-ui/core/Box";
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

const CustomDivider = (args: { marginTop: number; marginBottom: number }) => {
    return (
        <Box mt={args.marginTop} mb={args.marginBottom}>
            <Divider className="divider" />
        </Box>
    );
};

export const UserPasswordView = (props: UserPasswordViewProps): React.ReactElement => {
    return (
        <section className="section" style={props.background}>
            <Container className="container-wide">
                <Box pb={5}>
                    <Card elevation={0} className="card">
                        <CardContent className="card-content">
                            <Box pt={0} pb={0}>
                                <Typography component="span" className="caption black">
                                    {props.isLoading ? (
                                        <Skeleton variant="text" />
                                    ) : (
                                        props.sectionAccountPassword?.caption
                                    )}
                                </Typography>
                            </Box>
                            <CustomDivider marginTop={2} marginBottom={1} />
                            <Box pt={5} pb={1}>
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
                                <CustomDivider marginTop={5} marginBottom={2} />
                                <Grid className="button-container-update">
                                    <Box my={2}>
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="150px" height="40px" />
                                        ) : (
                                            <UpdatePasswordButton {...props} />
                                        )}
                                    </Box>
                                </Grid>
                            </Box>
                        </CardContent>
                    </Card>
                </Box>
            </Container>
        </section>
    );
};
