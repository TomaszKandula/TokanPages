import * as React from "react";
import { Link } from "react-router-dom";
import ReactHtmlParser from "react-html-parser";
import Box from "@material-ui/core/Box";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import Container from "@material-ui/core/Container";
import Skeleton from "@material-ui/lab/Skeleton";
import { UserRemovalStyle } from "./userRemovalStyle";

import { 
    Button, 
    CircularProgress, 
    Divider, 
    Grid, 
    Typography
} from "@material-ui/core";

import { 
    ISectionAccessDenied, 
    ISectionAccountRemoval 
} from "../../../../../Api/Models";

interface IBinding 
{
    bind: IProperties;
}

interface IProperties
{    
    isLoading: boolean;
    isAnonymous: boolean;
    deleteButtonHandler: any;
    deleteAccountProgress: boolean;
    sectionAccessDenied: ISectionAccessDenied;
    sectionAccountRemoval: ISectionAccountRemoval;
}

export const UserRemovalView = (props: IBinding): JSX.Element => 
{
    const classes = UserRemovalStyle();

    const DeleteAccountButton = (): JSX.Element => 
    {
        return(
            <Button fullWidth onClick={props.bind?.deleteButtonHandler} type="submit" variant="contained" 
                disabled={props.bind?.deleteAccountProgress} className={classes.delete_update}>
                {props.bind?.deleteAccountProgress &&  <CircularProgress size={20} />}
                {!props.bind?.deleteAccountProgress && props.bind?.sectionAccountRemoval?.deleteButtonText}
            </Button>
        );
    }

    const HomeButton = (): JSX.Element => 
    {
        return(
            <Link to="/" className={classes.home_link}>
                <Button fullWidth variant="contained" className={classes.home_button} disabled={props.bind?.isLoading}>
                    {props.bind?.sectionAccessDenied?.homeButtonText}
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
        <>
        <section className={classes.section} style={props.bind?.isAnonymous ? { display: "block" }: { display: "none" }}>
            <Container maxWidth="md">
                <Box pt={15} pb={8}>
                    <Card elevation={0} className={classes.card}>
                        <CardContent className={classes.card_content}>
                            <Box pt={0} pb={0}>
                                <Typography className={classes.caption}>
                                    {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.sectionAccessDenied?.accessDeniedCaption}
                                </Typography>
                            </Box>
                            <CustomDivider marginTop={2} marginBottom={1} />
                            <Box pt={3} pb={3}>
                                <Typography component="span" className={classes.access_denied_prompt}>
                                    {props.bind?.isLoading ? <Skeleton variant="text" height="100px" /> : ReactHtmlParser(props.bind?.sectionAccessDenied?.accessDeniedPrompt)}
                                </Typography>
                            </Box>
                            {props.bind?.isLoading ? <Skeleton variant="rect" width="100%" height="40px" /> : <HomeButton />}
                        </CardContent>
                    </Card>
                </Box>
            </Container>
        </section>

        <section className={classes.section} style={props.bind?.isAnonymous ? { display: "none" }: { display: "block" }}>
            <Container maxWidth="md">

                <Box pt={8} pb={10}>
                    <Card elevation={0} className={classes.card}>
                        <CardContent className={classes.card_content}>
                            <Box pt={0} pb={0}>
                                <Typography className={classes.caption}>
                                    {props.bind?.isLoading ? <Skeleton variant="text" /> : props.bind?.sectionAccountRemoval?.caption}
                                </Typography>
                            </Box>
                            <CustomDivider marginTop={2} marginBottom={1} />
                            <Box pt={1} pb={1}>
                                <Grid container spacing={2}>
                                    <Grid item>
                                        <Typography className={classes.label}>
                                            {props.bind?.isLoading ? <Skeleton variant="text" /> : ReactHtmlParser(props.bind?.sectionAccountRemoval?.warningText)}
                                        </Typography>
                                    </Grid>
                                </Grid>
                                <CustomDivider marginTop={2} marginBottom={2} />
                                <Grid className={classes.button_container_update}>
                                    <Box my={2}>
                                        {props.bind?.isLoading 
                                        ? <Skeleton variant="rect" width="150px" height="40px" /> 
                                        : <DeleteAccountButton />}
                                    </Box>
                                </Grid>
                            </Box>
                        </CardContent>
                    </Card>
                </Box>

            </Container>
        </section>
        </>
    );
}
