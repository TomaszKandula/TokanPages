import * as React from "react";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import TextField from "@material-ui/core/TextField";
import Button from "@material-ui/core/Button";
import BusinessCenterIcon from "@material-ui/icons/BusinessCenter";
import { Card, CardContent, CircularProgress } from "@material-ui/core";
import Skeleton from "@material-ui/lab/Skeleton";
import { ViewProperties } from "../../../Shared/Abstractions";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../Shared/types";
import { BusinessFormStyle } from "./businessFormStyle";
import { BusinessFormProps } from "../businessForm";

interface BusinessFormViewProps extends ViewProperties, BusinessFormProps {
    caption: string;
    text: string;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    firstName: string;
    lastName: string;
    email: string;
    subject: string;
    message: string;
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

const ActiveButton = (props: BusinessFormViewProps): JSX.Element => {
    const classes = BusinessFormStyle();
    return (
        <Button
            fullWidth
            type="submit"
            variant="contained"
            onClick={props.buttonHandler}
            disabled={props.progress}
            className={classes.button}
        >
            {!props.progress ? props.buttonText : <CircularProgress size={20} />}
        </Button>
    );
};

//TODO: change view //import BusinessCenterIcon from '@material-ui/icons/BusinessCenter';
export const BusinessFormView = (props: BusinessFormViewProps): JSX.Element => {
    const classes = BusinessFormStyle();
    return (
        <section className={classes.section}>
            <Container className={classes.container}>
                <Box pt={15} pb={30}>
                    <Box textAlign="center" data-aos="fade-down">
                        <Typography gutterBottom={true} className={classes.caption}>
                            {props.hasCaption ? props.caption?.toUpperCase() : <></>}
                        </Typography>
                    </Box>
                    <Card elevation={0} className={classes.card}>
                        <CardContent className={classes.card_content}>
                            <Box mb={3} textAlign="center">
                                {props.hasIcon ? (
                                    <>
                                        <BusinessCenterIcon className={classes.icon} />
                                        <Typography className={classes.small_caption}>{props.caption}</Typography>
                                    </>
                                ) : (
                                    <></>
                                )}
                            </Box>
                            <Grid container spacing={2}>
                                <Grid item xs={12} sm={6}>
                                    <div data-aos="zoom-in">
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
                                    </div>
                                </Grid>
                                <Grid item xs={12} sm={6}>
                                    <div data-aos="zoom-in">
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
                                    </div>
                                </Grid>
                                <Grid item xs={12}>
                                    <div data-aos="zoom-in">
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
                                    </div>
                                </Grid>
                                <Grid item xs={12}>
                                    <div data-aos="zoom-in">
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
                                    </div>
                                </Grid>
                                <Grid item xs={12}>
                                    <div data-aos="zoom-in">
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
                                    </div>
                                </Grid>
                            </Grid>
                            <Box my={2} data-aos="fade-up">
                                {props.isLoading ? (
                                    <Skeleton variant="rect" width="100%" height="40px" />
                                ) : (
                                    <ActiveButton {...props} />
                                )}
                            </Box>
                        </CardContent>
                    </Card>
                </Box>
            </Container>
        </section>
    );
};
