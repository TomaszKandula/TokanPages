import * as React from "react";
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
import Validate from "validate.js";
import AlertDialog, { modalDefaultValues } from "../../Shared/Modals/alertDialog";
import useStyles from "./styleUpdateSubscription";
import { UpdateSubscriberData } from "../../Api/Services/subscribers";
import { ValidateEmail } from "../../Shared/validate";
import { GetNewsletterWarning } from "../../Shared/Modals/messageHelper";

export interface IUpdateSubscription
{
    id: string | null;
}

export default function UpdateSubscriber(props: IUpdateSubscription)
{
    const classes = useStyles();
    const content = 
    {
        caption: "Update subscription email",
        button: "Update"
    }; 
    const formDefaultValues =
    {
        email: ""
    };

    const [Form, setForm] = React.useState(formDefaultValues);
    const [ButtonState, setButtonState] = React.useState(true);
    const [Progress, setProgress] = React.useState(false);
    const [Modal, setModal] = React.useState(modalDefaultValues);

    const FormHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    {
        setForm({ ...Form, [event.currentTarget.name]: event.currentTarget.value });
    };

    const ModalHandler = () => 
    { 
        setModal(modalDefaultValues); 
    };

    const ButtonHandler = async () =>
    {
        if (props.id == null)
        {
            return false;
        }

        let validationResult = ValidateEmail(Form.email);

        if (!Validate.isDefined(validationResult))
        {
            setButtonState(false);
            setProgress(true);

            setModal(await UpdateSubscriberData(
            { 
                id: props.id, 
                email: Form.email, 
                isActivated: true, 
                count: 0 
            }));

            setProgress(false);
            setButtonState(true);
            setForm(formDefaultValues);

            return true;
        }

        setModal(
        { 
            State: true, 
            Titile: "Warning", 
            Message: GetNewsletterWarning(validationResult), 
            Icon: 1 
        });

        return false;
    };

    return (
        <section>
            <Container maxWidth="sm">
                <AlertDialog State={Modal.State} Handle={ModalHandler} Title={Modal.Titile} Message={Modal.Message} Icon={Modal.Icon} />
                <Box pt={18} pb={10}>
                    <Card elevation={4}>
                        <CardContent className={classes.card}>
                            <Box mb={3} textAlign="center">
                                <AccountCircle color="primary" style={{ fontSize: 72 }} />
                                <Typography variant="h5" component="h2" color="textSecondary">
                                    {content.caption}
                                </Typography>
                            </Box>
                            <Box>
                                <Grid container spacing={2}>
                                    <Grid item xs={12}>
                                        <TextField onChange={FormHandler} value={Form.email} variant="outlined" required fullWidth name="email" id="email" label="Email address" autoComplete="email" />
                                    </Grid>
                                </Grid>
                                <Box my={2}>
                                    <Button onClick={ButtonHandler} fullWidth variant="contained" color="primary" disabled={!ButtonState}>
                                        {Progress &&  <CircularProgress size={20} />}
                                        {!Progress && content.button}
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
