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
import { ISectionAccessDenied, ISectionBasicInformation } from "../../Api/Models";
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
    formHandler: any;
    switchHandler: any;
    updateProgress: boolean;
    updateButtonHandler: any;
    uploadProgress: boolean;
    uploadAvatarButtonHandler: any;
    sectionAccessDenied: ISectionAccessDenied;
    sectionBasicInformation: ISectionBasicInformation;
}

const UserAccountView = (props: IBinding): JSX.Element => 
{
    const classes = userAccountStyle();

    const UpdateButton = (): JSX.Element => 
    {
        return(
            <Button fullWidth onClick={props.bind?.updateButtonHandler} type="submit" variant="contained" 
                disabled={props.bind?.updateProgress} className={classes.button_update}>
                {props.bind?.updateProgress &&  <CircularProgress size={20} />}
                {!props.bind?.updateProgress && props.bind?.sectionBasicInformation?.updateButtonText}
            </Button>
        );
    }

    const UploadAvatarButton = (): JSX.Element => 
    {
        return(
            <IconButton onClick={props.bind?.uploadAvatarButtonHandler} size="small"
                disabled={props.bind?.uploadProgress} className={classes.button_upload}>
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
        <section className={classes.section}>
            <Container maxWidth="md">
                <Box py={15}>
                    <Card elevation={0} className={classes.card}>
                        <CardContent className={classes.card_content} style={props.bind?.isAnonymous ? { display: "block" }: { display: "none" }}>
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
                        <CardContent className={classes.card_content} style={props.bind?.isAnonymous ? { display: "none" }: { display: "block" }}>
                            <Box pt={0} pb={0}>
                                <Typography className={classes.caption}>
                                    {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.sectionBasicInformation?.caption}
                                </Typography>
                            </Box>
                            <CustomDivider marginTop={2} marginBottom={1} />
                            <Box pt={5} pb={1}>
                                <Grid container spacing={2}>
                                    <Grid item xs={12} sm={3}>
                                        <Typography className={classes.label}>
                                            {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.sectionBasicInformation?.labelUserId}
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        <Typography className={classes.user_id}>
                                            {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.userId}
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography className={classes.label}>
                                            {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.sectionBasicInformation?.labelUserAlias}
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        <Typography className={classes.user_alias}>
                                            {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.userAlias}
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography className={classes.label}>
                                            {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.sectionBasicInformation?.labelUserAvatar}
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
                                            {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.sectionBasicInformation?.labelFirstName}
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="40px" /> 
                                        : <TextField required fullWidth onChange={props.bind?.formHandler} value={props.bind?.firstName}
                                            variant="outlined" name="firstName" id="firstName" />}
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography className={classes.label}>
                                            {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.sectionBasicInformation?.labelLastName}
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="40px" />
                                        : <TextField required fullWidth onChange={props.bind?.formHandler} value={props.bind?.lastName}
                                            variant="outlined" name="lastName" id="lastName" />}
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography className={classes.label}>
                                            {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.sectionBasicInformation?.labelEmail}
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="40px" />
                                        : <TextField required fullWidth onChange={props.bind?.formHandler} value={props.bind?.email}
                                            variant="outlined" name="email" id="email" />}
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography className={classes.label}>
                                            {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.sectionBasicInformation?.labelShortBio}
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="40px" />
                                        : <TextField required fullWidth multiline onChange={props.bind?.formHandler} value={props.bind?.shortBio}
                                            minRows={6} variant="outlined" name="shortBio" id="shortBio" />}
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography className={classes.label}>
                                            {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.sectionBasicInformation?.labelIsActivated}
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="40px" />
                                        : <FormControlLabel
                                            control={<CustomSwitchStyle checked={props.bind?.isUserActivated} onChange={props.bind?.switchHandler} name="checked" />}
                                            label={props.bind?.sectionBasicInformation?.isActivatedText} />}
                                    </Grid>
                                </Grid>
                                <CustomDivider marginTop={5} marginBottom={2} />
                                <Grid className={classes.button_container_update}>
                                    <Box my={2}>
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="150px" height="40px" /> 
                                        : <UpdateButton />}
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

export default UserAccountView;
