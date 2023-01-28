import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { Link } from "react-router-dom";
import { Box, Button, Container, Typography } from "@material-ui/core";
import { Skeleton } from "@material-ui/lab";
import { IApplicationState } from "../../Store/Configuration";
import { ContentWrongPagePromptAction } from "../../Store/Actions";
import { Style } from "./style";

const ActionButton = (args: { button: string }): JSX.Element => 
{
    const classes = Style();
    return(
        <Link to="/" className={classes.link}>
            <Button variant="contained" className={classes.button}>
                {args.button}
            </Button>
        </Link>
    );
}

export const WrongPage = (): JSX.Element =>
{
    const classes = Style();
    const dispatch = useDispatch();
    const language = useSelector((state: IApplicationState) => state.applicationLanguage);
    const prompt = useSelector((state: IApplicationState) => state.contentWrongPagePrompt);

    React.useEffect(() => 
    { 
        dispatch(ContentWrongPagePromptAction.get());
    }, [ language?.id ]);

    return (
        <section>
            <Container maxWidth="md">
                <Box pt={8} pb={10} textAlign="center">
                    {prompt?.isLoading
                    ? <Skeleton variant="text" height="200px" width="250px" className={classes.skeleton} />
                    : <Typography variant="h1">{prompt?.content.code}</Typography>}

                    {prompt?.isLoading
                    ? <Skeleton variant="text" height="45px" width="80%" className={classes.skeleton} />
                    : <Typography variant="h4" component="h2" gutterBottom={true}>{prompt?.content.header}</Typography>}

                    {prompt?.isLoading
                    ? <Skeleton variant="text" height="45px" className={classes.skeleton} />
                    : <Typography variant="subtitle1" color="textSecondary">{prompt?.content.description}</Typography>}

                    <Box mt={4}>
                        {prompt?.isLoading
                        ? <Skeleton variant="rect" height="60px" width="220px" className={classes.skeleton} />
                        : <ActionButton button={prompt?.content.button} />}
                    </Box>
                </Box>
            </Container>
      </section> 
    );
}
