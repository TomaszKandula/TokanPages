import * as React from "react";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import { AccountCircle } from "@material-ui/icons";
import userSignoutStyle from "./Styles/userSignoutStyle";

interface IBinding 
{
    bind: IProperties;
}

interface IProperties
{
    caption: string;
    status: string;
}

const UserSignoutView = (props: IBinding): JSX.Element => 
{
    const classes = userSignoutStyle();
    return (
        <section>
            <Container maxWidth="sm">
                <Box pt={18} pb={10}>             
                    <Card elevation={4}>
                        <CardContent className={classes.card}>
                            <Box mb={3} textAlign="center">
                                <AccountCircle color="primary" style={{ fontSize: 72 }} />
                                <Typography variant="h5" component="h2" color="textSecondary">
                                    {props.bind.caption}
                                </Typography>
                            </Box>
                            <Box>
                                <Grid container spacing={2}>
                                    <Grid item xs={12}>
                                        <Typography variant="body1" component="p" color="textSecondary">
                                            {props.bind.status}
                                        </Typography>
                                    </Grid>
                                </Grid>
                            </Box>
                        </CardContent>   
                    </Card>                    
                </Box>
            </Container>
        </section>
    );
}

export default UserSignoutView;
