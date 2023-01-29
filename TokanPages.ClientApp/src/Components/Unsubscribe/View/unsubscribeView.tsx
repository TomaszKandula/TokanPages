import * as React from "react";
import Box from "@material-ui/core/Box";
import Button from "@material-ui/core/Button";
import CircularProgress from "@material-ui/core/CircularProgress";
import Container from "@material-ui/core/Container";
import Typography from "@material-ui/core/Typography";
import { Card, CardContent } from "@material-ui/core";
import Skeleton from "@material-ui/lab/Skeleton";
import { ContentDto } from "../../../Api/Models";
import { ViewProperties } from "../../../Shared/interfaces";
import { UnsubscribeStyle } from "./unsubscribeStyle";

interface Properties extends ViewProperties
{
    contentPre: ContentDto;
    contentPost: ContentDto;
    buttonHandler: any;
    buttonState: boolean;
    progress: boolean;
    isRemoved: boolean;
}

const ActiveButton = (props: Properties): JSX.Element => 
{
    const classes = UnsubscribeStyle();
    const content: ContentDto = props.isRemoved ? props.contentPost : props.contentPre;
    return(
        <Button 
            fullWidth 
            type="submit" 
            variant="contained" 
            onClick={props.buttonHandler} 
            className={classes.button} 
            disabled={props.progress || !props.buttonState}>
            {!props.progress 
            ? content.button 
            : <CircularProgress size={20} />}
        </Button>
    );
}

export const UnsubscribeView = (props: Properties): JSX.Element =>
{
    const classes = UnsubscribeStyle();
    const content: ContentDto = props.isRemoved ? props.contentPost : props.contentPre;
    return (
        <section className={classes.section}>
            <Container maxWidth="sm">
                <Box py={15}>
                    <Card elevation={4}>
                        <CardContent className={classes.card}>
                        <Box textAlign="center" mb={3}>
                            <Box mt={2} mb={2}>
                                <Typography className={classes.caption}>
                                    {props.isLoading 
                                    ? <Skeleton variant="text" /> 
                                    : content.caption}
                                </Typography>
                            </Box>
                            <Box mt={5} mb={2}>
                                <Typography className={classes.text1}>
                                    {props.isLoading 
                                    ? <Skeleton variant="text" /> 
                                    : content.text1}
                                </Typography>
                            </Box>
                            <Box mt={5} mb={2}>
                                <Typography className={classes.text2}>
                                    {props.isLoading 
                                    ? <Skeleton variant="text" /> 
                                    : content.text2}
                                </Typography>
                            </Box>
                            <Box mt={5} mb={7}>
                                <Typography className={classes.text3}>
                                    {props.isLoading 
                                    ? <Skeleton variant="text" /> 
                                    : content.text3}
                                </Typography>
                            </Box>
                            {props.isLoading 
                            ? <Skeleton variant="rect" /> 
                            : <ActiveButton {...props} />}
                        </Box>
                        </CardContent>
                    </Card>
                </Box>
            </Container>
        </section>
    );
}
