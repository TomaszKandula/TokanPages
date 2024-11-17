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

interface UserRemovalViewProps extends ViewProperties, UserRemovalProps {
    deleteButtonHandler: () => void;
    deleteAccountProgress: boolean;
    sectionAccountRemoval: SectionAccountRemoval;
}

const DeleteAccountButton = (props: UserRemovalViewProps): React.ReactElement => {
    return (
        <Button
            fullWidth
            type="submit"
            variant="contained"
            onClick={props.deleteButtonHandler}
            disabled={props.deleteAccountProgress}
            className="delete-update"
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
    return (
        <Box mt={args.marginTop} mb={args.marginBottom}>
            <Divider className="divider" />
        </Box>
    );
};

export const UserRemovalView = (props: UserRemovalViewProps): React.ReactElement => {
    return (
        <section className="section" style={props.background}>
            <Container className="container-wide">
                <Box pb={5}>
                    <Card elevation={0} className="card">
                        <CardContent className="card-content">
                            <Box pt={0} pb={0}>
                                <Typography component="span" className="caption black">
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
                                        <Typography component="span" className="label">
                                            {props.isLoading ? (
                                                <Skeleton variant="text" width="200px" />
                                            ) : (
                                                <ReactHtmlParser html={props.sectionAccountRemoval?.warningText} />
                                            )}
                                        </Typography>
                                    </Grid>
                                </Grid>
                                <CustomDivider marginTop={2} marginBottom={2} />
                                <Grid className="button-container-update">
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
