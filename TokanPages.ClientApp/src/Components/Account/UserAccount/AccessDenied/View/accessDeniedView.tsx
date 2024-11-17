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

interface AccessDeniedViewProps extends AccessDeniedProps {
    isLoading: boolean;
    accessDeniedCaption: string;
    accessDeniedPrompt: string;
    homeButtonText: string;
}

const HomeButton = (props: AccessDeniedViewProps): React.ReactElement => {
    return (
        <Link to="/" className="link">
            <Button fullWidth variant="contained" className="button" disabled={props.isLoading}>
                {props.homeButtonText}
            </Button>
        </Link>
    );
};

const CustomDivider = (args: { marginTop: number; marginBottom: number }) => {
    return (
        <Box mt={args.marginTop} mb={args.marginBottom}>
            <Divider className="divider" />
        </Box>
    );
};

export const AccessDeniedView = (props: AccessDeniedViewProps): React.ReactElement => {
    return (
        <section className="section" style={props.background}>
            <Container className="container-wide">
                <Box pt={15} pb={8}>
                    <Card elevation={0} className="card">
                        <CardContent className="card-content">
                            <Box pt={0} pb={0}>
                                <Typography component="span" className="access-denied-caption">
                                    {props.isLoading ? <Skeleton variant="text" /> : props.accessDeniedCaption}
                                </Typography>
                            </Box>
                            <CustomDivider marginTop={2} marginBottom={1} />
                            <Box pt={3} pb={3}>
                                <Typography component="span" className="access-denied-prompt">
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
