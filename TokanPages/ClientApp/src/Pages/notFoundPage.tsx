import * as React from "react";
import { Link } from "react-router-dom";
import { Box, Button, Container, Typography } from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";
import { notFoundContentDefault } from "../Api/Defaults";
import { getNotFoundContent } from "../Api/Services";

const useStyles = makeStyles(() => (
{
    mainLink:
    {
        textDecoration: "none"
    }
}));

export default function NotFoundPage()
{
    const classes = useStyles();   
    const mountedRef = React.useRef(true);
    const [notFound, setNotFoundContent] = React.useState(notFoundContentDefault);

    const updateContent = React.useCallback(async () => 
    {
        if (!mountedRef.current) return;
        setNotFoundContent(await getNotFoundContent());
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
                    <Typography variant="h1">{notFound.content.code}</Typography>
                    <Typography variant="h4" component="h2" gutterBottom={true}>{notFound.content.header}</Typography>
                    <Typography variant="subtitle1" color="textSecondary">{notFound.content.description}</Typography>
                    <Box mt={4}>
                        <Link to="/" className={classes.mainLink}>
                            <Button variant="contained" color="primary">{notFound.content.button}</Button>
                        </Link>
                    </Box>
                </Box>
            </Container>
      </section> 
    );
}
