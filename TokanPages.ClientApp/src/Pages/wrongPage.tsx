import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { Link } from "react-router-dom";
import { Box, Button, Container, Typography } from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";
import { IApplicationState } from "../Redux/applicationState";
import { ActionCreators as WrongPagePromptContent } from "../Redux/Actions/Content/getWrongPagePromptContentAction";

const useStyles = makeStyles(() => (
{
    mainLink:
    {
        textDecoration: "none"
    }
}));

export default function WrongPage()
{
    const classes = useStyles();   
    const dispatch = useDispatch();

    const wrongPagePrompt = useSelector((state: IApplicationState) => state.getWrongPagePromptContent);
    const getContent = React.useCallback(() =>
    {
        dispatch(WrongPagePromptContent.getWrongPagePromptContent());
    }, [ dispatch ]);

    React.useEffect(() => getContent(), [ getContent ]);

    return (
        <section>
            <Container maxWidth="md">
                <Box pt={8} pb={10} textAlign="center">
                    <Typography variant="h1">{wrongPagePrompt?.content.code}</Typography>
                    <Typography variant="h4" component="h2" gutterBottom={true}>{wrongPagePrompt?.content.header}</Typography>
                    <Typography variant="subtitle1" color="textSecondary">{wrongPagePrompt?.content.description}</Typography>
                    <Box mt={4}>
                        <Link to="/" className={classes.mainLink}>
                            <Button variant="contained" color="primary">{wrongPagePrompt?.content.button}</Button>
                        </Link>
                    </Box>
                </Box>
            </Container>
      </section> 
    );
}
