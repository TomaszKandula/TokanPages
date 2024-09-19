import * as React from "react";
import { Link } from "react-router-dom";
import Box from "@material-ui/core/Box";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import Container from "@material-ui/core/Container";
import Skeleton from "@material-ui/lab/Skeleton";
import { Button, Divider, Typography } from "@material-ui/core";
import { ReactHtmlParser } from "../../../../../Shared/Services/Renderers";
import { AccessDeniedProps } from "../accessDenied";
import { AccessDeniedStyle } from "./accessDeniedStyle";

interface AccessDeniedViewProps extends AccessDeniedProps {
    isLoading: boolean;
    accessDeniedCaption: string;
    accessDeniedPrompt: string;
    homeButtonText: string;
}

const HomeButton = (props: AccessDeniedViewProps): React.ReactElement => {
    const classes = AccessDeniedStyle();
    return (
        <Link to="/" className={classes.home_link}>
            <Button fullWidth variant="contained" className={classes.home_button} disabled={props.isLoading}>
                {props.homeButtonText}
            </Button>
        </Link>
    );
};

const CustomDivider = (args: { marginTop: number; marginBottom: number }) => {
    const classes = AccessDeniedStyle();
    return (
        <Box mt={args.marginTop} mb={args.marginBottom}>
            <Divider className={classes.divider} />
        </Box>
    );
};

export const AccessDeniedView = (props: AccessDeniedViewProps): React.ReactElement => {
    const classes = AccessDeniedStyle();
    return (
        <section className={classes.section} style={props.background}>
            <Container className={classes.container}>
                <Box pt={15} pb={8}>
                    <Card elevation={0} className={classes.card}>
                        <CardContent className={classes.card_content}>
                            <Box pt={0} pb={0}>
                                <Typography component="span" className={classes.caption}>
                                    {props.isLoading ? <Skeleton variant="text" /> : props.accessDeniedCaption}
                                </Typography>
                            </Box>
                            <CustomDivider marginTop={2} marginBottom={1} />
                            <Box pt={3} pb={3}>
                                <Typography component="span" className={classes.access_denied_prompt}>
                                    {props.isLoading ? (
                                        <Skeleton variant="text" height="100px" />
                                    ) : (
                                        <ReactHtmlParser html={props.accessDeniedPrompt} />
                                    )}
                                </Typography>
                            </Box>
                            {props.isLoading ? (
                                <Skeleton variant="rect" width="100%" height="40px" />
                            ) : (
                                <HomeButton {...props} />
                            )}
                        </CardContent>
                    </Card>
                </Box>
            </Container>
        </section>
    );
};
