import * as React from "react";
import Container from "@material-ui/core/Container";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import TextField from "@material-ui/core/TextField";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import Button from "@material-ui/core/Button";
import ContactMailIcon from "@material-ui/icons/ContactMail";
import { Card, CardContent, CircularProgress, Checkbox } from "@material-ui/core";
import Skeleton from "@material-ui/lab/Skeleton";
import { ViewProperties } from "../../../Shared/Abstractions";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../Shared/types";
import { Animated } from "../../../Shared/Components";
import { ContactFormProps } from "../contactForm";

interface ContactFormViewProps extends ViewProperties, ContactFormProps {
    caption: string;
    text: string;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    firstName: string;
    lastName: string;
    email: string;
    subject: string;
    message: string;
    terms?: boolean;
    buttonHandler: () => void;
    progress: boolean;
    buttonText: string;
    consent: string;
    labelFirstName: string;
    labelLastName: string;
    labelEmail: string;
    labelSubject: string;
    labelMessage: string;
    multiline?: boolean;
    minRows?: number;
}

const ActiveButton = (props: ContactFormViewProps): React.ReactElement => {
    return (
        <Button
            fullWidth
            type="submit"
            variant="contained"
            onClick={props.buttonHandler}
            disabled={props.progress}
            className="button"
        >
            {!props.progress ? props.buttonText : <CircularProgress size={20} />}
        </Button>
    );
};

export const ContactFormView = (props: ContactFormViewProps): React.ReactElement => {
    return (
        <section className={`section ${props.background ?? ""}`}>
            <Container className="container">
                <div className={!props.className ? "pt-64 pb-96" : props.className}>
                    <Animated dataAos="fade-down" className="text-centre">
                        <h1 className="contact-caption">
                            {props.hasCaption ? props.caption?.toUpperCase() : <></>}
                        </h1>
                    </Animated>
                    <Card elevation={0} className={props.hasShadow ? "card" : undefined}>
                        <CardContent className="card-content">
                            <div className="text-centre mb-25">
                                {props.hasIcon ? (
                                    <>
                                        <ContactMailIcon className="contact-icon" />
                                        <Typography className="contact-small-caption">{props.caption}</Typography>
                                    </>
                                ) : (
                                    <></>
                                )}
                            </div>
                            <Grid container spacing={2}>
                                <Grid item xs={12} sm={6}>
                                    <Animated dataAos="zoom-in">
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="45px" />
                                        ) : (
                                            <TextField
                                                required
                                                fullWidth
                                                id="firstName"
                                                name="firstName"
                                                autoComplete="fname"
                                                variant="outlined"
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                                value={props.firstName}
                                                label={props.labelFirstName}
                                            />
                                        )}
                                    </Animated>
                                </Grid>
                                <Grid item xs={12} sm={6}>
                                    <Animated dataAos="zoom-in">
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="45px" />
                                        ) : (
                                            <TextField
                                                required
                                                fullWidth
                                                id="lastName"
                                                name="lastName"
                                                autoComplete="lname"
                                                variant="outlined"
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                                value={props.lastName}
                                                label={props.labelLastName}
                                            />
                                        )}
                                    </Animated>
                                </Grid>
                                <Grid item xs={12}>
                                    <Animated dataAos="zoom-in">
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="45px" />
                                        ) : (
                                            <TextField
                                                required
                                                fullWidth
                                                id="email"
                                                name="email"
                                                autoComplete="email"
                                                variant="outlined"
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                                value={props.email}
                                                label={props.labelEmail}
                                            />
                                        )}
                                    </Animated>
                                </Grid>
                                <Grid item xs={12}>
                                    <Animated dataAos="zoom-in">
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="45px" />
                                        ) : (
                                            <TextField
                                                required
                                                fullWidth
                                                id="subject"
                                                name="subject"
                                                autoComplete="subject"
                                                variant="outlined"
                                                onKeyUp={props.keyHandler}
                                                onChange={props.formHandler}
                                                value={props.subject}
                                                label={props.labelSubject}
                                            />
                                        )}
                                    </Animated>
                                </Grid>
                                <Grid item xs={12}>
                                    <Animated dataAos="zoom-in">
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="45px" />
                                        ) : (
                                            <TextField
                                                required
                                                fullWidth
                                                multiline={props.multiline}
                                                minRows={props.minRows}
                                                id="message"
                                                name="message"
                                                autoComplete="message"
                                                variant="outlined"
                                                onChange={props.formHandler}
                                                value={props.message}
                                                label={props.labelMessage}
                                            />
                                        )}
                                    </Animated>
                                </Grid>
                                <Grid item xs={12}>
                                    <Animated dataAos="zoom-in">
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="100%" height="30px" />
                                        ) : (
                                            <FormControlLabel
                                                control={
                                                    <Checkbox
                                                        disabled={props.progress}
                                                        onChange={props.formHandler}
                                                        checked={props.terms}
                                                        name="terms"
                                                        id="terms"
                                                        className="violet-check-box"
                                                    />
                                                }
                                                label={props.consent}
                                            />
                                        )}
                                    </Animated>
                                </Grid>
                            </Grid>
                            <Animated dataAos="fade-up" className="mt-15 mb-15">
                                {props.isLoading ? (
                                    <Skeleton variant="rect" width="100%" height="40px" />
                                ) : (
                                    <ActiveButton {...props} />
                                )}
                            </Animated>
                        </CardContent>
                    </Card>
                </div>
            </Container>
        </section>
    );
};
