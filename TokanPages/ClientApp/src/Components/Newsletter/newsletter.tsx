import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import TextField from "@material-ui/core/TextField";
import { CircularProgress } from "@material-ui/core";
import Skeleton from "@material-ui/lab/Skeleton";
import Validate from "validate.js";
import { IApplicationState } from "../../Redux/applicationState";
import { ActionCreators } from "../../Redux/Actions/addSubscriberAction";
import { AddSubscriberEnum } from "../../Redux/Enums/addSubscriberEnum";
import { ValidateEmail } from "../../Shared/validate";
import AlertDialog, { modalDefaultValues } from "../../Shared/Modals/alertDialog";
import { GetNewsletterSuccess, GetNewsletterWarning, GetNewsletterError } from "../../Shared/Modals/messageHelper";
import { IAddSubscriberDto, INewsletterContentDto } from "../../Api/Models";
import useStyles from "./styledNewsletter";

const formDefaultValues = 
{
    email: ""
};

export default function Newsletter(props: { newsletter: INewsletterContentDto, isLoading: boolean })
{
    const classes = useStyles();
    
    const [form, setForm] = React.useState(formDefaultValues);   
    const [modal, setModal] = React.useState(modalDefaultValues);
    const [progress, setProgress] = React.useState(false);

    const showSuccess = (text: string) => { setModal({ State: true, Title: "Newsletter", Message: text, Icon: 0 }); };
    const showWarning = (text: string) => { setModal({ State: true, Title: "Warning", Message: text, Icon: 1 }); };
    const showError = (text: string) => { setModal({ State: true, Title: "Error", Message: text, Icon: 2 }); };

    const addSubscriberState = useSelector((state: IApplicationState) => state.addSubscriber);
    const dispatch = useDispatch();

    const addSubscriber = React.useCallback((payload: IAddSubscriberDto) => 
        { dispatch(ActionCreators.addSubscriber(payload)); }, [ dispatch ]);

    const addSubscriberClear = React.useCallback(() => 
        { dispatch(ActionCreators.addSubscriberClear()); }, [ dispatch ]);

    React.useEffect(() => 
    { 
        if (addSubscriberState === undefined) 
            return;

        if (addSubscriberState.isAddingSubscriber === AddSubscriberEnum.notStarted && progress)
        {
            addSubscriber({ email: form.email });
            return;
        }
            
        if (addSubscriberState.isAddingSubscriber === AddSubscriberEnum.hasFinished)
        {
            setProgress(false);
            setForm(formDefaultValues);

            if (addSubscriberState.hasAddedSubscriber)
            {
                showSuccess(GetNewsletterSuccess());
                addSubscriberClear();
                return;
            }

            showError(GetNewsletterError("Cannot add subscription"));//TODO: impl. proper error message
            addSubscriberClear();
        }
    }, 
    [ addSubscriber, addSubscriberClear, addSubscriberState, progress, form ]);

    const modalHandler = () => 
        { setModal({ ...modal, State: false}); };

    const formHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
        { setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value }); };

    const buttonHandler = () =>
    {
        let results = ValidateEmail(form.email);

        if (!Validate.isDefined(results))
        {
            setProgress(true);
            return;
        }

        showWarning(GetNewsletterWarning(results));
    };

    return (
        <section className={classes.section}>
            <Container maxWidth="lg">
                <AlertDialog 
                    State={modal.State} 
                    Handle={modalHandler} 
                    Title={modal.Title} 
                    Message={modal.Message} 
                    Icon={modal.Icon} 
                />
                <div data-aos="fade-up">
                    <Box py={8} textAlign="center">
                        <Grid container spacing={2}>
                            <Grid item xs={12} md={5}>
                                <Typography variant="h4" component="h2">
                                    {props.isLoading ? <Skeleton variant="text" /> : props.newsletter.content.caption}
                                </Typography>
                                <Typography variant="subtitle1" color="textSecondary">
                                    {props.isLoading ? <Skeleton variant="text" /> : props.newsletter.content.text}
                                </Typography>
                            </Grid>
                            <Grid item xs={12} md={7}>
                                <Box display="flex" height="100%">
                                    <Box my="auto" width="100%">
                                        <Grid container spacing={2}>
                                            <Grid item xs={12} sm={7}>
                                                <TextField 
                                                    onChange={formHandler} value={form.email} variant="outlined" 
                                                    required fullWidth size="small" name="email" id="email_newletter" 
                                                    label="Email address" autoComplete="email" 
                                                />
                                            </Grid>
                                            <Grid item xs={12} sm={5}>
                                                <Button onClick={buttonHandler} type="submit" fullWidth variant="contained" color="primary" disabled={progress}>
                                                    {progress &&  <CircularProgress size={20} />}
                                                    {!progress && props.newsletter.content.button}
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
