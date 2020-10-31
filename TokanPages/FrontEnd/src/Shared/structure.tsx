import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";

const useStyles = makeStyles((theme) => (
{
    
    block: 
    {
        marginTop: theme.spacing(3),
        marginBottom: theme.spacing(5)
    }
    
}));

export default function Structure(props: { column1: any; column2: any })
{

    const classes = useStyles();

    return(
        <Container className={classes.block}>
            <Grid container spacing={2}>
                <Grid item xs={12} md={6}>
                    {props.column1}
                </Grid>
                <Grid item xs={12} md={6}>
                    {props.column2}
                </Grid>
            </Grid>
        </Container>
    );

}
   