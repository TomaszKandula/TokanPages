import * as React from "react";
import { AccountCircle } from "@material-ui/icons";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import TextField from "@material-ui/core/TextField";
import Button from "@material-ui/core/Button";
import CircularProgress from "@material-ui/core/CircularProgress";
import Skeleton from "@material-ui/lab/Skeleton";
import { ViewProperties } from "../../../Shared/Abstractions";
import { ReactChangeEvent } from "../../../Shared/types";
import { ExtendedViewProps } from "../newsletterUpdate";

interface NewsletterUpdateViewProps extends ViewProperties, ExtendedViewProps {
    caption: string;
    formHandler: (event: ReactChangeEvent) => void;
    email: string;
    buttonHandler: () => void;
    buttonState: boolean;
    progress: boolean;
    buttonText: string;
    labelEmail: string;
}

const ActiveButton = (props: NewsletterUpdateViewProps): React.ReactElement => {
    return (
        <Button
            fullWidth
            variant="contained"
            onClick={props.buttonHandler}
            className={props.buttonState ? "button" : ""}
            disabled={props.progress || !props.buttonState}
        >
            {!props.progress ? props.buttonText : <CircularProgress size={20} />}
        </Button>
    );
};

export const NewsletterUpdateView = (props: NewsletterUpdateViewProps): React.ReactElement => {
    return (
        <section className="section">
            <Container className="container">
                <Box pt={props.pt ?? 18} pb={props.pb ?? 10}>
                    <Card elevation={0} className="card">
                        <CardContent className="card-content">
                            <Box mb={3} textAlign="center">
                                <AccountCircle className="newsletter-update-account" />
                                <Typography className="newsletter-update-caption">
                                    {props.isLoading ? <Skeleton variant="text" /> : props.caption}
                                </Typography>
                            </Box>
                            <Box>
                                <Grid container spacing={2}>
                                    <Grid item xs={12}>
                                        <TextField
                                            required
                                            fullWidth
                                            id="email"
                                            name="email"
                                            variant="outlined"
                                            autoComplete="email"
                                            onChange={props.formHandler}
                                            value={props.email}
                                            label={props.labelEmail}
                                        />
                                    </Grid>
                                </Grid>
                                <Box my={2}>
                                    {props.isLoading ? <Skeleton variant="rect" /> : <ActiveButton {...props} />}
                                </Box>
                            </Box>
                        </CardContent>
                    </Card>
                </Box>
            </Container>
        </section>
    );
};
