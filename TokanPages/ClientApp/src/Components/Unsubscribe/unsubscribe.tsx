import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Box from "@material-ui/core/Box";
import Button from "@material-ui/core/Button";
import CircularProgress from "@material-ui/core/CircularProgress";
import Container from "@material-ui/core/Container";
import Typography from "@material-ui/core/Typography";
import { Card, CardContent } from "@material-ui/core";
import Skeleton from "@material-ui/lab/Skeleton";
import { IApplicationState } from "../../Redux/applicationState";
import { ActionCreators } from "../../Redux/Actions/removeSubscriberAction";
import { IconType } from "../../Shared/enums";
import AlertDialog, { alertModalDefault } from "../../Shared/Components/AlertDialog/alertDialog";
import { NewsletterSuccess } from "../../Shared/textWrappers";
import { IRemoveSubscriberDto, IUnsubscribeContentDto } from "../../Api/Models";
import useStyles from "./unsubscribeStyle";

export default function Unsubscribe(props: { id: string, unsubscribe: IUnsubscribeContentDto, isLoading: boolean })
{
    const contentPre = 
    { 
        caption: props.unsubscribe?.content.contentPre.caption,
        text1:   props.unsubscribe?.content.contentPre.text1, 
        text2:   props.unsubscribe?.content.contentPre.text2, 
        text3:   props.unsubscribe?.content.contentPre.text3, 
        button:  props.unsubscribe?.content.contentPre.button
    };

    const contentPost = 
    {
        caption: props.unsubscribe?.content.contentPost.caption,
        text1:   props.unsubscribe?.content.contentPost.text1, 
        text2:   props.unsubscribe?.content.contentPost.text2, 
        text3:   props.unsubscribe?.content.contentPost.text3, 
        button:  props.unsubscribe?.content.contentPost.button
    };

    const classes = useStyles();

    const [content, setContent] = React.useState(contentPre);
    const [buttonState, setButtonState] = React.useState(true);
    const [progress, setProgress] = React.useState(false);
    const [modal, setModal] = React.useState(alertModalDefault);

    const removeSubscriberState = useSelector((state: IApplicationState) => state).removeSubscriber;
    const dispatch = useDispatch();

    const removeSubscriber = React.useCallback((payload: IRemoveSubscriberDto) => 
    {
        dispatch(ActionCreators.removeSubscriber(payload));
    }, 
    [ dispatch ]);

    React.useEffect(() => 
    { 
        if (!removeSubscriberState.isRemovingSubscriber && progress) 
        {
            setProgress(false);
            setButtonState(true);
            setContent(contentPost);
            setModal(
            { 
                State: true, 
                Title: "Remove subscriber", 
                Message: NewsletterSuccess(), 
                Icon: IconType.info
            });
        }
        
        if (!removeSubscriberState.isRemovingSubscriber 
            && !removeSubscriberState.hasRemovedSubscriber && progress)
                removeSubscriber({ id: props.id });
    }, 
    [ removeSubscriber, removeSubscriberState, progress, props.id, contentPost ]);

    const modalHandler = () => 
    { 
        setModal(alertModalDefault); 
    }

    const buttonHandler = () =>
    {
        if (props.id == null)
            return;

        setProgress(true);
    };

    return (
        <section className={classes.section}>
            <Container maxWidth="sm">
                <AlertDialog State={modal.State} Handle={modalHandler} Title={modal.Title} Message={modal.Message} Icon={modal.Icon} />
                <Box py={15}>
                    <Card elevation={4}>
                        <CardContent className={classes.card}>
                        <Box textAlign="center" mb={3}>
                            <Box mt={2} mb={2}>
                                <Typography variant="h4" component="h4" gutterBottom={true}>
                                    {props.isLoading ? <Skeleton variant="text" /> : content.caption}
                                </Typography>
                            </Box>
                            <Box mt={5} mb={2}>
                                <Typography variant="h6" component="h6" color="textSecondary">
                                    {props.isLoading ? <Skeleton variant="text" /> : content.text1}
                                </Typography>
                            </Box>
                            <Box mt={5} mb={2}>
                                <Typography variant="body1" color="textSecondary">
                                    {props.isLoading ? <Skeleton variant="text" /> : content.text2}
                                </Typography>
                            </Box>
                            <Box mt={5} mb={7}>
                                <Typography variant="body1" color="textSecondary">
                                    {props.isLoading ? <Skeleton variant="text" /> : content.text3}
                                </Typography>
                            </Box>
                            <Button onClick={buttonHandler} type="submit" fullWidth variant="contained" color="primary" disabled={!buttonState}>
                                {progress &&  <CircularProgress size={20} />}
                                {!progress && content.button}
                            </Button>
                        </Box>
                        </CardContent>
                    </Card>
                </Box>
            </Container>
        </section>
    );
}
