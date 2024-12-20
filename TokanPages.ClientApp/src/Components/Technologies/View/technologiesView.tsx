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

interface TechnologiesViewProps {
    background?: React.CSSProperties;
}

export const TechnologiesView = (props: TechnologiesViewProps): React.ReactElement => {
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const technology = data?.components?.technologies;

    return (
        <section className="section-grey" style={props.background}>
            <Container className="contaoner-super-wide">
                <div style={{ paddingTop: 64, paddingBottom: 64 }}>
                    <div data-aos="fade-down" style={{ marginBottom: 64 }}>
                        <Typography className="technology-caption-text">
                            {data?.isLoading ? <Skeleton variant="text" /> : technology?.caption?.toUpperCase()}
                        </Typography>
                    </div>
                    <Grid container spacing={6}>
                        <Grid item xs={12} sm={6}>
                            <div data-aos="fade-up" style={{ display: "flex", alignItems: "center", marginBottom: 16 }}>
                                {data?.isLoading ? (
                                    <Skeleton variant="circle" className="technology-skeleton-circle" />
                                ) : (
                                    <CodeIcon className="technology-icon" />
                                )}
                                <Typography className="technology-feature-title">
                                    {data?.isLoading ? <Skeleton variant="text" width="250px" /> : technology?.title1}
                                </Typography>
                            </div>
                            <Typography component="span" className="technology-feature-text" data-aos="fade-up">
                                {data?.isLoading ? (
                                    <Skeleton variant="text" />
                                ) : (
                                    <ReactHtmlParser html={technology?.text1} />
                                )}
                            </Typography>
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <div data-aos="fade-up" style={{ display: "flex", alignItems: "center", marginBottom: 16 }}>
                                {data?.isLoading ? (
                                    <Skeleton variant="circle" className="technology-skeleton-circle" />
                                ) : (
                                    <LibraryBooksIcon className="technology-icon" />
                                )}
                                <Typography className="technology-feature-title">
                                    {data?.isLoading ? <Skeleton variant="text" width="250px" /> : technology?.title2}
                                </Typography>
                            </div>
                            <Typography component="span" className="technology-feature-text" data-aos="fade-up">
                                {data?.isLoading ? (
                                    <Skeleton variant="text" />
                                ) : (
                                    <ReactHtmlParser html={technology?.text2} />
                                )}
                            </Typography>
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <div data-aos="fade-up" style={{ display: "flex", alignItems: "center", marginBottom: 16 }}>
                                {data?.isLoading ? (
                                    <Skeleton variant="circle" className="technology-skeleton-circle" />
                                ) : (
                                    <StorageIcon className="technology-icon" />
                                )}
                                <Typography className="technology-feature-title">
                                    {data?.isLoading ? <Skeleton variant="text" width="250px" /> : technology?.title3}
                                </Typography>
                            </div>
                            <Typography component="span" className="technology-feature-text" data-aos="fade-up">
                                {data?.isLoading ? (
                                    <Skeleton variant="text" />
                                ) : (
                                    <ReactHtmlParser html={technology?.text3} />
                                )}
                            </Typography>
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <div data-aos="fade-up" style={{ display: "flex", alignItems: "center", marginBottom: 16 }}>
                                {data?.isLoading ? (
                                    <Skeleton variant="circle" className="technology-skeleton-circle" />
                                ) : (
                                    <CloudIcon color="primary" className="technology-icon" />
                                )}
                                <Typography className="technology-feature-title">
                                    {data?.isLoading ? <Skeleton variant="text" width="250px" /> : technology?.title4}
                                </Typography>
                            </div>
                            <Typography component="span" className="technology-feature-text" data-aos="fade-up">
                                {data?.isLoading ? (
                                    <Skeleton variant="text" />
                                ) : (
                                    <ReactHtmlParser html={technology?.text4} />
                                )}
                            </Typography>
                        </Grid>
                    </Grid>
                </div>
            </Container>
        </section>
    );
};
