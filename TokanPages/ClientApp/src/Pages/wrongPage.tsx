import * as React from "react";
import { Link } from "react-router-dom";
import { Box, Button, Container, Typography } from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";
import { wrongPagePromptContentDefault } from "../Api/Defaults";
import { getWrongPagePromptContent } from "../Api/Services";

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
    const mountedRef = React.useRef(true);
    const [wrongPagePrompt, setWrongPagePromptContent] = React.useState(wrongPagePromptContentDefault);

    const updateContent = React.useCallback(async () => 
    {
        if (!mountedRef.current) return;
        setWrongPagePromptContent(await getWrongPagePromptContent());
    }, [ ]);

    React.useEffect(() => 
    {
        updateContent();
        return () => { mountedRef.current = false; };
    }, 
    [ updateContent ]);

    return (
        <section>
            <Container maxWidth="md">
                <Box pt={8} pb={10} textAlign="center">
                    <Typography variant="h1">{wrongPagePrompt.content.code}</Typography>
                    <Typography variant="h4" component="h2" gutterBottom={true}>{wrongPagePrompt.content.header}</Typography>
                    <Typography variant="subtitle1" color="textSecondary">{wrongPagePrompt.content.description}</Typography>
                    <Box mt={4}>
                        <Link to="/" className={classes.mainLink}>
                            <Button variant="contained" color="primary">{wrongPagePrompt.content.button}</Button>
                        </Link>
                    </Box>
                </Box>
            </Container>
      </section> 
    );
}
