import * as React from "react";
import Container from "@material-ui/core/Container";
import Typography from "@material-ui/core/Typography";
import { Card, CardContent } from "@material-ui/core";
import Skeleton from "@material-ui/lab/Skeleton";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { ReactHtmlParser } from "../../../../Shared/Services/Renderers";
import { ExtendedViewProps } from "../activateAccount";

interface ActivateAccountViewProps extends ViewProperties, ExtendedViewProps {
    caption: string;
    text1: string;
    text2: string;
    progress: boolean;
}

export const ActivateAccountView = (props: ActivateAccountViewProps): React.ReactElement => {
    return (
        <section className="section" style={props.background}>
            <Container className="container">
                <div style={{ paddingTop: props.pt ?? 0, paddingBottom: props.pb ?? 15 }}>
                    <Card elevation={0} className="card">
                        <CardContent className="card-content">
                            <div style={{ textAlign: "center", marginBottom: 24 }} >
                                <div style={{ marginTop: 16, marginBottom: 16 }}>
                                    <Typography component="div" className="aa-caption">
                                        {props.isLoading ? <Skeleton variant="text" /> : props.caption}
                                    </Typography>
                                </div>
                                <div style={{ marginTop: 40, marginBottom: 16 }}>
                                    <Typography component="span" className="aa-text1">
                                        {props.isLoading ? (
                                            <Skeleton variant="text" />
                                        ) : (
                                            <ReactHtmlParser html={props.text1} />
                                        )}
                                    </Typography>
                                </div>
                                <div style={{ marginTop: 16, marginBottom: 40 }}>
                                    <Typography component="span" className="aa-text2">
                                        {props.isLoading ? (
                                            <Skeleton variant="text" />
                                        ) : (
                                            <ReactHtmlParser html={props.text2} />
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
