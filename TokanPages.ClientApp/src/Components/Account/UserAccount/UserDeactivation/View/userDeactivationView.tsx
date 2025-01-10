import * as React from "react";
import { Button, CircularProgress, Grid, Typography } from "@material-ui/core";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import Container from "@material-ui/core/Container";
import Skeleton from "@material-ui/lab/Skeleton";
import { ReactHtmlParser } from "../../../../../Shared/Services/Renderers";
import { ViewProperties } from "../../../../../Shared/Abstractions";
import { SectionAccountDeactivation } from "../../../../../Api/Models";
import { CustomDivider } from "../../../../../Shared/Components";
import { UserDeactivationProps } from "../userDeactivation";

interface UserDeactivationViewProps extends ViewProperties, UserDeactivationProps {
    buttonHandler: () => void;
    progress: boolean;
    section: SectionAccountDeactivation;
}

const DeactivationButton = (props: UserDeactivationViewProps): React.ReactElement => {
    return (
        <Button
            fullWidth
            type="submit"
            variant="contained"
            onClick={props.buttonHandler}
            disabled={props.progress}
            className="delete-update"
        >
            {!props.progress ? props.section?.deactivateButtonText : <CircularProgress size={20} />}
        </Button>
    );
};

export const UserDeactivationView = (props: UserDeactivationViewProps): React.ReactElement => {
    return (
        <section className="section" style={props.background}>
            <Container className="container-wide">
                <div style={{ paddingBottom: 40 }}>
                    <Card elevation={0} className="card">
                        <CardContent className="card-content">
                            <div style={{ paddingTop: 0, paddingBottom: 0 }}>
                                <Typography component="span" className="caption black">
                                    {props.isLoading ? <Skeleton variant="text" /> : props.section?.caption}
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
                                                <ReactHtmlParser html={props.section?.warningText} />
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
                                            <DeactivationButton {...props} />
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
