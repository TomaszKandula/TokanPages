import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import TextField from "@material-ui/core/TextField";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import Checkbox from "@material-ui/core/Checkbox";
import Button from "@material-ui/core/Button";
import { CircularProgress } from "@material-ui/core";
import Skeleton from "@material-ui/lab/Skeleton";
import Validate from "validate.js";
import { ActionCreators } from "../../Redux/Actions/sendMessageAction";
import { IApplicationState } from "../../Redux/applicationState";
import { OperationStatuses } from "../../Shared/Enums";
import { ValidateContactForm } from "../../Shared/validate";
import AlertDialog, { modalDefaultValues } from "../../Shared/Modals/alertDialog";
import { GetMessageOutSuccess, GetMessageOutWarning, GetMessageOutError } from "../../Shared/Modals/messageHelper";
import { IContactFormContentDto, ISendMessageDto } from "../../Api/Models";
import useStyles from "./styleContactForm";

const formDefaultValues =
{
    firstName: "", 
    lastName: "", 
    email: "", 
    subject: "", 
    message: "", 
    terms: false
};

export default function ContactForm(props: { contactForm: IContactFormContentDto, isLoading: boolean })
{
    const classes = useStyles();

    const [form, setForm] = React.useState(formDefaultValues);   
    const [modal, setModal] = React.useState(modalDefaultValues);
    const [progress, setProgress] = React.useState(false);

    const showSuccess = (text: string) => { setModal({ State: true, Title: "Contact Form", Message: text, Icon: 0 }); };
    const showWarning = (text: string) => { setModal({ State: true, Title: "Warning", Message: text, Icon: 1 }); };
    const showError = (text: string) => { setModal({ State: true, Title: "Error", Message: text, Icon: 2 }); };

    const sendMessageState = useSelector((state: IApplicationState) => state.sendMessage);
    const dispatch = useDispatch();

    const sendMessage = React.useCallback((payload: ISendMessageDto) => 
        { dispatch(ActionCreators.sendMessage(payload)); }, [ dispatch ]);

    const sendMessageClear = React.useCallback(() => 
        { dispatch(ActionCreators.sendMessageClear()); }, [ dispatch ]);
        
    React.useEffect(() => 
    { 
        if (sendMessageState === undefined) return;

        if (sendMessageState.isSendingMessage === OperationStatuses.notStarted && progress)
        {
            sendMessage(
            {
                firstName: form.firstName,
                lastName: form.lastName,
                userEmail: form.email,
                emailFrom: form.email,
                emailTos: [form.email],
                subject: form.subject,
                message: form.message
            });
            return;
        }

        if (sendMessageState.isSendingMessage === OperationStatuses.hasFinished)
        {
            setProgress(false);
            setForm(formDefaultValues);

            if (sendMessageState.hasSentMessage)
            {
                showSuccess(GetMessageOutSuccess());
                sendMessageClear();
                return;
            }

            showError(GetMessageOutError("Cannot send message"));//TODO: impl. proper error message
            sendMessageClear();
        }
    }, 
    [ sendMessage, sendMessageClear, sendMessageState, progress, form ]);

    const modalHandler = () => 
        { setModal({ ...modal, State: false}); };

    const formHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    {
        if (event.currentTarget.name !== "terms") 
            { setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value}); }
                else { setForm({ ...form, [event.currentTarget.name]: event.currentTarget.checked}); }
    };

    const buttonHandler = async () => 
    {
        let validationResult = ValidateContactForm( 
        { 
            FirstName: form.firstName,
            LastName: form.lastName, 
            Email: form.email, 
            Subject: form.subject, 
            Message: form.message, 
            Terms: form.terms 
        });

        if (!Validate.isDefined(validationResult))
        {
            setProgress(true);
            return;
        }

        showWarning(GetMessageOutWarning(validationResult));
    };

    return (
        <section className={classes.section}>
            <Container maxWidth="lg">
                <AlertDialog State={modal.State} Handle={modalHandler} Title={modal.Title} Message={modal.Message} Icon={modal.Icon} />
                <Container maxWidth="sm">
                    <div data-aos="fade-up">
                        <Box pt={8} pb={10}>
                            <Box mb={6} textAlign="center">
                                <Typography variant="h4" component="h2" gutterBottom={true}>
                                    {props.isLoading ? <Skeleton variant="text" /> : props.contactForm.content.caption}
                                </Typography>
                                <Typography variant="subtitle1" color="textSecondary" paragraph={true}>
                                    {props.isLoading ? <Skeleton variant="text" /> : props.contactForm.content.text}
                                </Typography>
                            </Box>
                            <Box>
                                <Grid container spacing={2}>
                                    <Grid item xs={12} sm={6}>
                                        <TextField 
                                            onChange={formHandler} value={form.firstName} variant="outlined" 
                                            required fullWidth name="firstName" id="firstName" label="First name" autoComplete="fname" 
                                        />
                                    </Grid>
                                    <Grid item xs={12} sm={6}>
                                        <TextField 
                                            onChange={formHandler} value={form.lastName} variant="outlined" 
                                            required fullWidth name="lastName" id="lastName" label="Last name" autoComplete="lname" 
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <TextField 
                                            onChange={formHandler} value={form.email} variant="outlined" 
                                            required fullWidth name="email" id="email" label="Email address" autoComplete="email" 
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <TextField 
                                            onChange={formHandler} value={form.subject} variant="outlined" 
                                            required fullWidth name="subject" id="subject" label="Subject" autoComplete="subject" 
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <TextField 
                                            onChange={formHandler} value={form.message} variant="outlined" 
                                            required multiline rows={6} fullWidth autoComplete="message" name="message" id="message" label="Message" 
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <FormControlLabel 
                                            control={<Checkbox onChange={formHandler} checked={form.terms} name="terms" id="terms" color="primary" />} 
                                            label="I agree to the terms of use and privacy policy." 
                                        />
                                    </Grid>
                                </Grid>
                                <Box my={2}>
                                    <Button onClick={buttonHandler} type="submit" fullWidth variant="contained" color="primary" disabled={progress}>
                                        {progress &&  <CircularProgress size={20} />}
                                        {!progress && props.contactForm.content.button}
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
