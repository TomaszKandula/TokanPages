import * as React from "react";
import { Button, CircularProgress, Grid, Typography } from "@material-ui/core";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import Container from "@material-ui/core/Container";
import Skeleton from "@material-ui/lab/Skeleton";
import { ViewProperties } from "../../../../../Shared/Abstractions";
import { SectionAccountDeactivation } from "../../../../../Api/Models";
import { CustomDivider, RenderParagraphs } from "../../../../../Shared/Components";
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
            className="button-delete"
        >
            {!props.progress ? props.section?.deactivateButtonText : <CircularProgress size={20} />}
        </Button>
    );
};

export const UserDeactivationView = (props: UserDeactivationViewProps): React.ReactElement => {
    return (
        <section className={`section ${props.background ?? ""}`}>
            <Container className="container-wide">
                <div className="pb-40">
                    <Card elevation={0} className="card">
                        <CardContent className="card-content">
                            <Typography component="span" className="caption black">
                                {props.isLoading ? <Skeleton variant="text" /> : props.section?.caption}
                            </Typography>
                            <CustomDivider mt={15} mb={8} />
                            <div className="pt-8 pb-8">
                                <Grid container spacing={2}>
                                    <Grid item>
                                        {props.isLoading ? (
                                            <Skeleton variant="text" width="200px" />
                                        ) : (
                                            <RenderParagraphs text={props.section?.warningText} className="label user-account-text-item" />
                                        )}
                                    </Grid>
                                </Grid>
                                <CustomDivider mt={15} mb={15} />
                                <Grid className="button-container-update">
                                    <div className="mt-15 mb-15">
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
