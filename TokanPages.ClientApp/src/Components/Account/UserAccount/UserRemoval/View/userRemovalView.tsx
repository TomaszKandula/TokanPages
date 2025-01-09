import * as React from "react";
import { Button, CircularProgress, Grid, Typography } from "@material-ui/core";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import Container from "@material-ui/core/Container";
import Skeleton from "@material-ui/lab/Skeleton";
import { SectionAccountRemoval } from "../../../../../Api/Models";
import { ReactHtmlParser } from "../../../../../Shared/Services/Renderers";
import { ViewProperties } from "../../../../../Shared/Abstractions";
import { CustomDivider } from "../../../../../Shared/Components";
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

export const UserRemovalView = (props: UserRemovalViewProps): React.ReactElement => {
    return (
        <section className="section" style={props.background}>
            <Container className="container-wide">
                <div style={{ paddingBottom: 40 }}>
                    <Card elevation={0} className="card">
                        <CardContent className="card-content">
                            <div style={{ paddingTop: 0, paddingBottom: 0 }}>
                                <Typography component="span" className="caption black">
                                    {props.isLoading ? (
                                        <Skeleton variant="text" />
                                    ) : (
                                        props.sectionAccountRemoval?.caption
                                    )}
                                </Typography>
                            </div>
                            <CustomDivider marginTop={16} marginBottom={8} />
                            <div style={{ paddingTop: 8, paddingBottom: 8 }}>
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
                                <CustomDivider marginTop={16} marginBottom={16} />
                                <Grid className="button-container-update">
                                    <div style={{ marginTop: 16, marginBottom: 16 }}>
                                        {props.isLoading ? (
                                            <Skeleton variant="rect" width="150px" height="40px" />
                                        ) : (
                                            <DeleteAccountButton {...props} />
                                        )}
                                    </div>
                                </Grid>
                            </div>
                        </CardContent>
                    </Card>
                </div>
            </Container>
        </section>
    );
};
