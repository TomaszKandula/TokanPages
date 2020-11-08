import * as React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import TextField from "@material-ui/core/TextField";
import { Card, CardContent } from "@material-ui/core";
import { AccountCircle } from "@material-ui/icons";

const useStyles = makeStyles(() => (
{
    card:
    {
        marginTop: 10,
        marginLeft: 15,
        marginRight: 15,
        marginBottom: 10
    }
}));

export default function ResetForm() 
{

    const classes = useStyles();

    return (
        <section>
            <Container maxWidth="sm">
                <Box pt={18} pb={10}>             
                    <Card elevation={4}>
                        <CardContent className={classes.card}>
                            <Box mb={3} textAlign="center">
                                <AccountCircle color="primary" style={{ fontSize: 72 }} />
                                <Typography variant="h5" component="h2" color="textSecondary">
                                    Reset password
                                </Typography>
                            </Box>
                            <Box>
                                <Grid container spacing={2}>
                                    <Grid item xs={12}>
                                        <TextField variant="outlined" required fullWidth name="email" id="email" label="Email address" autoComplete="email" />
                                    </Grid>
                                </Grid>
                                <Box my={2}>
                                    <Button fullWidth variant="contained" color="primary">
                                        Reset
                                    </Button>
                                </Box>
                            </Box>
                        </CardContent>   
                    </Card>                    
                </Box>
            </Container>
        </section>
    );

}
