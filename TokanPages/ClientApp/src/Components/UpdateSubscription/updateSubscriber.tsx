import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { AccountCircle } from "@material-ui/icons";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import TextField from "@material-ui/core/TextField";
import Button from "@material-ui/core/Button";
import CircularProgress from "@material-ui/core/CircularProgress";
import Skeleton from "@material-ui/lab/Skeleton";
import Validate from "validate.js";
import { IApplicationState } from "../../Redux/applicationState";
import { ActionCreators } from "../../Redux/Actions/updateSubscriberAction";
import { IGetUpdateSubscriberContent } from "../../Redux/States/getUpdateSubscriberContentState";
import AlertDialog, { alertModalDefault } from "../../Shared/Components/AlertDialog/alertDialog";
import { IconType } from "../../Shared/enums";
import { ValidateEmail } from "../../Shared/validate";
import { NewsletterSuccess, NewsletterWarning } from "../../Shared/textWrappers";
import { IUpdateSubscriberDto } from "../../Api/Models";
import updateSubscriptionStyle from "./updateSubscriptionStyle";

interface IGetUpdateSubscriberContentExtended extends IGetUpdateSubscriberContent
{
    id: string;
}

export default function UpdateSubscriber(props: IGetUpdateSubscriberContentExtended)
{
    const classes = updateSubscriptionStyle();

    const [form, setForm] = React.useState({email: ""});
    const [buttonState, setButtonState] = React.useState(true);
    const [progress, setProgress] = React.useState(false);
    const [modal, setModal] = React.useState(alertModalDefault);

    const updateSubscriberState = useSelector((state: IApplicationState) => state.updateSubscriber);
    const dispatch = useDispatch();

    const updateSubscriber = React.useCallback((payload: IUpdateSubscriberDto) => 
    {
        dispatch(ActionCreators.updateSubscriber(payload));
    }, 
    [ dispatch ]);

    React.useEffect(() => 
    { 
        if (!updateSubscriberState.isUpdatingSubscriber 
            && updateSubscriberState.hasUpdatedSubscriber && progress) 
        {
            setProgress(false);
            setButtonState(true);
            setForm({email: ""});
            setModal(
            { 
                State: true, 
                Title: "Update subscriber", 
                Message: NewsletterSuccess(), 
                Icon: IconType.info 
            });
        }
        
        if (!updateSubscriberState.isUpdatingSubscriber 
                && !updateSubscriberState.hasUpdatedSubscriber && progress)
                    updateSubscriber({ id: props.id, email: form.email, isActivated: true, count: 0 });
    }, 
    [ updateSubscriber, updateSubscriberState, progress, form, props.id ]);

    const formHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    {
        setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value });
    };

    const modalHandler = () => 
    { 
        setModal(alertModalDefault); 
    };

    const buttonHandler = () =>
    {
        if (props.id == null) 
            return;

        let validationResult = ValidateEmail(form.email);

        if (!Validate.isDefined(validationResult))
        {
            setButtonState(false);
            setProgress(true);
            return;
        }

        setModal(
        { 
            State: true, 
            Title: "Warning", 
            Message: NewsletterWarning(validationResult), 
            Icon: IconType.warning
        });
    };

    return (
        <section>
            <Container maxWidth="sm">
                <AlertDialog State={modal.State} Handle={modalHandler} Title={modal.Title} Message={modal.Message} Icon={modal.Icon} />
                <Box pt={18} pb={10}>
                    <Card elevation={4}>
                        <CardContent className={classes.card}>
                            <Box mb={3} textAlign="center">
                                <AccountCircle color="primary" style={{ fontSize: 72 }} />
                                <Typography variant="h5" component="h2" color="textSecondary">
                                    {props.isLoading ? <Skeleton variant="text" /> : props.content?.caption}
                                </Typography>
                            </Box>
                            <Box>
                                <Grid container spacing={2}>
                                    <Grid item xs={12}>
                                        <TextField 
                                            onChange={formHandler} value={form.email} variant="outlined" required fullWidth 
                                            name="email" id="email" label="Email address" autoComplete="email" 
                                        />
                                    </Grid>
                                </Grid>
                                <Box my={2}>
                                    <Button onClick={buttonHandler} fullWidth variant="contained" color="primary" disabled={!buttonState}>
                                        {progress &&  <CircularProgress size={20} />}
                                        {!progress && props.content?.button}
                                    </Button>
                                </Box>
                            </Box>
                        </CardContent>
                    </Card>
                </Box>
            </Container>
        </section>
    );
}
