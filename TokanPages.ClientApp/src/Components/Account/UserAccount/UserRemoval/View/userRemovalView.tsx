import * as React from "react";
import Box from "@material-ui/core/Box";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import Container from "@material-ui/core/Container";
import Skeleton from "@material-ui/lab/Skeleton";
import { ReactHtmlParser } from "../../../../../Shared/Services/Renderers";
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

interface IProperties
{    
    isLoading: boolean;
    deleteButtonHandler: any;
    deleteAccountProgress: boolean;
    sectionAccessDenied: ISectionAccessDenied;
    sectionAccountRemoval: ISectionAccountRemoval;
}

const DeleteAccountButton = (props: IProperties): JSX.Element => 
{
    const classes = UserRemovalStyle();
    return(
        <Button 
            fullWidth 
            type="submit" 
            variant="contained" 
            onClick={props.deleteButtonHandler} 
            disabled={props.deleteAccountProgress} 
            className={classes.delete_update}>
            {!props.deleteAccountProgress 
            ? props.sectionAccountRemoval?.deleteButtonText 
            : <CircularProgress size={20} />}
        </Button>
    );
}

const CustomDivider = (args: { marginTop: number, marginBottom: number }) => 
{
    const classes = UserRemovalStyle();
    return(
        <Box mt={args.marginTop} mb={args.marginBottom}>
            <Divider className={classes.divider} />
        </Box>
    );
}

export const UserRemovalView = (props: IProperties): JSX.Element => 
{
    const classes = UserRemovalStyle();
    return(
        <section className={classes.section}>
            <Container maxWidth="md">
                <Box pt={5} pb={10}>
                    <Card elevation={0} className={classes.card}>
                        <CardContent className={classes.card_content}>
                            <Box pt={0} pb={0}>
                                <Typography component="span" className={classes.caption}>
                                    {props.isLoading 
                                    ? <Skeleton variant="text" /> 
                                    : props.sectionAccountRemoval?.caption}
                                </Typography>
                            </Box>
                            <CustomDivider marginTop={2} marginBottom={1} />
                            <Box pt={1} pb={1}>
                                <Grid container spacing={2}>
                                    <Grid item>
                                        <Typography component="span" className={classes.label}>
                                            {props.isLoading 
                                            ? <Skeleton variant="text" width="200px" /> 
                                            : <ReactHtmlParser html={props.sectionAccountRemoval?.warningText} />}
                                        </Typography>
                                    </Grid>
                                </Grid>
                                <CustomDivider marginTop={2} marginBottom={2} />
                                <Grid className={classes.button_container_update}>
                                    <Box my={2}>
                                        {props.isLoading 
                                        ? <Skeleton variant="rect" width="150px" height="40px" /> 
                                        : <DeleteAccountButton {...props} />}
                                    </Box>
                                </Grid>
                            </Box>
                        </CardContent>
                    </Card>
                </Box>
            </Container>
        </section>
    );
}
