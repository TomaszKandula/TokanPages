import * as React from "react";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import TextField from "@material-ui/core/TextField";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import Checkbox from "@material-ui/core/Checkbox";
import Button from "@material-ui/core/Button";
import { CircularProgress } from "@material-ui/core";
import axios from "axios";
import useStyles from "./styleContactForm";
import AlertDialog from "../../Shared/Modals/alertDialog";
import Validate from "validate.js";
import * as Consts from "../../Shared/constants";
import { ConvertPropsToFields, HtmlRenderLines } from "../../Shared/helpers";
import { ValidateContactForm } from "../../Shared/validate";

export default function ContactForm()
{

    const classes = useStyles();
    const content = 
    {
        caption: "Contact me",
        text: "If you have any questions or you believe that I can do some work for you in technologies I currently work with, send me a message."
    };

    const [Form, setForm] = React.useState({ firstName: "", lastName: "", email: "", subject: "", message: "", terms: false });   
    const [Modal, setModal] = React.useState({ State: false, Titile:  "", Message: "", Icon: 0 });
    const [Progress, setProgress] = React.useState(false);

    const FormHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    {
        if (event.currentTarget.name !== "terms") { setForm({ ...Form, [event.currentTarget.name]: event.currentTarget.value}); }
            else { setForm({ ...Form, [event.currentTarget.name]: event.currentTarget.checked}); }
    }

    const ButtonHandler = () => 
    {
        
        let Results = ValidateContactForm( 
        { 
            FirstName: Form.firstName,
            LastName:  Form.lastName, 
            Email:     Form.email, 
            Subject:   Form.subject, 
            Message:   Form.message, 
            Terms:     Form.terms 
        });

        if (!Validate.isDefined(Results))
        {
            
            setProgress(true);

            axios.post(Consts.API_POST_MESSAGE, 
            {
                firstName: Form.firstName,
                lastName:  Form.lastName,
                userEmail: Form.email,
                emailFrom: "",
                emailTos:  [""],
                subject:   Form.subject,
                message:   Form.message
            })
            .then(function (response) 
            {

                if (response.status === 200 && response.data.isSucceeded) 
                {
                    setModal(
                    { 
                        ...Modal, 
                        State: true, 
                        Titile: "Contact Form", 
                        Message: Consts.MESSAGE_OUT_SUCCESS, 
                        Icon: 0 
                    });                      
                }
                else
                {
                    setModal(
                    { 
                        ...Modal, 
                        State: true, 
                        Titile: "Contact Form", 
                        Message: Consts.MESSAGE_OUT_ERROR.replace("{ERROR}", response.data.error.errorDesc), 
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
                    Titile: "Error", 
                    Message: Consts.MESSAGE_OUT_ERROR.replace("{ERROR}", error), 
                    Icon: 2 
                });
            })
            .then(function () 
            {
                setProgress(false);
                setForm({ firstName: "", lastName: "", email: "", subject: "", message: "", terms: false });
            });  

            return true;

        }

        setModal(
        { 
            ...Modal, 
            State: true, 
            Titile: "Warning", 
            Message: Consts.MESSAGE_OUT_WARN.replace("{LIST}", HtmlRenderLines(ConvertPropsToFields(Results), "li")), 
            Icon: 1 
        });

        return false;
        
    }

    const ModalHandler = () => 
    {
        setModal({ ...Modal, State: false });
    }

    return (
        <section className={classes.section}>
            <Container maxWidth="lg">
                <AlertDialog State={Modal.State} Handle={ModalHandler} Title={Modal.Titile} Message={Modal.Message} Icon={Modal.Icon} />
                <Container maxWidth="sm">
                    <div data-aos="fade-up">
                        <Box pt={8} pb={10}>
                            <Box mb={6} textAlign="center">
                                <Typography variant="h4" component="h2" gutterBottom={true}>
                                    {content.caption}
                                </Typography>
                                <Typography variant="subtitle1" color="textSecondary" paragraph={true}>
                                    {content.text}
                                </Typography>
                            </Box>
                            <Box>
                                <Grid container spacing={2}>
                                    <Grid item xs={12} sm={6}>
                                        <TextField onChange={FormHandler} value={Form.firstName} variant="outlined" required fullWidth name="firstName" id="firstName" label="First name" autoComplete="fname" />
                                    </Grid>
                                    <Grid item xs={12} sm={6}>
                                        <TextField onChange={FormHandler} value={Form.lastName} variant="outlined" required fullWidth name="lastName" id="lastName" label="Last name" autoComplete="lname" />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <TextField onChange={FormHandler} value={Form.email} variant="outlined" required fullWidth name="email" id="email" label="Email address" autoComplete="email" />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <TextField onChange={FormHandler} value={Form.subject} variant="outlined" required fullWidth name="subject" id="subject" label="Subject" autoComplete="subject" />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <TextField onChange={FormHandler} value={Form.message} variant="outlined" required multiline rows={6} fullWidth autoComplete="message" name="message" id="message" label="Message" />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <FormControlLabel control={<Checkbox onChange={FormHandler} checked={Form.terms} name="terms" id="terms" color="primary" />} label="I agree to the terms of use and privacy policy." />
                                    </Grid>
                                </Grid>
                                <Box my={2}>
                                    <Button onClick={ButtonHandler} type="submit" fullWidth variant="contained" color="primary" disabled={Progress}>
                                        {Progress &&  <CircularProgress size={20} />}
                                        {!Progress && "Submit"}
                                    </Button>
                                </Box>
                            </Box>
                        </Box>
                    </div>
                </Container>
            </Container>
        </section>
    );

}
