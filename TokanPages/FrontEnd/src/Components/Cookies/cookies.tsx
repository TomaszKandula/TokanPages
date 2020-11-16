import React, { useState } from "react";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import CardActions from "@material-ui/core/CardActions";
import { SetCookie, GetCookie } from "../../Shared/cookies";
import Validate from "validate.js";
import useStyles from "./styleCookies";

export default function Cookies() 
{

    const classes = useStyles();
    const [ close, setClose ] = useState(false);

    const onClickEvent = () => 
    { 
        setClose(true); 
        SetCookie(
        {
            cookieName: "cookieConsent", 
            value: "granted", 
            days: 3,
            sameSite: "Strict",
            secure: ""
        });
    }

    const renderConsent = () => 
    {

        return (		
            <Box position="fixed" width="100%" bottom={0} p={2} zIndex="modal" className={ close ? classes.close : classes.open }>
                <Container maxWidth="md">
                    <Card>
                        <CardContent>
                            <Typography variant="h5" component="h2" gutterBottom={true}>
                                Cookie Policy
                            </Typography>
                            <Typography variant="subtitle1" component="p" color="textSecondary">
                                We use cookies to personalise content, to provide social media features and to analyse our traffic. 
                                We also share information about your use of our site with our social media, advertising and analytics partners.
                                Your consent will be valid 3 days or until you clear all the cookies from your web browser.
                            </Typography>            
                        </CardContent>
                        <CardActions>
                            <Button onClick={onClickEvent} color="primary">
                                Accept cookies
                            </Button>
                        </CardActions>
                    </Card>
                </Container>
            </Box>
        );

    }

    const Content = Validate.isEmpty(GetCookie({cookieName: "cookieConsent"})) ? renderConsent() : null;
    return (<>{Content}</>);

}
