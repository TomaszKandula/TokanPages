import * as React from "react";
import Box from "@material-ui/core/Box";
import Button from "@material-ui/core/Button";
import CircularProgress from "@material-ui/core/CircularProgress";
import Container from "@material-ui/core/Container";
import Typography from "@material-ui/core/Typography";
import { Card, CardContent } from "@material-ui/core";
import Skeleton from "@material-ui/lab/Skeleton";
import { IContent } from "../../Api/Models/Components/unsubscribeContentDto";
import unsubscribeStyle from "./unsubscribeStyle";

interface IBinding 
{
    bind: IProperties;
}

interface IProperties
{
    isLoading: boolean;
    contentPre: IContent;
    contentPost: IContent;
    buttonHandler: any;
    buttonState: boolean;
    progress: boolean;
    isRemoved: boolean;
}

const UnsubscribeView = (props: IBinding): JSX.Element =>
{
    const classes = unsubscribeStyle();

    const content: IContent = props.bind?.isRemoved ? props.bind?.contentPost : props.bind?.contentPre;

    const ActiveButton = (): JSX.Element => 
    {
        return(
            <Button fullWidth onClick={props.bind?.buttonHandler} type="submit" variant="contained" 
                className={classes.button} disabled={props.bind?.progress || !props.bind?.buttonState}>
                {props.bind?.progress &&  <CircularProgress size={20} />}
                {!props.bind?.progress && content.button}
            </Button>
        );
    }

    return (
        <section className={classes.section}>
            <Container maxWidth="sm">
                <Box py={15}>
                    <Card elevation={4}>
                        <CardContent className={classes.card}>
                        <Box textAlign="center" mb={3}>
                            <Box mt={2} mb={2}>
                                <Typography gutterBottom={true} className={classes.caption}>
                                    {props.bind?.isLoading ? <Skeleton variant="text" /> : content.caption}
                                </Typography>
                            </Box>
                            <Box mt={5} mb={2}>
                                <Typography className={classes.text1}>
                                    {props.bind?.isLoading ? <Skeleton variant="text" /> : content.text1}
                                </Typography>
                            </Box>
                            <Box mt={5} mb={2}>
                                <Typography className={classes.text2}>
                                    {props.bind?.isLoading ? <Skeleton variant="text" /> : content.text2}
                                </Typography>
                            </Box>
                            <Box mt={5} mb={7}>
                                <Typography className={classes.text3}>
                                    {props.bind?.isLoading ? <Skeleton variant="text" /> : content.text3}
                                </Typography>
                            </Box>
                            {props.bind?.isLoading ? <Skeleton variant="rect" /> : <ActiveButton />}
                        </Box>
                        </CardContent>
                    </Card>
                </Box>
            </Container>
        </section>
    );
}

export default UnsubscribeView;
