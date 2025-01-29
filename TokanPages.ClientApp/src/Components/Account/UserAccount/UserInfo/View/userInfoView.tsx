import * as React from "react";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import Container from "@material-ui/core/Container";
import Skeleton from "@material-ui/lab/Skeleton";
import { Button, CircularProgress, Grid, TextField, Typography, Backdrop } from "@material-ui/core";
import { AuthenticateUserResultDto, SectionAccountInformation } from "../../../../../Api/Models";
import { GET_USER_IMAGE } from "../../../../../Api/Request";
import { UserMedia } from "../../../../../Shared/enums";
import { CustomDivider, UploadUserMedia } from "../../../../../Shared/Components";
import { AccountFormInput } from "../../../../../Shared/Services/FormValidation";
import { ViewProperties } from "../../../../../Shared/Abstractions";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../../Shared/types";
import { UserInfoProps } from "../userInfo";

interface UserInfoViewProps extends ViewProperties, UserInfoProps {
    fileUploadingCustomHandle?: string;
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
    userAbout?: {
        multiline?: boolean;
        minRows?: number;
    };
}

interface Properties extends UserInfoViewProps {
    value: string;
}

const RenderText = (props: Properties): React.ReactElement => {
    return props.isLoading ? <Skeleton variant="text" /> : <>{props.value}</>;
};

const UpdateAccountButton = (props: UserInfoViewProps): React.ReactElement => {
    return (
        <Button
            fullWidth
            type="submit"
            variant="contained"
            onClick={props.saveButtonHandler}
            disabled={props.formProgress}
            className="button-update"
        >
            {!props.formProgress ? props.sectionAccountInformation?.updateButtonText : <CircularProgress size={20} />}
        </Button>
    );
};

const RequestVerificationButton = (props: UserInfoViewProps): React.ReactElement => {
    const clickable = (
        <Typography component="span" onClick={props.verifyButtonHandler} className="user-email-verification">
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

const RenderEmailStatus = (props: UserInfoViewProps): React.ReactElement => {
    return props.userStore?.isVerified ? (
        <>{props.sectionAccountInformation?.labelEmailStatus?.positive}</>
    ) : (
        <>{props.sectionAccountInformation?.labelEmailStatus?.negative}</>
    );
};

const RenderLoadingOrStatus = (props: UserInfoViewProps): React.ReactElement => {
    return props.isLoading ? <Skeleton variant="text" /> : <RenderEmailStatus {...props} />;
};

export const UserInfoView = (props: UserInfoViewProps): React.ReactElement => {
    const previewImage = GET_USER_IMAGE
    .replace("{id}", props.userStore.userId)
    .replace("{name}", props.userImageName ?? "");

    return (
        <section className="section" style={props.background}>
            <Backdrop className="backdrop" open={props.isRequestingVerification}>
                <CircularProgress color="inherit" />
            </Backdrop>
            <Container className="container-wide">
                <div style={{ paddingTop: 120, paddingBottom: 40 }}>
                    <Card elevation={0} className="card">
                        <CardContent className="card-content">

                            <div style={{ paddingTop: 0, paddingBottom: 0 }}>
                                <Typography component="span" className="caption black">
                                    <RenderText {...props} value={props.sectionAccountInformation?.caption} />
                                </Typography>
                            </div>

                            <CustomDivider marginTop={16} marginBottom={8} />

                            <div style={{ paddingTop: 24, paddingBottom: 8 }}>
                                <Grid container spacing={2}>
                                    <Grid item xs={12} sm={3}>
                                        <Typography component="span" className="label">
                                            <RenderText
                                                {...props}
                                                value={props.sectionAccountInformation?.labelUserId}
                                            />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        <Typography component="span" className="user-id">
                                            <RenderText {...props} value={props.userStore?.userId} />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography component="span" className="label">
                                            <RenderText
                                                {...props}
                                                value={props.sectionAccountInformation?.labelEmailStatus?.label}
                                            />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        <Typography className="user-email-status">
                                            <RenderLoadingOrStatus {...props} />
                                            <RequestVerificationButton {...props} />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography component="span" className="label">
                                            <RenderText
                                                {...props}
                                                value={props.sectionAccountInformation?.labelUserAlias}
                                            />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        <Typography component="span" className="user-alias">
                                            <RenderText {...props} value={props.userStore?.aliasName} />
                                        </Typography>
                                    </Grid>
                                </Grid>

                                <CustomDivider marginTop={24} marginBottom={24} />

                                <Grid container spacing={2}>
                                    <Grid item xs={12} sm={3} className="label-centered">
                                        <Typography component="span" className="label">
                                            <RenderText
                                                {...props}
                                                value={props.sectionAccountInformation?.labelUserAvatar}
                                            />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.isLoading ? null : (
                                            <UploadUserMedia
                                                customHandle={props.fileUploadingCustomHandle}
                                                mediaTarget={UserMedia.userImage}
                                                handle="userInfoSection_userImage"
                                                previewImage={previewImage}
                                            />
                                        )}
                                    </Grid>
                                </Grid>

                                <CustomDivider marginTop={24} marginBottom={32} />

                                <Grid container spacing={2}>
                                    <Grid item xs={12} sm={3} className="label-centered">
                                        <Typography component="span" className="label">
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
                                    <Grid item xs={12} sm={3} className="label-centered">
                                        <Typography component="span" className="label">
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
                                    <Grid item xs={12} sm={3} className="label-centered">
                                        <Typography component="span" className="label">
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
                                    <Grid item xs={12} sm={3} className="label-centered">
                                        <Typography component="span" className="label">
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
                                                multiline={props.userAbout?.multiline}
                                                minRows={props.userAbout?.minRows}
                                                id="userAboutText"
                                                name="userAboutText"
                                                variant="outlined"
                                                value={props.accountForm?.userAboutText}
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
                                            <UpdateAccountButton {...props} />
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
