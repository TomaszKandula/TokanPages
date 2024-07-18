import * as React from "react";
import { Button, CircularProgress, Divider, Grid, Typography } from "@material-ui/core";
import Box from "@material-ui/core/Box";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import Container from "@material-ui/core/Container";
import Skeleton from "@material-ui/lab/Skeleton";
import { SectionAccountRemoval } from "../../../../../Api/Models";
import { ReactHtmlParser } from "../../../../../Shared/Services/Renderers";
import { ViewProperties } from "../../../../../Shared/Abstractions";
import { UserRemovalProps } from "../userRemoval";
import { UserRemovalStyle } from "./userRemovalStyle";

interface UserRemovalViewProps extends ViewProperties, UserRemovalProps {
    deleteButtonHandler: () => void;
    deleteAccountProgress: boolean;
    sectionAccountRemoval: SectionAccountRemoval;
}

const DeleteAccountButton = (props: UserRemovalViewProps): JSX.Element => {
    const classes = UserRemovalStyle();
    return (
        <Button
            fullWidth
            type="submit"
            variant="contained"
            onClick={props.deleteButtonHandler}
            disabled={props.deleteAccountProgress}
            className={classes.delete_update}
        >
            {!props.deleteAccountProgress ? (
                props.sectionAccountRemoval?.deleteButtonText
            ) : (
                <CircularProgress size={20} />
            )}
        </Button>
    );
};

const CustomDivider = (args: { marginTop: number; marginBottom: number }) => {
    const classes = UserRemovalStyle();
    return (
        <Box mt={args.marginTop} mb={args.marginBottom}>
            <Divider className={classes.divider} />
        </Box>
    );
};

export const UserRemovalView = (props: UserRemovalViewProps): JSX.Element => {
    const classes = UserRemovalStyle();
    return (
        <section className={classes.section} style={props.background}>
            <Container className={classes.container}>
                <Box pb={5}>
                    <Card elevation={0} className={classes.card}>
                        <CardContent className={classes.card_content}>
                            <Box pt={0} pb={0}>
                                <Typography component="span" className={classes.caption}>
                                    {props.isLoading ? (
                                        <Skeleton variant="text" />
                                    ) : (
                                        props.sectionAccountRemoval?.caption
                                    )}
                                </Typography>
                            </Box>
                            <CustomDivider marginTop={2} marginBottom={1} />
                            <Box pt={1} pb={1}>
                                <Grid container spacing={2}>
                                    <Grid item>
                                        <Typography component="span" className={classes.label}>
                                            {props.isLoading ? (
                                                <Skeleton variant="text" width="200px" />
                                            ) : (
                                                <ReactHtmlParser html={props.sectionAccountRemoval?.warningText} />
                                            )}
                                        </Typography>
                                    </Grid>
                                </Grid>
                                <CustomDivider marginTop={2} marginBottom={2} />
                                <Grid className={classes.button_container_update}>
                                    <Box my={2}>
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="150px" height="40px" />
                                        ) : (
                                            <DeleteAccountButton {...props} />
                                        )}
                                    </Box>
                                </Grid>
                            </Box>
                        </CardContent>
                    </Card>
                </Box>
            </Container>
        </section>
    );
};
