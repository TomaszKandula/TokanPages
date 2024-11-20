import * as React from "react";
import { Link } from "react-router-dom";
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

interface CustomDividerProps {
    marginTop: number; 
    marginBottom: number;
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

const CustomDivider = (props: CustomDividerProps) => {
    return (
        <div style={{ marginTop: props.marginTop, marginBottom: props.marginBottom }}>
            <Divider className="divider" />
        </div>
    );
};

export const AccessDeniedView = (props: AccessDeniedViewProps): React.ReactElement => {
    return (
        <section className="section" style={props.background}>
            <Container className="container-wide">
                <div style={{ paddingTop: 120, paddingBottom: 64 }}>
                    <Card elevation={0} className="card">
                        <CardContent className="card-content">
                            <div style={{ paddingTop: 0, paddingBottom: 0 }}>
                                <Typography component="span" className="access-denied-caption">
                                    {props.isLoading ? <Skeleton variant="text" /> : props.accessDeniedCaption}
                                </Typography>
                            </div>
                            <CustomDivider marginTop={2} marginBottom={1} />
                            <div style={{ paddingTop: 24, paddingBottom: 24 }}>
                                <Typography component="span" className="access-denied-prompt">
                                    {props.isLoading ? (
                                        <Skeleton variant="text" height="100px" />
                                    ) : (
                                        <ReactHtmlParser html={props.accessDeniedPrompt} />
                                    )}
                                </Typography>
                            </div>
                            {props.isLoading ? (
                                <Skeleton variant="rect" width="100%" height="40px" />
                            ) : (
                                <HomeButton {...props} />
                            )}
                        </CardContent>
                    </Card>
                </div>
            </Container>
        </section>
    );
};
