import * as React from "react";
import { Link } from "react-router-dom";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import Container from "@material-ui/core/Container";
import Skeleton from "@material-ui/lab/Skeleton";
import { Button, Typography } from "@material-ui/core";
import { ReactHtmlParser } from "../../../../../Shared/Services/Renderers";
import { CustomDivider } from "../../../../../Shared/Components";
import { AccessDeniedProps } from "../accessDenied";

interface AccessDeniedViewProps extends AccessDeniedProps {
    isLoading: boolean;
    languageId: string;
    accessDeniedCaption: string;
    accessDeniedPrompt: string;
    homeButtonText: string;
}

const HomeButton = (props: AccessDeniedViewProps): React.ReactElement => {
    return (
        <Link to={`/${props.languageId}`} className="link" rel="noopener nofollow">
            <Button fullWidth variant="contained" className="button" disabled={props.isLoading}>
                {props.homeButtonText}
            </Button>
        </Link>
    );
};

export const AccessDeniedView = (props: AccessDeniedViewProps): React.ReactElement => {
    return (
        <section className={`section ${props.background ?? ""}`}>
            <Container className="container-wide">
                <div className="pt-120 pb-64">
                    <Card elevation={0} className="card">
                        <CardContent className="card-content">
                            <Typography component="span" className="access-denied-caption">
                                {props.isLoading ? <Skeleton variant="text" /> : props.accessDeniedCaption}
                            </Typography>
                            <CustomDivider mt={2} mb={1} />
                            <div className="pt-25 pb-25">
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
