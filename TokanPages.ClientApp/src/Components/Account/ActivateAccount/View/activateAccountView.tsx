import * as React from "react";
import Box from "@material-ui/core/Box";
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
                <Box pt={props.pt ?? 0} pb={props.pb ?? 15}>
                    <Card elevation={0} className="card">
                        <CardContent className="card_content">
                            <Box textAlign="center" mb={3}>
                                <Box mt={2} mb={2}>
                                    <Typography component="div" className="aa-caption">
                                        {props.isLoading ? <Skeleton variant="text" /> : props.caption}
                                    </Typography>
                                </Box>
                                <Box mt={5} mb={2}>
                                    <Typography component="span" className="aa-text1">
                                        {props.isLoading ? (
                                            <Skeleton variant="text" />
                                        ) : (
                                            <ReactHtmlParser html={props.text1} />
                                        )}
                                    </Typography>
                                </Box>
                                <Box mt={2} mb={5}>
                                    <Typography component="span" className="aa-text2">
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
