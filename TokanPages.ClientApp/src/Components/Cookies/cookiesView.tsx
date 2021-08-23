import * as React from "react";
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
    const renderConsent = (): JSX.Element => 
    {
        return (		
            <Box position="fixed" width="100%" bottom={0} p={2} zIndex="modal" className={props.bind?.modalClose ? classes.close : classes.open}>
                <Container maxWidth="md">
                    <Card>
                        <CardContent>
                            <Typography variant="h5" component="h2" gutterBottom={true}>
                                {props.bind?.caption}
                            </Typography>
                            <Typography variant="subtitle1" component="p" color="textSecondary">
                                {props.bind?.text}
                            </Typography>            
                        </CardContent>
                        <CardActions>
                            <Button onClick={props.bind?.onClickEvent} color="primary">
                                {props.bind?.buttonText}
                            </Button>
                        </CardActions>
                    </Card>
                </Container>
            </Box>
        );
    }

    return (<>{props.bind?.shouldShow ? renderConsent() : null}</>);
}

export default CookiesView;
