import * as React from "react";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import Button from "@material-ui/core/Button";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import { AccountCircle } from "@material-ui/icons";
import { UserSignoutStyle } from "./userSignoutStyle";

interface IProperties
{
    isLoading: boolean;
    caption: string;
    status: string;
    buttonText: string;
}

export const UserSignoutView = (props: IProperties): JSX.Element => 
{
    const classes = UserSignoutStyle();
    return (
        <section className={classes.section}>
            <Container maxWidth="sm">
                <Box pt={18} pb={10}>             
                    <Card elevation={0} className={classes.card}>
                        <CardContent className={classes.card_content}>
                            <Box mb={3} textAlign="center">
                                <AccountCircle className={classes.account} />
                                <Typography className={classes.caption}>{props.caption}</Typography>
                            </Box>
                            <Box>
                                <Grid container spacing={2}>
                                    <Grid item xs={12}>
                                        <Typography className={classes.status}>{props.status}</Typography>
                                    </Grid>
                                </Grid>
                            </Box>
                            <Box mt={4}>
                                <Link to="/" className={classes.link}>
                                    <Button 
                                        fullWidth 
                                        variant="contained" 
                                        className={classes.button} 
                                        disabled={props.isLoading}>
                                        {props.buttonText}
                                    </Button>
                                </Link>
                            </Box>
                        </CardContent>   
                    </Card>                    
                </Box>
            </Container>
        </section>
    );
}
