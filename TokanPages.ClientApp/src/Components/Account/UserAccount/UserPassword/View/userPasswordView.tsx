import * as React from "react";
import { Link } from "react-router-dom";
import ReactHtmlParser from "react-html-parser";
import Box from "@material-ui/core/Box";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import Container from "@material-ui/core/Container";
import Skeleton from "@material-ui/lab/Skeleton";
import { UserPasswordStyle } from "./userPasswordStyle";

import { 
    Button, 
    CircularProgress, 
    Divider, 
    Grid, 
    TextField, 
    Typography
} from "@material-ui/core";

import { 
    ISectionAccessDenied, 
    ISectionAccountPassword
} from "../../../../../Api/Models";

interface IBinding 
{
    bind: IProperties;
}

interface IProperties
{    
    isLoading: boolean;
    isAnonymous: boolean;
    oldPassword: string;
    newPassword: string;
    confirmPassword: string;
    passwordFormProgress: boolean;
    passwordFormHandler: any;
    passwordButtonHandler: any;
    sectionAccessDenied: ISectionAccessDenied;
    sectionAccountPassword: ISectionAccountPassword;
}

export const UserPasswordView = (props: IBinding): JSX.Element => 
{
    const classes = UserPasswordStyle();

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

            </Container>
        </section>
        </>
    );
}