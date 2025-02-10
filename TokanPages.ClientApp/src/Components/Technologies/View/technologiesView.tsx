import * as React from "react";
import { useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import CodeIcon from "@material-ui/icons/Code";
import LibraryBooksIcon from "@material-ui/icons/LibraryBooks";
import StorageIcon from "@material-ui/icons/Storage";
import CloudIcon from "@material-ui/icons/Cloud";
import Skeleton from "@material-ui/lab/Skeleton";
import { ApplicationState } from "../../../Store/Configuration";
import { ReactHtmlParser } from "../../../Shared/Services/Renderers";
import { Animated } from "../../../Shared/Components";

interface TechnologiesViewProps {
    background?: React.CSSProperties;
}

export const TechnologiesView = (props: TechnologiesViewProps): React.ReactElement => {
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const technology = data?.components?.technologies;

    return (
        <section className="section-grey" style={props.background}>
            <Container className="container-super-wide">
                <div className="technology-box">
                    <Animated dataAos="fade-down" className="mb-64">
                        <h1 className="technology-caption-text">
                            {data?.isLoading ? <Skeleton variant="text" /> : technology?.caption?.toUpperCase()}
                        </h1>
                    </Animated>
                    <Grid container spacing={6}>
                        <Grid item xs={12} sm={6}>
                            <Animated dataAos="fade-up" className="flex-centre mb-15">
                                {data?.isLoading ? (
                                    <Skeleton variant="circle" className="technology-skeleton-circle" />
                                ) : (
                                    <CodeIcon className="technology-icon" />
                                )}
                                <h2 className="technology-feature-title">
                                    {data?.isLoading ? <Skeleton variant="text" width="250px" /> : technology?.title1}
                                </h2>
                            </Animated>
                            <Animated dataAos="fade-up">
                                <Typography component="span" className="technology-feature-text">
                                    {data?.isLoading ? (
                                        <Skeleton variant="text" />
                                    ) : (
                                        <ReactHtmlParser html={technology?.text1} />
                                    )}
                                </Typography>
                            </Animated>
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <Animated dataAos="fade-up" className="flex-centre mb-15">
                                {data?.isLoading ? (
                                    <Skeleton variant="circle" className="technology-skeleton-circle" />
                                ) : (
                                    <LibraryBooksIcon className="technology-icon" />
                                )}
                                <h2 className="technology-feature-title">
                                    {data?.isLoading ? <Skeleton variant="text" width="250px" /> : technology?.title2}
                                </h2>
                            </Animated>
                            <Animated dataAos="fade-up">
                                <Typography component="span" className="technology-feature-text">
                                    {data?.isLoading ? (
                                        <Skeleton variant="text" />
                                    ) : (
                                        <ReactHtmlParser html={technology?.text2} />
                                    )}
                                </Typography>
                            </Animated>
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <Animated dataAos="fade-up" className="flex-centre mb-15">
                                {data?.isLoading ? (
                                    <Skeleton variant="circle" className="technology-skeleton-circle" />
                                ) : (
                                    <StorageIcon className="technology-icon" />
                                )}
                                <h2 className="technology-feature-title">
                                    {data?.isLoading ? <Skeleton variant="text" width="250px" /> : technology?.title3}
                                </h2>
                            </Animated>
                            <Animated dataAos="fade-up">
                                <Typography component="span" className="technology-feature-text">
                                    {data?.isLoading ? (
                                        <Skeleton variant="text" />
                                    ) : (
                                        <ReactHtmlParser html={technology?.text3} />
                                    )}
                                </Typography>
                            </Animated>
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <Animated dataAos="fade-up" className="flex-centre mb-15">
                                {data?.isLoading ? (
                                    <Skeleton variant="circle" className="technology-skeleton-circle" />
                                ) : (
                                    <CloudIcon color="primary" className="technology-icon" />
                                )}
                                <h2 className="technology-feature-title">
                                    {data?.isLoading ? <Skeleton variant="text" width="250px" /> : technology?.title4}
                                </h2>
                            </Animated>
                            <Animated dataAos="fade-up">
                                <Typography component="span" className="technology-feature-text">
                                    {data?.isLoading ? (
                                        <Skeleton variant="text" />
                                    ) : (
                                        <ReactHtmlParser html={technology?.text4} />
                                    )}
                                </Typography>
                            </Animated>
                        </Grid>
                    </Grid>
                </div>
            </Container>
        </section>
    );
};
