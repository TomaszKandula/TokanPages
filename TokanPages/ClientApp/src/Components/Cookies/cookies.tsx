import * as React from "react";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import CardActions from "@material-ui/core/CardActions";
import { SetCookie, GetCookie } from "../../Shared/cookies";
import Validate from "validate.js";
import { ICookiesPromptContentDto } from "../../Api/Models";
import useStyles from "./styleCookies";

export default function Cookies(props: { cookiesPrompt: ICookiesPromptContentDto, isLoading: boolean }) 
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
            days: props.cookiesPrompt.content.days,
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
                                {props.cookiesPrompt.content.caption}
                            </Typography>
                            <Typography variant="subtitle1" component="p" color="textSecondary">
                                {props.cookiesPrompt.content.text}
                            </Typography>            
                        </CardContent>
                        <CardActions>
                            <Button onClick={onClickEvent} color="primary">
                                {props.cookiesPrompt.content.button}
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
