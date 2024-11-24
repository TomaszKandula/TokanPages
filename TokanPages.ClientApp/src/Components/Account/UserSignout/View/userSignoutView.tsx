import * as React from "react";
import { Link } from "react-router-dom";
import { Skeleton } from "@material-ui/lab";
import Container from "@material-ui/core/Container";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import Button from "@material-ui/core/Button";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import { AccountCircle } from "@material-ui/icons";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { UserSignoutProps } from "../userSignout";

interface UserSignoutViewProps extends ViewProperties, UserSignoutProps {
    languageId: string;
    caption: string;
    status: string;
    buttonText: string;
    isAnonymous: boolean;
}

export const UserSignoutView = (props: UserSignoutViewProps): React.ReactElement => {
    return (
        <section className="section" style={props.background}>
            <Container maxWidth="sm">
                <div style={{ paddingTop: props.pt ?? 32, paddingBottom: props.pb ?? 80 }}>
                    <Card elevation={0} className="card">
                        <CardContent className="card-content">
                            <div style={{ textAlign: "center", marginBottom: 24 }}>
                                <AccountCircle className="account" />
                                <Typography className="caption">
                                    {props.isLoading ? <Skeleton variant="text" /> : props.caption}
                                </Typography>
                            </div>
                            <div>
                                <Grid container spacing={2}>
                                    <Grid item xs={12}>
                                        <Typography className="status">
                                            {props.isLoading ? <Skeleton variant="text" /> : props.status}
                                        </Typography>
                                    </Grid>
                                </Grid>
                            </div>
                            <div style={{ marginTop: 32 }}>
                                <Link to={`/${props.languageId}`} className="link">
                                    <Button
                                        fullWidth
                                        variant="contained"
                                        className="button"
                                        disabled={props.isLoading || !props.isAnonymous}
                                    >
                                        {props.buttonText}
                                    </Button>
                                </Link>
                            </div>
                        </CardContent>
                    </Card>
                </div>
            </Container>
        </section>
    );
};
