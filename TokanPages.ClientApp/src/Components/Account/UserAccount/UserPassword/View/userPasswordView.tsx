import * as React from "react";
import Box from "@material-ui/core/Box";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import Container from "@material-ui/core/Container";
import Skeleton from "@material-ui/lab/Skeleton";
import { ViewProperties } from "../../../../../Shared/Abstractions";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../../Shared/types";
import { TextFiedWithPassword } from "../../../../../Shared/Components";
import { UserPasswordStyle } from "./userPasswordStyle";

import { Button, CircularProgress, Divider, Grid, Typography } from "@material-ui/core";

import { SectionAccessDenied, SectionAccountPassword } from "../../../../../Api/Models";

interface Properties extends ViewProperties {
    oldPassword: string;
    newPassword: string;
    confirmPassword: string;
    formProgress: boolean;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    buttonHandler: () => void;
    sectionAccessDenied: SectionAccessDenied;
    sectionAccountPassword: SectionAccountPassword;
}

const UpdatePasswordButton = (props: Properties): JSX.Element => {
    const classes = UserPasswordStyle();
    return (
        <Button
            fullWidth
            type="submit"
            variant="contained"
            onClick={props.buttonHandler}
            disabled={props.formProgress}
            className={classes.button_update}
        >
            {!props.formProgress ? props.sectionAccountPassword?.updateButtonText : <CircularProgress size={20} />}
        </Button>
    );
};

const CustomDivider = (args: { marginTop: number; marginBottom: number }) => {
    const classes = UserPasswordStyle();
    return (
        <Box mt={args.marginTop} mb={args.marginBottom}>
            <Divider className={classes.divider} />
        </Box>
    );
};

export const UserPasswordView = (props: Properties): JSX.Element => {
    const classes = UserPasswordStyle();
    return (
        <section className={classes.section}>
            <Container maxWidth="md">
                <Box pt={5} pb={5}>
                    <Card elevation={0} className={classes.card}>
                        <CardContent className={classes.card_content}>
                            <Box pt={0} pb={0}>
                                <Typography component="span" className={classes.caption}>
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
                                    <Grid item xs={12} sm={3}>
                                        <Typography component="span" className={classes.label}>
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

                                    <Grid item xs={12} sm={3}>
                                        <Typography component="span" className={classes.label}>
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
                                    <Grid item xs={12} sm={3}>
                                        <Typography component="span" className={classes.label}>
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
                                <Grid className={classes.button_container_update}>
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
