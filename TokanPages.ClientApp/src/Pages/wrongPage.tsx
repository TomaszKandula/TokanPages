import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { Link } from "react-router-dom";
import { Box, Button, Container, Typography } from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";
import { Skeleton } from "@material-ui/lab";
import { IApplicationState } from "../Redux/applicationState";
import { ActionCreators } from "../Redux/Actions/Content/getWrongPagePromptContentAction";
import { CustomColours } from "../Theme/customColours";

const useStyles = makeStyles(() => 
({
    link:
    {
        textDecoration: "none"
    },
    skeleton:
    {
        marginLeft: "auto",
        marginRight: "auto"
    },
    button:
    {
        "&:hover": 
        {
            color: CustomColours.colours.white,
            background: CustomColours.colours.darkViolet1,
        },
        color: CustomColours.colours.white,
        background: CustomColours.colours.violet
    }
}));

const WrongPage = (): JSX.Element =>
{
    const classes = useStyles();   
    const dispatch = useDispatch();

    const ActionButton = (): JSX.Element => 
    {
        return(
            <Link to="/" className={classes.link}>
s                <Button variant="contained" className={classes.button}>
                    {wrongPagePrompt?.content.button}
                </Button>
            </Link>
        );
    }

    const wrongPagePrompt = useSelector((state: IApplicationState) => state.getWrongPagePromptContent);
    React.useEffect(() => { dispatch(ActionCreators.getWrongPagePromptContent()) }, [ dispatch ]);

    return (
        <section>
            <Container maxWidth="md">
                <Box pt={8} pb={10} textAlign="center">

                    {wrongPagePrompt?.isLoading
                    ? <Skeleton variant="text" height="200px" width="250px" className={classes.skeleton} />
                    : <Typography variant="h1">{wrongPagePrompt?.content.code}</Typography>}

                    {wrongPagePrompt?.isLoading
                    ? <Skeleton variant="text" height="45px" width="80%" className={classes.skeleton} />
                    : <Typography variant="h4" component="h2" gutterBottom={true}>{wrongPagePrompt?.content.header}</Typography>}

                    {wrongPagePrompt?.isLoading
                    ? <Skeleton variant="text" height="45px" className={classes.skeleton} />
                    : <Typography variant="subtitle1" color="textSecondary">{wrongPagePrompt?.content.description}</Typography>}

                    <Box mt={4}>
                        {wrongPagePrompt?.isLoading
                        ? <Skeleton variant="rect" height="60px" width="220px" className={classes.skeleton} />
                        : <ActionButton />}
                    </Box>
                </Box>
            </Container>
      </section> 
    );
}

export default WrongPage;
