import * as React from "react";
import Box from "@material-ui/core/Box";
import Button from "@material-ui/core/Button";
import CircularProgress from "@material-ui/core/CircularProgress";
import Container from "@material-ui/core/Container";
import Typography from "@material-ui/core/Typography";
import { Card, CardContent } from "@material-ui/core";
import useStyles from "./styledUnsubscribe";
import AlertDialog from "../../Shared/Modals/alertDialog";
import * as Consts from "../../Shared/constants";
import axios from "axios";

interface IUnsubscribe
{
    uid: string | null;    
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

    const [content, setContent] = React.useState(
    { 
        caption: contentPre.caption,
        text1:   contentPre.text1, 
        text2:   contentPre.text2, 
        text3:   contentPre.text3, 
        button:  contentPre.button
    });
    const [ButtonState, setButtonState] = React.useState(true);
    const [Progress, setProgress] = React.useState(false);
    const [Modal, setModal] = React.useState({ State: false, Titile:  "", Message: "", Icon: 0 });
    const ButtonHandler = () =>
    {

        if (props.uid == null)
        {
            return false;
        }

        setButtonState(false);
        setProgress(true);

        axios.delete(Consts.API_DELETE_SUBSCRIBER.replace("{id}", props.uid))
        .then(function (response) 
        {

            if (response.status === 200 && response.data.isSucceeded) 
            {
                
                setContent(
                {
                    caption: contentPost.caption,
                    text1:   contentPost.text1, 
                    text2:   contentPost.text2, 
                    text3:   contentPost.text3, 
                    button:  contentPost.button
                });
            }
            else
            {
                setButtonState(true);
                setModal(
                { 
                    ...Modal, 
                    State: true, 
                    Titile: "Unsubscribe", 
                    Message: Consts.SUBSCRIBER_DEL_ERROR.replace("{ERROR}", response.data.error.errorDesc), 
                    Icon: 2 
                });
            }

        })
        .catch(function (error) 
        {
            console.error(error);
            setButtonState(true);
            setModal(
            { 
                ...Modal, 
                State: true, 
                Titile: "Error", 
                Message: Consts.SUBSCRIBER_DEL_ERROR.replace("{ERROR}", error), 
                Icon: 2 
            });
        })
        .then(function () 
        {
            setProgress(false);
        });            

        return true;

    }

    const ModalHandler = () => { setModal({ ...Modal, State: false }); }

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
                                    {content.caption}
                                </Typography>
                            </Box>
                            <Box mt={5} mb={2}>
                                <Typography variant="h6" component="h6" color="textSecondary">
                                    {content.text1}
                                </Typography>
                            </Box>
                            <Box mt={5} mb={2}>
                                <Typography variant="body1" color="textSecondary">
                                    {content.text2}
                                </Typography>
                            </Box>
                            <Box mt={5} mb={7}>
                                <Typography variant="body1" color="textSecondary">
                                    {content.text3}
                                </Typography>
                            </Box>
                            <Button onClick={ButtonHandler} type="submit" fullWidth variant="contained" color="primary" disabled={!ButtonState}>
                                {Progress &&  <CircularProgress size={20} />}
                                {!Progress && content.button}
                            </Button>
                        </Box>
                        </CardContent>
                    </Card>
                </Box>
            </Container>
        </section>
    );

}
