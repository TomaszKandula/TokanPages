import * as React from "react";
import Box from "@material-ui/core/Box";
import Button from "@material-ui/core/Button";
import CircularProgress from "@material-ui/core/CircularProgress";
import Container from "@material-ui/core/Container";
import Typography from "@material-ui/core/Typography";
import { Card, CardContent } from "@material-ui/core";
import Skeleton from "@material-ui/lab/Skeleton";
import { ContentDto } from "../../../Api/Models";
import { ViewProperties } from "../../../Shared/Abstractions";
import { ExtendedViewProps } from "../newsletterRemove";
import { NewsletterRemoveStyle } from "./newsletterRemoveStyle";

interface NewsletterRemoveViewProps extends ViewProperties, ExtendedViewProps {
    contentPre: ContentDto;
    contentPost: ContentDto;
    buttonHandler: () => void;
    buttonState: boolean;
    progress: boolean;
    isRemoved: boolean;
}

const ActiveButton = (props: NewsletterRemoveViewProps): React.ReactElement => {
    const classes = NewsletterRemoveStyle();
    const content: ContentDto = props.isRemoved ? props.contentPost : props.contentPre;
    return (
        <Button
            fullWidth
            type="submit"
            variant="contained"
            onClick={props.buttonHandler}
            className={classes.button}
            disabled={props.progress || !props.buttonState}
        >
            {!props.progress ? content.button : <CircularProgress size={20} />}
        </Button>
    );
};

export const NewsletterRemoveView = (props: NewsletterRemoveViewProps): React.ReactElement => {
    const classes = NewsletterRemoveStyle();
    const content: ContentDto = props.isRemoved ? props.contentPost : props.contentPre;
    return (
        <section className={classes.section} style={props.background}>
            <Container className={classes.container}>
                <Box pt={props.pt ?? 0} pb={props.pb ?? 15}>
                    <Card elevation={0} className={classes.card}>
                        <CardContent className={classes.card_content}>
                            <Box textAlign="center" mb={3}>
                                <Box mt={2} mb={2}>
                                    <Typography className={classes.caption}>
                                        {props.isLoading ? <Skeleton variant="text" /> : content.caption}
                                    </Typography>
                                </Box>
                                <Box mt={5} mb={2}>
                                    <Typography className={classes.text1}>
                                        {props.isLoading ? <Skeleton variant="text" /> : content.text1}
                                    </Typography>
                                </Box>
                                <Box mt={5} mb={2}>
                                    <Typography className={classes.text2}>
                                        {props.isLoading ? <Skeleton variant="text" /> : content.text2}
                                    </Typography>
                                </Box>
                                <Box mt={5} mb={7}>
                                    <Typography className={classes.text3}>
                                        {props.isLoading ? <Skeleton variant="text" /> : content.text3}
                                    </Typography>
                                </Box>
                                {props.isLoading ? <Skeleton variant="rect" /> : <ActiveButton {...props} />}
                            </Box>
                        </CardContent>
                    </Card>
                </Box>
            </Container>
        </section>
    );
};
