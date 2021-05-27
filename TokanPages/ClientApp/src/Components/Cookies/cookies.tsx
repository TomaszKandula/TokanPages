import * as React from "react";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import CardActions from "@material-ui/core/CardActions";
import { IGetCookiesPromptContent } from "../../Redux/States/getCookiesPromptContentState";
import { SetCookie, GetCookie } from "../../Shared/cookies";
import Validate from "validate.js";
import useStyles from "./cookiesStyle";

export default function Cookies(props: IGetCookiesPromptContent) 
{
    const classes = useStyles();
    const [close, setClose] = React.useState(false);
    const onClickEvent = () => 
    { 
        setClose(true); 
        SetCookie(
        {
            cookieName: "cookieConsent", 
            value: "granted", 
            days: props.content?.days,
            sameSite: "Strict",
            secure: ""
        });
    };

    const renderConsent = (): JSX.Element => 
    {
        return (		
            <Box position="fixed" width="100%" bottom={0} p={2} zIndex="modal" className={close ? classes.close : classes.open}>
                <Container maxWidth="md">
                    <Card>
                        <CardContent>
                            <Typography variant="h5" component="h2" gutterBottom={true}>
                                {props.content?.caption}
                            </Typography>
                            <Typography variant="subtitle1" component="p" color="textSecondary">
                                {props.content?.text}
                            </Typography>            
                        </CardContent>
                        <CardActions>
                            <Button onClick={onClickEvent} color="primary">
                                {props.content?.button}
                            </Button>
                        </CardActions>
                    </Card>
                </Container>
            </Box>
        );
    }

    return (
        <>
            {Validate.isEmpty(GetCookie({cookieName: "cookieConsent"})) 
                ? renderConsent() 
                : null}
        </>
    );
}
