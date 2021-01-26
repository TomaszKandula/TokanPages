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
import AlertDialog, { modalDefaultValues } from "../../Shared/Modals/alertDialog";
import { ValidateEmail } from "../../Shared/validate";
import { AddNewSubscriber } from "../../Api/Services/subscribers";
import { GetNewsletterWarning } from "../../Shared/Modals/messageHelper";

export default function Newsletter()
{
    const classes = useStyles();
    const formDefaultValues = 
    {
        email: ""    
    };
    const content = 
    {
        caption: "Join the newsletter!",
        text: "We will never share your email address.",
        button: "Subscribe",
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
        setForm({ ...Form, [event.currentTarget.name]: event.currentTarget.value });
    }

    const ButtonHandler = async () =>
    {
        let Results = ValidateEmail(Form.email);

        if (!Validate.isDefined(Results))
        {
            setProgress(true);
            setModal(await AddNewSubscriber(
            { 
                email: Form.email 
            }));

            setProgress(false); 
            setForm(formDefaultValues);

            return true;
        }

        setModal(
        { 
            State: true, 
            Titile: "Warning", 
            Message: GetNewsletterWarning(Results), 
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
