import * as React from "react";
import Box from "@material-ui/core/Box";
import Container from "@material-ui/core/Container";
import Typography from "@material-ui/core/Typography";
import { Card, CardContent } from "@material-ui/core";
import Skeleton from "@material-ui/lab/Skeleton";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { ReactHtmlParser } from "../../../../Shared/Services/Renderers";
import { ActivateAccountStyle } from "./activateAccountStyle";

interface IProperties extends ViewProperties {
    caption: string;
    text1: string;
    text2: string;
    progress: boolean;
}

export const ActivateAccountView = (props: IProperties): JSX.Element => {
    const classes = ActivateAccountStyle();
    return (
        <section className={classes.section}>
            <Container maxWidth="sm">
                <Box py={15}>
                    <Card elevation={0} className={classes.card}>
                        <CardContent className={classes.card_content}>
                            <Box textAlign="center" mb={3}>
                                <Box mt={2} mb={2}>
                                    <Typography variant="h4" component="div" gutterBottom={true}>
                                        {props.isLoading ? <Skeleton variant="text" /> : props.caption}
                                    </Typography>
                                </Box>
                                <Box mt={5} mb={2}>
                                    <Typography variant="h6" component="div" color="textSecondary">
                                        {props.isLoading ? (
                                            <Skeleton variant="text" />
                                        ) : (
                                            <ReactHtmlParser html={props.text1} />
                                        )}
                                    </Typography>
                                </Box>
                                <Box mt={2} mb={5}>
                                    <Typography variant="body1" component="div" color="textSecondary">
                                        {props.isLoading ? (
                                            <Skeleton variant="text" />
                                        ) : (
                                            <ReactHtmlParser html={props.text2} />
                                        )}
                                    </Typography>
                                </Box>
                            </Box>
                        </CardContent>
                    </Card>
                </Box>
            </Container>
        </section>
    );
};
