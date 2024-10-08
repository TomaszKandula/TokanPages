import * as React from "react";
import { Skeleton } from "@material-ui/lab";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import CardActions from "@material-ui/core/CardActions";
import { ViewProperties } from "../../../Shared/Abstractions";
import { CookiesStyle } from "./cookiesStyle";

interface Properties extends ViewProperties {
    modalClose: boolean;
    shouldShow: boolean;
    caption: string;
    text: string;
    onClickEvent: () => void;
    buttonText: string;
}

const ActiveButton = (props: Properties): React.ReactElement => {
    const classes = CookiesStyle();
    return (
        <Button onClick={props.onClickEvent} className={classes.button}>
            {props.buttonText}
        </Button>
    );
};

export const CookiesView = (props: Properties): React.ReactElement => {
    const classes = CookiesStyle();
    const renderConsent = (): React.ReactElement => {
        return (
            <Box
                position="fixed"
                width="100%"
                bottom={0}
                p={3}
                zIndex="modal"
                className={props.modalClose ? classes.close : classes.open}
            >
                <Container maxWidth="md">
                    <Card elevation={0} className={classes.container}>
                        <CardContent>
                            <Typography className={classes.caption}>
                                {props.isLoading ? <Skeleton variant="text" /> : props.caption}
                            </Typography>
                            <Typography className={classes.text}>
                                {props.isLoading ? <Skeleton variant="text" /> : props.text}
                            </Typography>
                        </CardContent>
                        <CardActions>
                            {props.isLoading ? <Skeleton variant="rect" /> : <ActiveButton {...props} />}
                        </CardActions>
                    </Card>
                </Container>
            </Box>
        );
    };

    return <>{props.shouldShow ? renderConsent() : null}</>;
};
