import * as React from "react";
import { Link } from "react-router-dom";
import ReactHtmlParser from "react-html-parser";
import Box from "@material-ui/core/Box";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import Container from "@material-ui/core/Container";
import Skeleton from "@material-ui/lab/Skeleton";
import userAccountStyle from "./Styles/userAccountStyle";
import BackupIcon from "@material-ui/icons/Backup";
import { Button, IconButton, CircularProgress, Divider, Grid, TextField, Typography, FormControlLabel } from "@material-ui/core";
import { ISectionAccessDenied, ISectionAccountInformation, ISectionAccountPassword, ISectionAccountRemoval } from "../../Api/Models";
import { CustomSwitchStyle } from "./Styles/customSwitchStyle";

interface IBinding 
{
    bind: IProperties;
}

interface IProperties
{    
    isLoading: boolean;
    isAnonymous: boolean;
    userId: string;
    userAlias: string;
    firstName: string;
    lastName: string;
    email: string;
    shortBio: string;
    userAvatar: string;
    isUserActivated: boolean;
    accountFormProgress: boolean;
    accountFormHandler: any;
    accountSwitchHandler: any;
    accountButtonHandler: any;
    avatarUploadProgress: boolean;
    avatarButtonHandler: any;
    oldPassword: string;
    newPassword: string;
    confirmPassword: string;
    passwordFormProgress: boolean;
    passwordFormHandler: any;
    passwordButtonHandler: any;
    deleteButtonHandler: any;
    deleteAccountProgress: boolean;
    sectionAccessDenied: ISectionAccessDenied;
    sectionAccountInformation: ISectionAccountInformation;
    sectionAccountPassword: ISectionAccountPassword;
    sectionAccountRemoval: ISectionAccountRemoval;
}

const UserAccountView = (props: IBinding): JSX.Element => 
{
    const classes = userAccountStyle();

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

    const UpdatePasswordButton = (): JSX.Element => 
    {
        return(
            <Button fullWidth onClick={props.bind?.passwordButtonHandler} type="submit" variant="contained" 
                disabled={props.bind?.passwordFormProgress} className={classes.button_update}>
                {props.bind?.passwordFormProgress &&  <CircularProgress size={20} />}
                {!props.bind?.passwordFormProgress && props.bind?.sectionAccountPassword?.updateButtonText}
            </Button>
        );
    }

    const DeleteAccountButton = (): JSX.Element => 
    {
        return(
            <Button fullWidth onClick={props.bind?.deleteButtonHandler} type="submit" variant="contained" 
                disabled={props.bind?.deleteAccountProgress} className={classes.delete_update}>
                {props.bind?.deleteAccountProgress &&  <CircularProgress size={20} />}
                {!props.bind?.deleteAccountProgress && props.bind?.sectionAccountRemoval?.deleteButtonText}
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

    const HomeButton = (): JSX.Element => 
    {
        return(
            <Link to="/" className={classes.home_link}>
                <Button fullWidth variant="contained" className={classes.home_button} disabled={props.bind?.isLoading}>
                    {props.bind?.sectionAccessDenied?.homeButtonText}
                </Button>
            </Link>
        );
    }

    const CustomDivider = (args: { marginTop: number, marginBottom: number }) => 
    {
        return(
            <Box mt={args.marginTop} mb={args.marginBottom}>
                <Divider className={classes.divider} />
            </Box>
        );
    }

    return(
        <>
        <section className={classes.section} style={props.bind?.isAnonymous ? { display: "block" }: { display: "none" }}>
            <Container maxWidth="md">
                <Box pt={15} pb={8}>
                    <Card elevation={0} className={classes.card}>
                        <CardContent className={classes.card_content}>
                            <Box pt={0} pb={0}>
                                <Typography className={classes.caption}>
                                    {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.sectionAccessDenied?.accessDeniedCaption}
                                </Typography>
                            </Box>
                            <CustomDivider marginTop={2} marginBottom={1} />
                            <Box pt={3} pb={3}>
                                <Typography component="span" className={classes.access_denied_prompt}>
                                    {props.bind?.isLoading ? <Skeleton variant="text" height="100px" /> : ReactHtmlParser(props.bind?.sectionAccessDenied?.accessDeniedPrompt)}
                                </Typography>
                            </Box>
                            {props.bind?.isLoading ? <Skeleton variant="rect" width="100%" height="40px" /> : <HomeButton />}
                        </CardContent>
                    </Card>
                </Box>
            </Container>
        </section>
        <section className={classes.section} style={props.bind?.isAnonymous ? { display: "none" }: { display: "block" }}>
            <Container maxWidth="md">
                <Box pt={15}>
                    <Card elevation={0} className={classes.card}>
                        <CardContent className={classes.card_content}>
                            <Box pt={0} pb={0}>
                                <Typography className={classes.caption}>
                                    {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.sectionAccountInformation?.caption}
                                </Typography>
                            </Box>
                            <CustomDivider marginTop={2} marginBottom={1} />
                            <Box pt={5} pb={1}>
                                <Grid container spacing={2}>
                                    <Grid item xs={12} sm={3}>
                                        <Typography className={classes.label}>
                                            {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.sectionAccountInformation?.labelUserId}
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        <Typography className={classes.user_id}>
                                            {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.userId}
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography className={classes.label}>
                                            {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.sectionAccountInformation?.labelUserAlias}
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        <Typography className={classes.user_alias}>
                                            {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.userAlias}
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography className={classes.label}>
                                            {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.sectionAccountInformation?.labelUserAvatar}
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        <Box className={classes.user_avatar_box}>
                                            <Typography component="span" className={classes.user_avatar}>
                                                {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.userAvatar}
                                            </Typography>
                                            {props.bind?.isLoading ? null : <UploadAvatarButton />}
                                        </Box>
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography className={classes.label}>
                                            {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.sectionAccountInformation?.labelFirstName}
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="40px" /> 
                                        : <TextField required fullWidth onChange={props.bind?.accountFormHandler} value={props.bind?.firstName}
                                            variant="outlined" name="firstName" id="firstName" />}
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography className={classes.label}>
                                            {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.sectionAccountInformation?.labelLastName}
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="40px" />
                                        : <TextField required fullWidth onChange={props.bind?.accountFormHandler} value={props.bind?.lastName}
                                            variant="outlined" name="lastName" id="lastName" />}
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography className={classes.label}>
                                            {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.sectionAccountInformation?.labelEmail}
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="40px" />
                                        : <TextField required fullWidth onChange={props.bind?.accountFormHandler} value={props.bind?.email}
                                            variant="outlined" name="email" id="email" />}
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography className={classes.label}>
                                            {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.sectionAccountInformation?.labelShortBio}
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="40px" />
                                        : <TextField required fullWidth multiline onChange={props.bind?.accountFormHandler} value={props.bind?.shortBio}
                                            minRows={6} variant="outlined" name="shortBio" id="shortBio" />}
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography className={classes.label}>
                                            {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.sectionAccountInformation?.labelIsActivated}
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="40px" />
                                        : <FormControlLabel
                                            control={<CustomSwitchStyle checked={props.bind?.isUserActivated} onChange={props.bind?.accountSwitchHandler} name="checked" />}
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
                <Box pt={8}>
                    <Card elevation={0} className={classes.card}>
                        <CardContent className={classes.card_content}>
                            <Box pt={0} pb={0}>
                                <Typography className={classes.caption}>
                                    {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.sectionAccountPassword?.caption}
                                </Typography>
                            </Box>
                            <CustomDivider marginTop={2} marginBottom={1} />
                            <Box pt={5} pb={1}>
                                <Grid container spacing={2}>
                                    <Grid item xs={12} sm={3}>
                                        <Typography className={classes.label}>
                                            {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.sectionAccountPassword?.labelOldPassword}
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="40px" />
                                        : <TextField required fullWidth onChange={props.bind?.passwordFormHandler} value={props.bind?.oldPassword}
                                            variant="outlined" name="oldPassword" id="oldPassword" type="password" />}
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography className={classes.label}>
                                            {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.sectionAccountPassword?.labelNewPassword}
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="40px" />
                                        : <TextField required fullWidth onChange={props.bind?.passwordFormHandler} value={props.bind?.newPassword}
                                            variant="outlined" name="newPassword" id="newPassword" type="password" />}
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography className={classes.label}>
                                            {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.sectionAccountPassword?.labelConfirmPassword}
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="40px" />
                                        : <TextField required fullWidth onChange={props.bind?.passwordFormHandler} value={props.bind?.confirmPassword}
                                            variant="outlined" name="confirmPassword" id="confirmPassword" type="password" />}
                                    </Grid>
                                </Grid>
                                <CustomDivider marginTop={5} marginBottom={2} />
                                <Grid className={classes.button_container_update}>
                                    <Box my={2}>
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="150px" height="40px" /> 
                                        : <UpdatePasswordButton />}
                                    </Box>
                                </Grid>
                            </Box>
                        </CardContent>
                    </Card>
                </Box>
                <Box pt={8} pb={10}>
                    <Card elevation={0} className={classes.card}>
                        <CardContent className={classes.card_content}>
                            <Box pt={0} pb={0}>
                                <Typography className={classes.caption}>
                                    {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.sectionAccountRemoval?.caption}
                                </Typography>
                            </Box>
                            <CustomDivider marginTop={2} marginBottom={1} />
                            <Box pt={1} pb={1}>
                                <Grid container spacing={2}>
                                    <Grid item>
                                        <Typography className={classes.label}>
                                            {props.bind?.isLoading ? <Skeleton variant="text" /> : ReactHtmlParser(props.bind?.sectionAccountRemoval?.warningText)}
                                        </Typography>
                                    </Grid>
                                </Grid>
                                <CustomDivider marginTop={2} marginBottom={2} />
                                <Grid className={classes.button_container_update}>
                                    <Box my={2}>
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="150px" height="40px" /> 
                                        : <DeleteAccountButton />}
                                    </Box>
                                </Grid>
                            </Box>
                        </CardContent>
                    </Card>
                </Box>
            </Container>
        </section>
        </>
    );
}

export default UserAccountView;
