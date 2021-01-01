import React from "react";
import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import TextField from "@material-ui/core/TextField";
import { CircularProgress } from "@material-ui/core";
import useStyles from "./styledNewsletter";
import Validate from "validate.js";
import AlertDialog from "Shared/Modals/alertDialog";
import { ValidateEmail } from "Shared/validate";
import { ConvertPropsToFields, HtmlRenderLines } from "Shared/helpers";
import axios from "axios";
import { API_COMMAND_ADD_SUBSCRIBER, NEWSLETTER_SUCCESS, NEWSLETTER_ERROR, NEWSLETTER_WARN } from "../../Shared/constants";

export default function Newsletter()
{

    const classes = useStyles();
    const content = 
    {
        caption: "Join the newsletter!",
        text: "We will never share your email address.",
        button: "Subscribe",
    };

    const [Form, setForm] = React.useState({ email: "" });   
    const [Modal, setModal] = React.useState({ State: false, Titile:  "", Message: "", Icon: 0 });
    const [Progress, setProgress] = React.useState(false);
    
    const FormHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    {
        setForm({ ...Form, [event.currentTarget.name]: event.currentTarget.value });
    }

    const ModalHandler = () => 
    {
        setModal({ ...Modal, State: false });
    }

    const ButtonHandler = () =>
    {

        let Results = ValidateEmail(Form.email);

        if (!Validate.isDefined(Results))
        {
            setProgress(true);

            axios.post(API_COMMAND_ADD_SUBSCRIBER, 
            {
                email: Form.email
            })
            .then(function (response) 
            {
                if (response.status === 200) 
                {
                    setModal(
                    { 
                        ...Modal, 
                        State: true, 
                        Titile: "Newsletter", 
                        Message: NEWSLETTER_SUCCESS, 
                        Icon: 0 
                    });
                }
            })
            .catch(function (error) 
            {
                console.error(error);
                setModal(
                { 
                    ...Modal, 
                    State: true, 
                    Titile: "Newsletter | Error", 
                    Message: NEWSLETTER_ERROR.replace("{ERROR}", error.response.data.ErrorMessage),
                    Icon: 2 
                });
            })
            .then(function () 
            {
                setProgress(false);
                setForm({ email: "" });
            });  

            return true;
        }

        setModal(
        { 
            ...Modal, 
            State: true, 
            Titile: "Warning", 
            Message: NEWSLETTER_WARN.replace("{LIST}", HtmlRenderLines(ConvertPropsToFields(Results), "li")), 
            Icon: 1 
        });

        return false;
    }

    return (
        <section className={classes.section}>
            <Container maxWidth="lg">
                <AlertDialog State={Modal.State} Handle={ModalHandler} Title={Modal.Titile} Message={Modal.Message} Icon={Modal.Icon} />
                <div data-aos="fade-up">
                    <Box py={8} textAlign="center">
                        <Grid container spacing={2}>
                            <Grid item xs={12} md={5}>
                                <Typography variant="h4" component="h2">
                                    {content.caption}
                                </Typography>
                                <Typography variant="subtitle1" color="textSecondary">
                                    {content.text}
                                </Typography>
                            </Grid>
                            <Grid item xs={12} md={7}>
                                <Box display="flex" height="100%">
                                    <Box my="auto" width="100%">              
                                        <Grid container spacing={2}>
                                            <Grid item xs={12} sm={7}>
                                                <TextField onChange={FormHandler} value={Form.email} variant="outlined" required fullWidth size="small" name="email" id="email_newletter" label="Email address" autoComplete="email" />
                                            </Grid>
                                            <Grid item xs={12} sm={5}>
                                                <Button onClick={ButtonHandler} type="submit" fullWidth variant="contained" color="primary" disabled={Progress}>
                                                    {Progress &&  <CircularProgress size={20} />}
                                                    {!Progress && content.button}
                                                </Button>
                                            </Grid>
                                        </Grid>
                                    </Box>
                                </Box>
                            </Grid>
                        </Grid>
                    </Box>
                </div>
            </Container>
        </section>
    );

}
