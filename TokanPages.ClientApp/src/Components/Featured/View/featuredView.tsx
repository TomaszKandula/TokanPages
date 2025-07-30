import * as React from "react";
import { useSelector } from "react-redux";
import { GET_FEATURED_IMAGE_URL } from "../../../Api";
import { ApplicationState } from "../../../Store/Configuration";
import { Animated, Link, CustomImage, Skeleton } from "../../../Shared/Components";
import { useDimensions } from "../../../Shared/Hooks";
import "./featuredView.css";

interface FeaturedViewProps {
    className?: string;
}

export const FeaturedView = (props: FeaturedViewProps): React.ReactElement => {
    const media = useDimensions();
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const isLoading = data?.isLoading;
    const featured = data?.components?.sectionFeatured;

    return (
        <section className={props.className}>
            <div className="bulma-container">
                <div className="py-6">
                    <Animated dataAos="fade-down">
                        <Skeleton isLoading={isLoading} mode="Rect" height={40}>
                            <p className="is-size-3	has-text-centered has-text-link">
                                {featured?.caption?.toUpperCase()}
                            </p>
                        </Skeleton>
                    </Animated>
                    <div className={`bulma-columns ${media.isMobile ? "m-2" : "m-6"}`}>
                        <div className={`bulma-column is-clickable ${media.isMobile ? "mt-6" : ""}`}>
                            <Animated dataAos="fade-up" dataAosDelay={350}>
                                <Link to={featured?.link1}>
                                    <div className="bulma-card">
                                        <div className="bulma-card-image">
                                            <figure className="bulma-image">
                                                <Skeleton
                                                    isLoading={isLoading}
                                                    mode="Rect"
                                                    className="featured-card-image"
                                                >
                                                    <CustomImage
                                                        base={GET_FEATURED_IMAGE_URL}
                                                        source={featured?.image1}
                                                        className="featured-card-image"
                                                        title="Illustration"
                                                        alt={featured?.title1}
                                                    />
                                                </Skeleton>
                                            </figure>
                                        </div>
                                        <div className="bulma-card-content featured-card-height is-align-content-center">
                                            <Skeleton isLoading={isLoading} mode="Text" height={24} disableMarginY>
                                                <h2 className="is-size-4 has-text-weight-semibold has-text-centered">
                                                    {featured?.title1}
                                                </h2>
                                            </Skeleton>
                                            <Skeleton isLoading={isLoading} mode="Text" height={24}>
                                                <p className="is-size-5 has-text-grey-light has-text-centered">
                                                    {featured?.subtitle1}
                                                </p>
                                            </Skeleton>
                                        </div>
                                    </div>
                                </Link>
                            </Animated>
                        </div>
                        <div className={`bulma-column is-clickable ${media.isMobile ? "mt-6" : ""}`}>
                            <Animated dataAos="fade-up" dataAosDelay={150}>
                                <Link to={featured?.link2}>
                                    <div className="bulma-card">
                                        <div className="bulma-card-image">
                                            <figure className="bulma-image">
                                                <Skeleton
                                                    isLoading={isLoading}
                                                    mode="Rect"
                                                    className="featured-card-image"
                                                >
                                                    <CustomImage
                                                        base={GET_FEATURED_IMAGE_URL}
                                                        source={featured?.image2}
                                                        className="featured-card-image"
                                                        title="Illustration"
                                                        alt={featured?.title2}
                                                    />
                                                </Skeleton>
                                            </figure>
                                        </div>
                                        <div className="bulma-card-content featured-card-height is-align-content-center">
                                            <Skeleton isLoading={isLoading} mode="Text" height={24} disableMarginY>
                                                <h2 className="is-size-4 has-text-weight-semibold has-text-centered">
                                                    {featured?.title2}
                                                </h2>
                                            </Skeleton>
                                            <Skeleton isLoading={isLoading} mode="Text" height={24}>
                                                <p className="is-size-5 has-text-grey-light has-text-centered">
                                                    {featured?.subtitle2}
                                                </p>
                                            </Skeleton>
                                        </div>
                                    </div>
                                </Link>
                            </Animated>
                        </div>
                        <div className={`bulma-column is-clickable ${media.isMobile ? "mt-6" : ""}`}>
                            <Animated dataAos="fade-up" dataAosDelay={550}>
                                <Link to={featured?.link3}>
                                    <div className="bulma-card">
                                        <div className="bulma-card-image">
                                            <figure className="bulma-image">
                                                <Skeleton
                                                    isLoading={isLoading}
                                                    mode="Rect"
                                                    className="featured-card-image"
                                                >
                                                    <CustomImage
                                                        base={GET_FEATURED_IMAGE_URL}
                                                        source={featured?.image3}
                                                        className="featured-card-image"
                                                        title="Illustration"
                                                        alt={featured?.title3}
                                                    />
                                                </Skeleton>
                                            </figure>
                                        </div>
                                        <div className="bulma-card-content featured-card-height is-align-content-center">
                                            <Skeleton isLoading={isLoading} mode="Text" height={24} disableMarginY>
                                                <h2 className="is-size-4 has-text-weight-semibold has-text-centered">
                                                    {featured?.title3}
                                                </h2>
                                            </Skeleton>
                                            <Skeleton isLoading={isLoading} mode="Text" height={24}>
                                                <p className="is-size-5 has-text-grey-light has-text-centered">
                                                    {featured?.subtitle3}
                                                </p>
                                            </Skeleton>
                                        </div>
                                    </div>
                                </Link>
                            </Animated>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};
