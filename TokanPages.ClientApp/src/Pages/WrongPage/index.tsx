import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { Link } from "react-router-dom";
import { Box, Button, Container, Typography } from "@material-ui/core";
import { Skeleton } from "@material-ui/lab";
import { IApplicationState } from "../../Store/Configuration";
import { ActionCreators } from "../../Store/Actions/Content/getWrongPagePromptContentAction";
import { Style } from "./style";

export const WrongPage = (): JSX.Element =>
{
    const classes = Style();   
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

    const language = useSelector((state: IApplicationState) => state.userLanguage);
    const wrongPagePrompt = useSelector((state: IApplicationState) => state.getWrongPagePromptContent);
    React.useEffect(() => 
    { 
        dispatch(ActionCreators.getWrongPagePromptContent()) 
    }, 
    [ dispatch, language?.id ]);

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
