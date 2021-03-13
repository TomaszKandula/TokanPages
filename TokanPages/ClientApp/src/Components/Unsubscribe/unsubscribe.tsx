import * as React from "react";
import Box from "@material-ui/core/Box";
import Button from "@material-ui/core/Button";
import CircularProgress from "@material-ui/core/CircularProgress";
import Container from "@material-ui/core/Container";
import Typography from "@material-ui/core/Typography";
import { Card, CardContent } from "@material-ui/core";
import AlertDialog, { modalDefaultValues } from "../../Shared/Modals/alertDialog";
import { RemoveSubscriberData } from "../../Api/Services/subscribers";
import { IUnsubscribe } from "../../Api/Models";
import useStyles from "./styledUnsubscribe";

export default function Unsubscribe(id: string | null, props: IUnsubscribe)
{
    const classes = useStyles();
    const [Content, setContent] = React.useState(
    { 
        caption: props.content.contentPre.caption,
        text1:   props.content.contentPre.text1, 
        text2:   props.content.contentPre.text2, 
        text3:   props.content.contentPre.text3, 
        button:  props.content.contentPre.button
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
        if (id == null)
        {
            return false;
        }

        setButtonState(false);
        setProgress(true);

        setModal(await RemoveSubscriberData(
        { 
            id: id 
        }));

        setContent(
        {
            caption: props.content.contentPost.caption,
            text1:   props.content.contentPost.text1, 
            text2:   props.content.contentPost.text2, 
            text3:   props.content.contentPost.text3, 
            button:  props.content.contentPost.button
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
