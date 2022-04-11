import * as React from "react";
import Box from "@material-ui/core/Box";
import Button from "@material-ui/core/Button";
import CircularProgress from "@material-ui/core/CircularProgress";
import Container from "@material-ui/core/Container";
import Typography from "@material-ui/core/Typography";
import { Card, CardContent } from "@material-ui/core";
import Skeleton from "@material-ui/lab/Skeleton";
import activateAccountStyle from "./Styles/activateAccountStyle";

interface IBinding 
{
    bind: IProperties;
}

interface IProperties
{
    isLoading: boolean;
    caption: string;
    text1: string;
    text2: string;
    buttonHandler: any;
    buttonDisabled: boolean;
    progress: boolean;
    buttonText: string;
}

const ActivateAccountView = (props: IBinding): JSX.Element =>
{
    const classes = activateAccountStyle();

    const ActiveButton = (): JSX.Element => 
    {
        return(
            <Button fullWidth onClick={props.bind?.buttonHandler} type="submit" variant="contained" 
                className={classes.button} disabled={props.bind?.buttonDisabled}>
                {props.bind?.progress &&  <CircularProgress size={20} />}
                {!props.bind?.progress && props.bind?.buttonText}
            </Button>
        );
    }

    return (
        <section className={classes.section}>
            <Container maxWidth="sm">
                <Box py={15}>
                    <Card elevation={4}>
                        <CardContent className={classes.card}>
                        <Box textAlign="center" mb={3}>
                            <Box mt={2} mb={2}>
                                <Typography gutterBottom={true} className={classes.caption}>
                                    {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.caption}
                                </Typography>
                            </Box>
                            <Box mt={5} mb={2}>
                                <Typography className={classes.text1}>
                                    {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.text1}
                                </Typography>
                            </Box>
                            <Box mt={2} mb={5}>
                                <Typography className={classes.text2}>
                                    {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.text2}
                                </Typography>
                            </Box>
                            {props.bind?.isLoading ? <Skeleton variant="rect" /> : <ActiveButton />}
                        </Box>
                        </CardContent>
                    </Card>
                </Box>
            </Container>
        </section>
    );
}

export default ActivateAccountView;
