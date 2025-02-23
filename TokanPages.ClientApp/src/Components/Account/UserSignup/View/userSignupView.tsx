import * as React from "react";
import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import { Card, CardContent, CircularProgress, Checkbox } from "@material-ui/core";
import TextField from "@material-ui/core/TextField";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import { AccountCircle } from "@material-ui/icons";
import { Alert } from "@material-ui/lab";
import Skeleton from "@material-ui/lab/Skeleton";
import { LinkDto, WarningPasswordDto } from "../../../../Api/Models";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../Shared/types";
import { RedirectTo, TextFiedWithPassword } from "../../../../Shared/Components";
import { UserSignupProps } from "../userSignup";

interface UserSignupViewProps extends ViewProperties, UserSignupProps {
    caption: string;
    warning: WarningPasswordDto;
    consent: string;
    button: string;
    link: LinkDto;
    buttonHandler: () => void;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    progress: boolean;
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    terms?: boolean;
    labelFirstName: string;
    labelLastName: string;
    labelEmail: string;
    labelPassword: string;
}

const ActiveButton = (props: UserSignupViewProps): React.ReactElement => {
    return (
        <Button
            fullWidth
            type="submit"
            variant="contained"
            onClick={props.buttonHandler}
            className="button"
            disabled={props.progress}
        >
            {!props.progress ? props.button : <CircularProgress size={20} />}
        </Button>
    );
};

export const UserSignupView = (props: UserSignupViewProps): React.ReactElement => {
    return (
        <section className={`section ${props.background ?? ""}`}>
            <Container className="container">
                <div className={!props.className ? "pt-32 pb-80" : props.className}>
                    <Card elevation={0} className="card">
                        <CardContent className="card-content">
                            <div className="text-centre mb-25">
                                <AccountCircle className="account" />
                                <Typography className="caption">
                                    {props.isLoading ? <Skeleton variant="text" /> : props.caption}
                                </Typography>
                            </div>
                            <Grid container spacing={2}>
                                <Grid item xs={12} sm={6}>
                                    {props.isLoading ? (
                                        <Skeleton variant="rect" width="100%" height="45px" />
                                    ) : (
                                        <TextField
                                            required
                                            fullWidth
                                            id="firstName"
                                            name="firstName"
                                            variant="outlined"
                                            autoComplete="one-time-code"
                                            autoFocus={true}
                                            onKeyUp={props.keyHandler}
                                            onChange={props.formHandler}
                                            value={props.firstName}
                                            label={props.labelFirstName}
                                            disabled={props.progress}
                                        />
                                    )}
                                </Grid>
                                <Grid item xs={12} sm={6}>
                                    {props.isLoading ? (
                                        <Skeleton variant="rect" width="100%" height="40px" />
                                    ) : (
                                        <TextField
                                            required
                                            fullWidth
                                            id="lastName"
                                            name="lastName"
                                            variant="outlined"
                                            autoComplete="one-time-code"
                                            onKeyUp={props.keyHandler}
                                            onChange={props.formHandler}
                                            value={props.lastName}
                                            label={props.labelLastName}
                                            disabled={props.progress}
                                        />
                                    )}
                                </Grid>
                                <Grid item xs={12}>
                                    {props.isLoading ? (
                                        <Skeleton variant="rect" width="100%" height="45px" />
                                    ) : (
                                        <TextField
                                            required
                                            fullWidth
                                            id="email"
                                            name="email"
                                            variant="outlined"
                                            autoComplete="one-time-code"
                                            onKeyUp={props.keyHandler}
                                            onChange={props.formHandler}
                                            value={props.email}
                                            label={props.labelEmail}
                                            disabled={props.progress}
                                        />
                                    )}
                                </Grid>
                                <Grid item xs={12}>
                                    {props.isLoading ? (
                                        <Skeleton variant="rect" width="100%" height="45px" />
                                    ) : (
                                        <TextFiedWithPassword
                                            uuid="password"
                                            fullWidth={true}
                                            value={props.password}
                                            label={props.labelPassword}
                                            onKeyUp={props.keyHandler}
                                            onChange={props.formHandler}
                                            isDisabled={props.progress}
                                        />
                                    )}
                                </Grid>
                                <Grid item xs={12}>
                                    {props.isLoading ? (
                                        <Skeleton variant="rect" width="100%" height="45px" />
                                    ) : (
                                        <Alert severity="warning">
                                            <p className="signup-warning-text-pre">
                                                {props.warning?.textPre}
                                            </p>
                                            <ul className="signup-warning-text-list">
                                            {props.warning?.textList?.map((value, index) => (
                                                <li key={index}>
                                                    {value}
                                                </li>
                                            ))}
                                            </ul>
                                            <p className="signup-warning-text-post">
                                                {props.warning?.textPost}
                                            </p>
                                            <a href={props.warning?.textNist?.href} target="_blank" rel="noopener nofollow">
                                                {props.warning?.textNist?.text}
                                            </a>
                                        </Alert>
                                    )}
                                </Grid>
                                <Grid item xs={12}>
                                    {props.isLoading ? (
                                        <Skeleton variant="rect" width="100%" height="30px" />
                                    ) : (
                                        <FormControlLabel
                                            control={
                                                <Checkbox
                                                    id="terms"
                                                    name="terms"
                                                    onChange={props.formHandler}
                                                    checked={props.terms}
                                                    disabled={props.progress}
                                                    className="violet-check-box"
                                                />
                                            }
                                            label={props.consent}
                                        />
                                    )}
                                </Grid>
                            </Grid>
                            <div className="mt-15 mb-15">
                                {props.isLoading ? (
                                    <Skeleton variant="rect" width="100%" height="40px" />
                                ) : (
                                    <ActiveButton {...props} />
                                )}
                            </div>
                            <div className="text-right">
                                {props.isLoading ? (
                                    <Skeleton variant="text" />
                                ) : (
                                    <RedirectTo path={props.link?.href} name={props.link?.text} />
                                )}
                            </div>
                        </CardContent>
                    </Card>
                </div>
            </Container>
        </section>
    );
};
