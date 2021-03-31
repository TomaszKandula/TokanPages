import * as React from "react";
import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import TextField from "@material-ui/core/TextField";
import { CircularProgress } from "@material-ui/core";
import Skeleton from "@material-ui/lab/Skeleton";
import Validate from "validate.js";
import { ValidateEmail } from "../../Shared/validate";
import AlertDialog, { modalDefaultValues } from "../../Shared/Modals/alertDialog";
import { INewsletter } from "../../Api/Models";
import { AddNewSubscriber } from "../../Api/Services/subscribers";
import { GetNewsletterWarning } from "../../Shared/Modals/messageHelper";
import useStyles from "./styledNewsletter";

const formDefaultValues = 
{
    email: ""
};

export default function Newsletter(props: { newsletter: INewsletter, isLoading: boolean })
{
    const classes = useStyles();
    
    const [form, setForm] = React.useState(formDefaultValues);   
    const [modal, setModal] = React.useState(modalDefaultValues);
    const [progress, setProgress] = React.useState(false);

    const modalHandler = () => 
    {
        setModal({ ...modal, State: false}); 
    };

    const formHandler = (event: React.ChangeEvent<HTMLInputElement>) => 
    {
        setForm({ ...form, [event.currentTarget.name]: event.currentTarget.value });
    };

    const buttonHandler = async () =>
    {
        let Results = ValidateEmail(form.email);

        if (!Validate.isDefined(Results))
        {
            setProgress(true);
            setModal(await AddNewSubscriber(
            { 
                email: form.email 
            }));

            setProgress(false); 
            setForm(formDefaultValues);

            return true;
        }

        setModal(
        { 
            State: true, 
            Title: "Warning", 
            Message: GetNewsletterWarning(Results), 
            Icon: 1 
        });

        return false;
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
