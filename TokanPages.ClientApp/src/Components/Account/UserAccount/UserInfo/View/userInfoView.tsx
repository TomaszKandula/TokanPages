import * as React from "react";
import Box from "@material-ui/core/Box";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import Container from "@material-ui/core/Container";
import Skeleton from "@material-ui/lab/Skeleton";
import { Button, CircularProgress, Divider, Grid, TextField, Typography, Backdrop } from "@material-ui/core";
import { AuthenticateUserResultDto, SectionAccountInformation } from "../../../../../Api/Models";
import { UserMedia } from "../../../../../Shared/enums";
import { UploadUserMedia } from "../../../../../Shared/Components";
import { AccountFormInput } from "../../../../../Shared/Services/FormValidation";
import { ViewProperties } from "../../../../../Shared/Abstractions";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../../Shared/types";
import { UserInfoStyle } from "./userInfoStyle";

interface UserInfoViewProps extends ViewProperties {
    userStore: AuthenticateUserResultDto;
    accountForm: AccountFormInput;
    userImageName: string;
    isUserActivated: boolean;
    isRequestingVerification: boolean;
    formProgress: boolean;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    switchHandler: (event: ReactChangeEvent) => void;
    saveButtonHandler: () => void;
    verifyButtonHandler: () => void;
    sectionAccountInformation: SectionAccountInformation;
    background?: React.CSSProperties;
}

interface Properties extends UserInfoViewProps {
    value: string;
}

const ReturnFileName = (value: string): string => {
    const maxFileNameLength: number = 8;
    const fileNameLength = value.length;
    const fileNameExtension = value.substring(fileNameLength - 3, fileNameLength);
    const shortFileName = value.substring(0, maxFileNameLength);

    return fileNameLength > maxFileNameLength ? `${shortFileName}~1.${fileNameExtension}` : value;
};

const CustomDivider = (args: { marginTop: number; marginBottom: number }): JSX.Element => {
    const classes = UserInfoStyle();
    return (
        <Box mt={args.marginTop} mb={args.marginBottom}>
            <Divider className={classes.divider} />
        </Box>
    );
};

const RenderText = (props: Properties): JSX.Element => {
    return props.isLoading ? <Skeleton variant="text" /> : <>{props.value}</>;
};

const UpdateAccountButton = (props: UserInfoViewProps): JSX.Element => {
    const classes = UserInfoStyle();
    return (
        <Button
            fullWidth
            type="submit"
            variant="contained"
            onClick={props.saveButtonHandler}
            disabled={props.formProgress}
            className={classes.button_update}
        >
            {!props.formProgress ? props.sectionAccountInformation?.updateButtonText : <CircularProgress size={20} />}
        </Button>
    );
};

const RequestVerificationButton = (props: UserInfoViewProps): JSX.Element => {
    const classes = UserInfoStyle();
    const clickable = (
        <Typography component="span" onClick={props.verifyButtonHandler} className={classes.user_email_verification}>
            request verification
        </Typography>
    );

    const link = (
        <>
            <Typography component="span">&nbsp;(</Typography>
            {clickable}
            <Typography component="span">)</Typography>
        </>
    );

    return props.userStore.isVerified ? <></> : link;
};

const RenderEmailStatus = (props: UserInfoViewProps): JSX.Element => {
    return props.userStore?.isVerified ? (
        <>{props.sectionAccountInformation?.labelEmailStatus?.positive}</>
    ) : (
        <>{props.sectionAccountInformation?.labelEmailStatus?.negative}</>
    );
};

const RenderLoadingOrStatus = (props: UserInfoViewProps): JSX.Element => {
    return props.isLoading ? <Skeleton variant="text" /> : <RenderEmailStatus {...props} />;
};

export const UserInfoView = (props: UserInfoViewProps): JSX.Element => {
    const classes = UserInfoStyle();
    return (
        <section className={classes.section} style={props.background}>
            <Backdrop className={classes.backdrop} open={props.isRequestingVerification}>
                <CircularProgress color="inherit" />
            </Backdrop>
            <Container className={classes.container}>
                <Box pt={15} pb={5}>
                    <Card elevation={0} className={classes.card}>
                        <CardContent className={classes.card_content}>
                            <Box pt={0} pb={0}>
                                <Typography component="span" className={classes.caption}>
                                    <RenderText {...props} value={props.sectionAccountInformation?.caption} />
                                </Typography>
                            </Box>
                            <CustomDivider marginTop={2} marginBottom={1} />
                            <Box pt={5} pb={1}>
                                <Grid container spacing={2}>
                                    <Grid item xs={12} sm={3}>
                                        <Typography component="span" className={classes.label}>
                                            <RenderText
                                                {...props}
                                                value={props.sectionAccountInformation?.labelUserId}
                                            />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        <Typography component="span" className={classes.user_id}>
                                            <RenderText {...props} value={props.userStore?.userId} />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography component="span" className={classes.label}>
                                            <RenderText
                                                {...props}
                                                value={props.sectionAccountInformation?.labelEmailStatus?.label}
                                            />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        <Typography className={classes.user_email_status}>
                                            <RenderLoadingOrStatus {...props} />
                                            <RequestVerificationButton {...props} />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography component="span" className={classes.label}>
                                            <RenderText
                                                {...props}
                                                value={props.sectionAccountInformation?.labelUserAlias}
                                            />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        <Typography component="span" className={classes.user_alias}>
                                            <RenderText {...props} value={props.userStore?.aliasName} />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={3} className={classes.label_centered}>
                                        <Typography component="span" className={classes.label}>
                                            <RenderText
                                                {...props}
                                                value={props.sectionAccountInformation?.labelUserAvatar}
                                            />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.isLoading ? null : (
                                            <UploadUserMedia
                                                mediaTarget={UserMedia.userImage}
                                                handle="userInfoSection_userImage"
                                            />
                                        )}
                                        {props.isLoading ? null : (
                                            <Typography component="span" className={classes.user_avatar_text}>
                                                {ReturnFileName(props.userImageName)}
                                            </Typography>
                                        )}
                                    </Grid>
                                </Grid>
                                <CustomDivider marginTop={4} marginBottom={4} />
                                <Grid container spacing={2}>
                                    <Grid item xs={12} sm={3} className={classes.label_centered}>
                                        <Typography component="span" className={classes.label}>
                                            <RenderText
                                                {...props}
                                                value={props.sectionAccountInformation?.labelFirstName}
                                            />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="40px" />
                                        ) : (
                                            <TextField
                                                required
                                                fullWidth
                                                id="firstName"
                                                name="firstName"
                                                variant="outlined"
                                                value={props.accountForm?.firstName}
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                            />
                                        )}
                                    </Grid>
                                    <Grid item xs={12} sm={3} className={classes.label_centered}>
                                        <Typography component="span" className={classes.label}>
                                            <RenderText
                                                {...props}
                                                value={props.sectionAccountInformation?.labelLastName}
                                            />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="40px" />
                                        ) : (
                                            <TextField
                                                required
                                                fullWidth
                                                id="lastName"
                                                name="lastName"
                                                variant="outlined"
                                                value={props.accountForm?.lastName}
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                            />
                                        )}
                                    </Grid>
                                    <Grid item xs={12} sm={3} className={classes.label_centered}>
                                        <Typography component="span" className={classes.label}>
                                            <RenderText
                                                {...props}
                                                value={props.sectionAccountInformation?.labelEmail}
                                            />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="40px" />
                                        ) : (
                                            <TextField
                                                required
                                                fullWidth
                                                id="email"
                                                name="email"
                                                variant="outlined"
                                                value={props.accountForm?.email}
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                            />
                                        )}
                                    </Grid>
                                    <Grid item xs={12} sm={3} className={classes.label_centered}>
                                        <Typography component="span" className={classes.label}>
                                            <RenderText
                                                {...props}
                                                value={props.sectionAccountInformation?.labelShortBio}
                                            />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="40px" />
                                        ) : (
                                            <TextField
                                                required
                                                fullWidth
                                                multiline
                                                minRows={6}
                                                id="userAboutText"
                                                name="userAboutText"
                                                variant="outlined"
                                                value={props.accountForm?.userAboutText}
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
                                            <UpdateAccountButton {...props} />
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
