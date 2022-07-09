import * as React from "react";
import { Skeleton } from "@material-ui/lab";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import CardActions from "@material-ui/core/CardActions";
import cookiesStyle from "./cookiesStyle";

interface IBinding
{
    bind: IProperties;
}

interface IProperties
{
    isLoading: boolean;
    modalClose: boolean;
    shouldShow: boolean;
    caption: string;
    text: string;
    onClickEvent: any;
    buttonText: string;
}

const CookiesView = (props: IBinding): JSX.Element => 
{
    const classes = cookiesStyle();

    const ActiveButton = (): JSX.Element => 
    {
        return(
            <Button onClick={props.bind?.onClickEvent} className={classes.button}>
                {props.bind?.buttonText}
            </Button>
        );
    }

    const renderConsent = (): JSX.Element => 
    {
        return (		
            <Box position="fixed" width="100%" bottom={0} p={3} zIndex="modal" className={props.bind?.modalClose ? classes.close : classes.open}>
                <Container maxWidth="md">
                    <Card elevation={0} className={classes.container}>
                        <CardContent>
                            <Typography className={classes.caption}>
                                {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.caption}
                            </Typography>
                            <Typography className={classes.text}>
                                {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.text}
                            </Typography>
                        </CardContent>
                        <CardActions>
                            {props.bind?.isLoading ? <Skeleton variant="rect" /> : <ActiveButton />}
                        </CardActions>
                    </Card>
                </Container>
            </Box>
        );
    }

    return (<>{props.bind?.shouldShow ? renderConsent() : null}</>);
}

export default CookiesView;
