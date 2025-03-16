import * as React from "react";
import { useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import Grid from "@material-ui/core/Grid";
import CodeIcon from "@material-ui/icons/Code";
import LibraryBooksIcon from "@material-ui/icons/LibraryBooks";
import StorageIcon from "@material-ui/icons/Storage";
import CloudIcon from "@material-ui/icons/Cloud";
import Skeleton from "@material-ui/lab/Skeleton";
import { Typography } from "@material-ui/core";
import { ApplicationState } from "../../../Store/Configuration";
import { Animated } from "../../../Shared/Components";

interface TechnologiesViewProps {
    background?: string;
}

export const TechnologiesView = (props: TechnologiesViewProps): React.ReactElement => {
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const technology = data?.components?.sectionTechnologies;

    return (
        <section className={`section ${props.background ?? ""}`}>
            <Container className="container-super-wide">
                <div className="technology-box">
                    <Animated dataAos="fade-down" className="mb-64">
                        <Typography className="technology-caption-text">
                            {data?.isLoading ? <Skeleton variant="text" /> : technology?.caption?.toUpperCase()}
                        </Typography>
                    </Animated>
                    <Grid container spacing={6}>
                        <Grid item xs={12} sm={6}>
                            <Animated dataAos="fade-up" className="flex-centre mb-15">
                                {data?.isLoading ? (
                                    <Skeleton variant="circle" className="technology-skeleton-circle" />
                                ) : (
                                    <CodeIcon className="technology-icon" />
                                )}
                                <h3 className="technology-feature-title">
                                    {data?.isLoading ? <Skeleton variant="text" width="250px" /> : technology?.title1}
                                </h3>
                            </Animated>
                            <Animated dataAos="fade-up">
                                {data?.isLoading ? (
                                    <Skeleton variant="text" />
                                ) : (
                                    <h4 className="technology-feature-text">{technology?.text1}</h4>
                                )}
                            </Animated>
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <Animated dataAos="fade-up" className="flex-centre mb-15">
                                {data?.isLoading ? (
                                    <Skeleton variant="circle" className="technology-skeleton-circle" />
                                ) : (
                                    <LibraryBooksIcon className="technology-icon" />
                                )}
                                <h3 className="technology-feature-title">
                                    {data?.isLoading ? <Skeleton variant="text" width="250px" /> : technology?.title2}
                                </h3>
                            </Animated>
                            <Animated dataAos="fade-up">
                                {data?.isLoading ? (
                                    <Skeleton variant="text" />
                                ) : (
                                    <h4 className="technology-feature-text">{technology?.text2}</h4>
                                )}
                            </Animated>
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <Animated dataAos="fade-up" className="flex-centre mb-15">
                                {data?.isLoading ? (
                                    <Skeleton variant="circle" className="technology-skeleton-circle" />
                                ) : (
                                    <StorageIcon className="technology-icon" />
                                )}
                                <h3 className="technology-feature-title">
                                    {data?.isLoading ? <Skeleton variant="text" width="250px" /> : technology?.title3}
                                </h3>
                            </Animated>
                            <Animated dataAos="fade-up">
                                {data?.isLoading ? (
                                    <Skeleton variant="text" />
                                ) : (
                                    <h4 className="technology-feature-text">{technology?.text3}</h4>
                                )}
                            </Animated>
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <Animated dataAos="fade-up" className="flex-centre mb-15">
                                {data?.isLoading ? (
                                    <Skeleton variant="circle" className="technology-skeleton-circle" />
                                ) : (
                                    <CloudIcon color="primary" className="technology-icon" />
                                )}
                                <h3 className="technology-feature-title">
                                    {data?.isLoading ? <Skeleton variant="text" width="250px" /> : technology?.title4}
                                </h3>
                            </Animated>
                            <Animated dataAos="fade-up">
                                {data?.isLoading ? (
                                    <Skeleton variant="text" />
                                ) : (
                                    <h4 className="technology-feature-text">{technology?.text4}</h4>
                                )}
                            </Animated>
                        </Grid>
                    </Grid>
                </div>
            </Container>
        </section>
    );
};
