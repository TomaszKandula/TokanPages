import * as React from "react";
import Button from "@material-ui/core/Button";
import CircularProgress from "@material-ui/core/CircularProgress";
import Container from "@material-ui/core/Container";
import Typography from "@material-ui/core/Typography";
import { Card, CardContent } from "@material-ui/core";
import Skeleton from "@material-ui/lab/Skeleton";
import { ContentDto } from "../../../Api/Models";
import { ViewProperties } from "../../../Shared/Abstractions";
import { ExtendedViewProps } from "../newsletterRemove";

interface NewsletterRemoveViewProps extends ViewProperties, ExtendedViewProps {
    contentPre: ContentDto;
    contentPost: ContentDto;
    buttonHandler: () => void;
    buttonState: boolean;
    progress: boolean;
    isRemoved: boolean;
}

const ActiveButton = (props: NewsletterRemoveViewProps): React.ReactElement => {
    const content: ContentDto = props.isRemoved ? props.contentPost : props.contentPre;
    return (
        <Button
            fullWidth
            type="submit"
            variant="contained"
            onClick={props.buttonHandler}
            className={props.buttonState ? "button" : ""}
            disabled={props.progress || !props.buttonState}
        >
            {!props.progress ? content.button : <CircularProgress size={20} />}
        </Button>
    );
};

export const NewsletterRemoveView = (props: NewsletterRemoveViewProps): React.ReactElement => {
    const content: ContentDto = props.isRemoved ? props.contentPost : props.contentPre;
    return (
        <section className="section" style={props.background}>
            <Container className="container">
                <div style={{ paddingTop: props.pt ?? 0, paddingBottom: props.pb ?? 120 }}>
                    <Card elevation={0} className="card">
                        <CardContent className="card-content">
                            <div style={{ textAlign: "center", marginBottom: 24 }}>
                                <div style={{ marginTop: 16, marginBottom: 16 }}>
                                    <Typography className="newsletter-remove-caption">
                                        {props.isLoading ? <Skeleton variant="text" /> : content.caption}
                                    </Typography>
                                </div>
                                <div style={{ marginTop: 40, marginBottom: 16 }}>
                                    <Typography className="newsletter-remove-text1">
                                        {props.isLoading ? <Skeleton variant="text" /> : content.text1}
                                    </Typography>
                                </div>
                                <div style={{ marginTop: 40, marginBottom: 16 }}>
                                    <Typography className="newsletter-remove-text2">
                                        {props.isLoading ? <Skeleton variant="text" /> : content.text2}
                                    </Typography>
                                </div>
                                <div style={{ marginTop: 40, marginBottom: 56 }}>
                                    <Typography className="newsletter-remove-text3">
                                        {props.isLoading ? <Skeleton variant="text" /> : content.text3}
                                    </Typography>
                                </div>
                                {props.isLoading ? <Skeleton variant="rect" /> : <ActiveButton {...props} />}
                            </div>
                        </CardContent>
                    </Card>
                </div>
            </Container>
        </section>
    );
};
