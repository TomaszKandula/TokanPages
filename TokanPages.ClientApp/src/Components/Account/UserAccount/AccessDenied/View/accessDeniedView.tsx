import * as React from "react";
import { Link } from "react-router-dom";
import ReactHtmlParser from "react-html-parser";
import Box from "@material-ui/core/Box";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import Container from "@material-ui/core/Container";
import Skeleton from "@material-ui/lab/Skeleton";
import { IContentAccount } from "../../../../../Store/States";
import { AccessDeniedStyle } from "./accessDeniedStyle";

import { 
    Button, 
    Divider, 
    Typography
} from "@material-ui/core";

export const AccessDeniedView = (props: IContentAccount): JSX.Element => 
{
    const classes = AccessDeniedStyle();

    const HomeButton = (): JSX.Element => 
    {
        return(
            <Link to="/" className={classes.home_link}>
                <Button fullWidth variant="contained" className={classes.home_button} disabled={props.isLoading}>
                    {props.content?.sectionAccessDenied?.homeButtonText}
                </Button>
            </Link>
        );
    }

    const CustomDivider = (args: { marginTop: number, marginBottom: number }) => 
    {
        return(
            <Box mt={args.marginTop} mb={args.marginBottom}>
                <Divider className={classes.divider} />
            </Box>
        );
    }

    return(
        <section className={classes.section}>
            <Container maxWidth="md">
                <Box pt={15} pb={8}>
                    <Card elevation={0} className={classes.card}>
                        <CardContent className={classes.card_content}>
                            <Box pt={0} pb={0}>
                                <Typography className={classes.caption}>
                                    {props.isLoading 
                                    ? <Skeleton variant="text" /> 
                                    : props.content?.sectionAccessDenied?.accessDeniedCaption}
                                </Typography>
                            </Box>
                            <CustomDivider marginTop={2} marginBottom={1} />
                            <Box pt={3} pb={3}>
                                <Typography component="span" className={classes.access_denied_prompt}>
                                    {props.isLoading 
                                    ? <Skeleton variant="text" height="100px" /> 
                                    : ReactHtmlParser(props.content?.sectionAccessDenied?.accessDeniedPrompt)}
                                </Typography>
                            </Box>
                            {props.isLoading 
                            ? <Skeleton variant="rect" width="100%" height="40px" /> 
                            : <HomeButton />}
                        </CardContent>
                    </Card>
                </Box>
            </Container>
        </section>
    );
}
