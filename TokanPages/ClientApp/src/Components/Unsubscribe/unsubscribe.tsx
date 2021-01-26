import * as React from "react";
import Box from "@material-ui/core/Box";
import Button from "@material-ui/core/Button";
import CircularProgress from "@material-ui/core/CircularProgress";
import Container from "@material-ui/core/Container";
import Typography from "@material-ui/core/Typography";
import { Card, CardContent } from "@material-ui/core";
import useStyles from "./styledUnsubscribe";
import AlertDialog, { modalDefaultValues } from "../../Shared/Modals/alertDialog";
import { RemoveSubscriberData } from "Api/Services/subscribers";

export interface IUnsubscribe
{
    id: string | null;    
}

export default function Unsubscribe(props: IUnsubscribe)
{
    const classes = useStyles();
    const contentPre = 
    {
        caption: "Cancel your subscribtion",
        text1: "We are sorry to see you go...", 
        text2: "...but we understand there are circumstances when you need to stop your service. If for any reason you were unhappy with our service, please let us know what we can fo to improve it.",
        text3: "Please contact us should you have any questions or to let us know how we might improve our service.",
        button: "Unsubscribe Now"
    };
    const contentPost = 
    {
        caption: "Subscribtion cancelled",
        text1: "We are sorry to see you go...", 
        text2: "...your email address has been remove from our list. You will no longer receive any emails from us. ",
        text3: "Please contact us should you have any questions or to let us know how we might improve our service.",
        button: "Unsubscribe Now"
    };

    const [Content, setContent] = React.useState(
    { 
        caption: contentPre.caption,
        text1:   contentPre.text1, 
        text2:   contentPre.text2, 
        text3:   contentPre.text3, 
        button:  contentPre.button
    });
    const [ButtonState, setButtonState] = React.useState(true);
    const [Progress, setProgress] = React.useState(false);
    const [Modal, setModal] = React.useState(modalDefaultValues);

    const ModalHandler = () => 
    { 
        setModal(modalDefaultValues); 
    }

    const ButtonHandler = async () =>
    {
        if (props.id == null)
        {
            return false;
        }

        setButtonState(false);
        setProgress(true);

        setModal(await RemoveSubscriberData(
        { 
            id: props.id 
        }));

        setContent(
        {
            caption: contentPost.caption,
            text1:   contentPost.text1, 
            text2:   contentPost.text2, 
            text3:   contentPost.text3, 
            button:  contentPost.button
        });

        setProgress(false);
        return true;
    };

    return (
        <section className={classes.section}>
            <Container maxWidth="sm">
                <AlertDialog State={Modal.State} Handle={ModalHandler} Title={Modal.Titile} Message={Modal.Message} Icon={Modal.Icon} />
                <Box py={15}>
                    <Card elevation={4}>
                        <CardContent className={classes.card}>
                        <Box textAlign="center" mb={3}>
                            <Box mt={2} mb={2}>
                                <Typography variant="h4" component="h4" gutterBottom={true}>
                                    {Content.caption}
                                </Typography>
                            </Box>
                            <Box mt={5} mb={2}>
                                <Typography variant="h6" component="h6" color="textSecondary">
                                    {Content.text1}
                                </Typography>
                            </Box>
                            <Box mt={5} mb={2}>
                                <Typography variant="body1" color="textSecondary">
                                    {Content.text2}
                                </Typography>
                            </Box>
                            <Box mt={5} mb={7}>
                                <Typography variant="body1" color="textSecondary">
                                    {Content.text3}
                                </Typography>
                            </Box>
                            <Button onClick={ButtonHandler} type="submit" fullWidth variant="contained" color="primary" disabled={!ButtonState}>
                                {Progress &&  <CircularProgress size={20} />}
                                {!Progress && Content.button}
                            </Button>
                        </Box>
                        </CardContent>
                    </Card>
                </Box>
            </Container>
        </section>
    );
}
