import * as React from "react";
import Box from "@material-ui/core/Box";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import Container from "@material-ui/core/Container";
import Skeleton from "@material-ui/lab/Skeleton";
import BackupIcon from "@material-ui/icons/Backup";
import { IValidateAccountForm } from "../../../../../Shared/Services/FormValidation";
import { UserInfoStyle, CustomSwitchStyle } from "./userInfoStyle";

import { 
    Button, 
    IconButton, 
    CircularProgress, 
    Divider, 
    Grid, 
    TextField, 
    Typography, 
    FormControlLabel 
} from "@material-ui/core";

import { 
    IAuthenticateUserResultDto,
    ISectionAccessDenied, 
    ISectionAccountInformation, 
} from "../../../../../Api/Models";

interface IBinding 
{
    bind: IProperties;
}

interface IProperties
{    
    isLoading: boolean;
    userStore: IAuthenticateUserResultDto;
    accountForm: IValidateAccountForm;
    isUserActivated: boolean;
    accountFormProgress: boolean;
    accountFormHandler: any;
    accountSwitchHandler: any;
    accountButtonHandler: any;
    avatarUploadProgress: boolean;
    avatarButtonHandler: any;
    sectionAccessDenied: ISectionAccessDenied;
    sectionAccountInformation: ISectionAccountInformation;
}

export const UserInfoView = (props: IBinding): JSX.Element => 
{
    const classes = UserInfoStyle();

    const UpdateAccountButton = (): JSX.Element => 
    {
        return(
            <Button fullWidth onClick={props.bind?.accountButtonHandler} type="submit" variant="contained" 
                disabled={props.bind?.accountFormProgress} className={classes.button_update}>
                {props.bind?.accountFormProgress &&  <CircularProgress size={20} />}
                {!props.bind?.accountFormProgress && props.bind?.sectionAccountInformation?.updateButtonText}
            </Button>
        );
    }

    const UploadAvatarButton = (): JSX.Element => 
    {
        return(
            <IconButton onClick={props.bind?.avatarButtonHandler} size="small"
                disabled={props.bind?.avatarUploadProgress} className={classes.button_upload}>
                <BackupIcon />
            </IconButton>
        );
    }

    const CustomDivider = (args: { marginTop: number, marginBottom: number }): JSX.Element => 
    {
        return(
            <Box mt={args.marginTop} mb={args.marginBottom}>
                <Divider className={classes.divider} />
            </Box>
        );
    }

    const RenderText = (args: { value: string }): JSX.Element => 
    {
        return props.bind?.isLoading ? <Skeleton variant="text" /> : <>{args.value}</>;
    }

    return(
        <section className={classes.section}>
            <Container maxWidth="md">
                <Box pt={15} pb={5}>
                    <Card elevation={0} className={classes.card}>
                        <CardContent className={classes.card_content}>
                            <Box pt={0} pb={0}>
                                <Typography className={classes.caption}>
                                    <RenderText value={props.bind?.sectionAccountInformation?.caption} />
                                </Typography>
                            </Box>
                            <CustomDivider marginTop={2} marginBottom={1} />
                            <Box pt={5} pb={1}>
                                <Grid container spacing={2}>
                                    <Grid item xs={12} sm={3}>
                                        <Typography className={classes.label}>
                                            <RenderText value={props.bind?.sectionAccountInformation?.labelUserId} />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        <Typography className={classes.user_id}>
                                            <RenderText value={props.bind?.userStore?.userId} />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography className={classes.label}>
                                            <RenderText value={props.bind?.sectionAccountInformation?.labelUserAlias} />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        <Typography className={classes.user_alias}>
                                            <RenderText value={props.bind?.userStore?.aliasName} />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography className={classes.label}>
                                            <RenderText value={props.bind?.sectionAccountInformation?.labelUserAvatar} />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        <Box className={classes.user_avatar_box}>
                                            <Typography component="span" className={classes.user_avatar}>
                                                <RenderText value={props.bind?.userStore?.avatarName} />
                                            </Typography>
                                            {props.bind?.isLoading ? null : <UploadAvatarButton />}
                                        </Box>
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography className={classes.label}>
                                            <RenderText value={props.bind?.sectionAccountInformation?.labelFirstName} />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="40px" /> 
                                        : <TextField required fullWidth value={props.bind?.accountForm?.firstName}
                                            onChange={props.bind?.accountFormHandler} 
                                            variant="outlined" name="firstName" id="firstName" />}
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography className={classes.label}>
                                            <RenderText value={props.bind?.sectionAccountInformation?.labelLastName} />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="40px" />
                                        : <TextField required fullWidth value={props.bind?.accountForm?.lastName}
                                            onChange={props.bind?.accountFormHandler} 
                                            variant="outlined" name="lastName" id="lastName" />}
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography className={classes.label}>
                                            <RenderText value={props.bind?.sectionAccountInformation?.labelEmail} />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="40px" />
                                        : <TextField required fullWidth value={props.bind?.accountForm?.email}
                                            onChange={props.bind?.accountFormHandler}
                                            variant="outlined" name="email" id="email" />}
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography className={classes.label}>
                                            <RenderText value={props.bind?.sectionAccountInformation?.labelShortBio} />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="40px" />
                                        : <TextField required fullWidth multiline value={props.bind?.accountForm?.userAboutText}
                                            onChange={props.bind?.accountFormHandler} 
                                            minRows={6} variant="outlined" name="userAboutText" id="userAboutText" />}
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography className={classes.label}>
                                            <RenderText value={props.bind?.sectionAccountInformation?.labelIsActivated} />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="40px" />
                                        : <FormControlLabel
                                            control={<CustomSwitchStyle checked={props.bind?.isUserActivated} 
                                            onChange={props.bind?.accountSwitchHandler} name="checked" />}
                                            label={props.bind?.sectionAccountInformation?.isActivatedText} />}
                                    </Grid>
                                </Grid>
                                <CustomDivider marginTop={5} marginBottom={2} />
                                <Grid className={classes.button_container_update}>
                                    <Box my={2}>
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="150px" height="40px" /> 
                                        : <UpdateAccountButton />}
                                    </Box>
                                </Grid>
                            </Box>
                        </CardContent>
                    </Card>
                </Box>
            </Container>
        </section>
    );
}
