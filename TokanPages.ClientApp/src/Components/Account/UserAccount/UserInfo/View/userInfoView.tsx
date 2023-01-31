import * as React from "react";
import Box from "@material-ui/core/Box";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import Container from "@material-ui/core/Container";
import Skeleton from "@material-ui/lab/Skeleton";
import { UserMedia } from "../../../../../Shared/enums";
import { UploadUserMedia } from "../../../../../Shared/Components";
import { AccountFormInput } from "../../../../../Shared/Services/FormValidation";
import { ViewProperties } from "../../../../../Shared/interfaces";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../../Shared/types";
import { UserInfoStyle, CustomSwitchStyle } from "./userInfoStyle";

import { 
    Button, 
    CircularProgress, 
    Divider, 
    Grid, 
    TextField, 
    Typography, 
    FormControlLabel 
} from "@material-ui/core";

import { 
    AuthenticateUserResultDto,
    SectionAccessDenied, 
    SectionAccountInformation, 
} from "../../../../../Api/Models";

interface BaseProperties extends ViewProperties
{    
    userStore: AuthenticateUserResultDto;
    accountForm: AccountFormInput;
    userImageName: string;
    isUserActivated: boolean;
    accountFormProgress: boolean;
    accountKeyHandler: (event: ReactKeyboardEvent) => void;
    accountFormHandler: (event: ReactChangeEvent) => void;
    accountSwitchHandler: (event: ReactChangeEvent) => void;
    accountButtonHandler: () => void;
    sectionAccessDenied: SectionAccessDenied;
    sectionAccountInformation: SectionAccountInformation;
}

interface Properties extends BaseProperties
{
    value: string;
}

const ReturnFileName = (value: string): string => 
{
    const maxFileNameLength: number = 8;
    const fileNameLength = value.length;
    const fileNameExtension = value.substring(fileNameLength - 3, fileNameLength);
    const shortFileName = value.substring(0, maxFileNameLength);

    return fileNameLength > maxFileNameLength ? `${shortFileName}~1.${fileNameExtension}` : value;
}

const CustomDivider = (args: { marginTop: number, marginBottom: number }): JSX.Element => 
{
    const classes = UserInfoStyle();
    return(
        <Box mt={args.marginTop} mb={args.marginBottom}>
            <Divider className={classes.divider} />
        </Box>
    );
}

const RenderText = (props: Properties): JSX.Element => 
{
    return props.isLoading ? <Skeleton variant="text" /> : <>{props.value}</>;
}

const UpdateAccountButton = (props: BaseProperties): JSX.Element => 
{
    const classes = UserInfoStyle();
    return(
        <Button 
            fullWidth 
            type="submit" 
            variant="contained" 
            onClick={props.accountButtonHandler} 
            disabled={props.accountFormProgress} 
            className={classes.button_update}>
            {!props.accountFormProgress 
            ? props.sectionAccountInformation?.updateButtonText 
            : <CircularProgress size={20} />}
        </Button>
    );
}

export const UserInfoView = (props: BaseProperties): JSX.Element => 
{
    const classes = UserInfoStyle();
    return(
        <section className={classes.section}>
            <Container maxWidth="md">
                <Box pt={15} pb={5}>
                    <Card elevation={0} className={classes.card}>
                        <CardContent className={classes.card_content}>
                            <Box pt={0} pb={0}>
                                <Typography component="span" className={classes.caption}>
                                    <RenderText 
                                        {...props} 
                                        value={props.sectionAccountInformation?.caption} 
                                    />
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
                                            <RenderText 
                                                {...props} 
                                                value={props.userStore?.userId} 
                                            />
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
                                            <RenderText 
                                                {...props} 
                                                value={props.userStore?.aliasName} 
                                            />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography component="span" className={classes.label}>
                                            <RenderText 
                                                {...props} 
                                                value={props.sectionAccountInformation?.labelUserAvatar}
                                            />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        <Box className={classes.user_avatar_box}>
                                            {props.isLoading 
                                            ? null 
                                            : <UploadUserMedia mediaTarget={UserMedia.userImage} handle="userInfoSection_userImage" />}
                                            {props.isLoading 
                                            ? 
                                            null 
                                            : <Typography component="span" className={classes.user_avatar_text}>
                                                {ReturnFileName(props.userImageName)}
                                            </Typography>}
                                        </Box>
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography component="span" className={classes.label}>
                                            <RenderText 
                                                {...props} 
                                                value={props.sectionAccountInformation?.labelFirstName}
                                            />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="40px" /> 
                                        : <TextField 
                                            required 
                                            fullWidth 
                                            id="firstName"
                                            name="firstName" 
                                            variant="outlined" 
                                            value={props.accountForm?.firstName}
                                            onKeyUp={props.accountKeyHandler}
                                            onChange={props.accountFormHandler} 
                                        />}
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography component="span" className={classes.label}>
                                            <RenderText 
                                                {...props} 
                                                value={props.sectionAccountInformation?.labelLastName} 
                                            />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="40px" />
                                        : <TextField 
                                            required 
                                            fullWidth 
                                            id="lastName"
                                            name="lastName" 
                                            variant="outlined" 
                                            value={props.accountForm?.lastName}
                                            onKeyUp={props.accountKeyHandler}
                                            onChange={props.accountFormHandler} 
                                        />}
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography component="span" className={classes.label}>
                                            <RenderText 
                                                {...props} 
                                                value={props.sectionAccountInformation?.labelEmail}
                                            />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="40px" />
                                        : <TextField 
                                            required 
                                            fullWidth 
                                            id="email"
                                            name="email" 
                                            variant="outlined" 
                                            value={props.accountForm?.email}
                                            onKeyUp={props.accountKeyHandler}
                                            onChange={props.accountFormHandler}
                                        />}
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography component="span" className={classes.label}>
                                            <RenderText 
                                                {...props} 
                                                value={props.sectionAccountInformation?.labelShortBio}
                                            />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="40px" />
                                        : <TextField 
                                            required 
                                            fullWidth 
                                            multiline 
                                            minRows={6} 
                                            id="userAboutText"
                                            name="userAboutText" 
                                            variant="outlined" 
                                            value={props.accountForm?.userAboutText}
                                            onKeyUp={props.accountKeyHandler}
                                            onChange={props.accountFormHandler} 
                                        />}
                                    </Grid>
                                    <Grid item xs={12} sm={3}>
                                        <Typography component="span" className={classes.label}>
                                            <RenderText 
                                                {...props} 
                                                value={props.sectionAccountInformation?.labelIsActivated} 
                                            />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12} sm={9}>
                                        {props.isLoading 
                                        ? <Skeleton variant="rect" width="100%" height="40px" />
                                        : <FormControlLabel
                                            control={<CustomSwitchStyle 
                                                name="checked" 
                                                checked={props.isUserActivated} 
                                                onChange={props.accountSwitchHandler} 
                                            />}
                                            label={props.sectionAccountInformation?.isActivatedText}
                                        />}
                                    </Grid>
                                </Grid>
                                <CustomDivider marginTop={5} marginBottom={2} />
                                <Grid className={classes.button_container_update}>
                                    <Box my={2}>
                                        {props.isLoading 
                                        ? <Skeleton variant="rect" width="150px" height="40px" /> 
                                        : <UpdateAccountButton {...props} />}
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
