import * as React from "react";
import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import TextField from "@material-ui/core/TextField";
import { CircularProgress } from "@material-ui/core";
import Skeleton from "@material-ui/lab/Skeleton";
import { ViewProperties } from "../../../Shared/Abstractions";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../Shared/types";
import { NewsletterStyle } from "./newsletterStyle";

interface NewsletterViewProps extends ViewProperties {
    caption: string;
    text: string;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    email: string;
    buttonHandler: () => void;
    progress: boolean;
    buttonText: string;
    labelEmail: string;
    background?: React.CSSProperties;
}

const ActiveButton = (props: NewsletterViewProps): JSX.Element => {
    const classes = NewsletterStyle();
    return (
        <Button
            fullWidth
            type="submit"
            variant="contained"
            onClick={props.buttonHandler}
            className={classes.button}
            disabled={props.progress}
        >
            {!props.progress ? props.buttonText : <CircularProgress size={20} />}
        </Button>
    );
};

export const NewsletterView = (props: NewsletterViewProps): JSX.Element => {
    const classes = NewsletterStyle();
    return (
        <section className={classes.section} style={props.background}>
            <Container maxWidth="lg">
                <Box py={8} textAlign="center">
                    <Grid container spacing={2}>
                        <Grid item xs={12} md={5}>
                            <Typography className={classes.caption} data-aos="fade-down">
                                {props.isLoading ? <Skeleton variant="text" /> : props.caption}
                            </Typography>
                            <div data-aos="zoom-in">
                                <Typography className={classes.text}>
                                    {props.isLoading ? <Skeleton variant="text" /> : props.text}
                                </Typography>
                            </div>
                        </Grid>
                        <Grid item xs={12} md={7}>
                            <div data-aos="zoom-in">
                                <Box display="flex" height="100%">
                                    <Box my="auto" width="100%">
                                        <Grid container spacing={2}>
                                            <Grid item xs={12} sm={7}>
                                                {props.isLoading ? (
                                                    <Skeleton variant="rect" width="100%" height="45px" />
                                                ) : (
                                                    <TextField
                                                        required
                                                        fullWidth
                                                        id="email_newletter"
                                                        name="email"
                                                        variant="outlined"
                                                        size="small"
                                                        autoComplete="email"
                                                        onKeyUp={props.keyHandler}
                                                        onChange={props.formHandler}
                                                        value={props.email}
                                                        label={props.labelEmail}
                                                    />
                                                )}
                                            </Grid>
                                            <Grid item xs={12} sm={5}>
                                                {props.isLoading ? (
                                                    <Skeleton variant="rect" width="100%" height="40px" />
                                                ) : (
                                                    <ActiveButton {...props} />
                                                )}
                                            </Grid>
                                        </Grid>
                                    </Box>
                                </Box>
                            </div>
                        </Grid>
                    </Grid>
                </Box>
            </Container>
        </section>
    );
};
