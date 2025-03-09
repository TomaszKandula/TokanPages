import * as React from "react";
import Container from "@material-ui/core/Container";
import Typography from "@material-ui/core/Typography";
import { Card, CardContent } from "@material-ui/core";
import Skeleton from "@material-ui/lab/Skeleton";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { ExtendedViewProps } from "../accountActivate";

interface AccountActivateViewProps extends ViewProperties, ExtendedViewProps {
    caption: string;
    text1: string;
    text2: string;
    progress: boolean;
}

export const AccountActivateView = (props: AccountActivateViewProps): React.ReactElement => {
    return (
        <section className={`section ${props.background ?? ""}`}>
            <Container className="container">
                <div className={!props.className ? "pt-0 pb-15" : props.className}>
                    <Card elevation={0} className="card">
                        <CardContent className="card-content">
                            <div className="text-centre mb-25">
                                <div className="mt-15 mb-15">
                                    <Typography component="div" className="aa-caption">
                                        {props.isLoading ? <Skeleton variant="text" /> : props.caption}
                                    </Typography>
                                </div>
                                <div className="mt-40 mb-15">
                                    <Typography component="span" className="aa-text1">
                                        {props.isLoading ? (
                                            <Skeleton variant="text" />
                                        ) : (
                                            props.text1
                                        )}
                                    </Typography>
                                </div>
                                <div className="mt-15 mb-40">
                                    <Typography component="span" className="aa-text2">
                                        {props.isLoading ? (
                                            <Skeleton variant="text" />
                                        ) : (
                                            props.text2
                                        )}
                                    </Typography>
                                </div>
                            </div>
                        </CardContent>
                    </Card>
                </div>
            </Container>
        </section>
    );
};
