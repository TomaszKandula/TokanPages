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
import Validate from "validate.js";
import useStyles from "./styleContactForm";
import { ValidateContactForm } from "../../Shared/validate";
import AlertDialog, { modalDefaultValues } from "../../Shared/Modals/alertDialog";
import { GetMessageOutWarning } from "../../Shared/Modals/messageHelper";
import { SendMessage } from "../../Api/Services/mailer";

export default function ContactForm()
{

    const classes = useStyles();
    const formDefaultValues =
    {
        firstName: "", 
        lastName: "", 
        email: "", 
        subject: "", 
        message: "", 
        terms: false
    };
    const content = 
    {
        caption: "Contact me",
        text: "If you have any questions or you believe that I can do some work for you in technologies I currently work with, send me a message."
    };

    const [Form, setForm] = React.useState(formDefaultValues);   
    const [Modal, setModal] = React.useState(modalDefaultValues);
    const [Progress, setProgress] = React.useState(false);

    const ModalHandler = () => 
    {
        setModal(modalDefaultValues);
    }

    const FormHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    {
        if (event.currentTarget.name !== "terms") { setForm({ ...Form, [event.currentTarget.name]: event.currentTarget.value}); }
            else { setForm({ ...Form, [event.currentTarget.name]: event.currentTarget.checked}); }
    }

    const ButtonHandler = async () => 
    {
        let validationResult = ValidateContactForm( 
        { 
            FirstName: Form.firstName,
            LastName:  Form.lastName, 
            Email:     Form.email, 
            Subject:   Form.subject, 
            Message:   Form.message, 
            Terms:     Form.terms 
        });

        if (!Validate.isDefined(validationResult))
        {

            setProgress(true);
            setModal(await SendMessage(
            {
                firstName: Form.firstName,
                lastName: Form.lastName,
                userEmail: Form.email,
                emailFrom: Form.email,
                emailTos: [Form.email],
                subject: Form.subject,
                message: Form.message
            }));
            setProgress(false);
            setForm(formDefaultValues);
            return true;
        }

        setModal(
        { 
            ...Modal, 
            State: true, 
            Titile: "Warning", 
            Message: GetMessageOutWarning(validationResult), 
            Icon: 1 
        });

        return false;
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
