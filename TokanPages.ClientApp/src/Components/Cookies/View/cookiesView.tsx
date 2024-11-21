import * as React from "react";
import { Skeleton } from "@material-ui/lab";
import Container from "@material-ui/core/Container";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import CardActions from "@material-ui/core/CardActions";
import { ViewProperties } from "../../../Shared/Abstractions";

interface Properties extends ViewProperties {
    modalClose: boolean;
    shouldShow: boolean;
    caption: string;
    text: string;
    onClickEvent: () => void;
    buttonText: string;
}

const ActiveButton = (props: Properties): React.ReactElement => {
    return (
        <Button onClick={props.onClickEvent} className="button cookies-button">
            {props.buttonText}
        </Button>
    );
};

export const CookiesView = (props: Properties): React.ReactElement => {
    const style = props.modalClose ? "cookies-close" : "cookies-open";
    const renderConsent = (): React.ReactElement => {
        return (
            <div className={`cookies-box ${style}`}>
                <Container className="container-wide">
                    <Card elevation={0} className="cookies-card">
                        <CardContent>
                            <Typography className="cookies-caption">
                                {props.isLoading ? <Skeleton variant="text" /> : props.caption}
                            </Typography>
                            <Typography className="cookies-text">
                                {props.isLoading ? <Skeleton variant="text" /> : props.text}
                            </Typography>
                        </CardContent>
                        <CardActions>
                            {props.isLoading ? <Skeleton variant="rect" /> : <ActiveButton {...props} />}
                        </CardActions>
                    </Card>
                </Container>
            </div>
        );
    };

    return <>{props.shouldShow ? renderConsent() : null}</>;
};
