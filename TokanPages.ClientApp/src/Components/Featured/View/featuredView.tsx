import * as React from "react";
import { useSelector } from "react-redux";
import { GET_FEATURED_IMAGE_URL } from "../../../Api";
import { ApplicationState } from "../../../Store/Configuration";
import { Animated, Link, CustomImage } from "../../../Shared/Components";
import { useDimensions } from "../../../Shared/Hooks";
import "./featuredView.css";

interface FeaturedViewProps {
    className?: string;
}

export const FeaturedView = (props: FeaturedViewProps): React.ReactElement => {
    const media = useDimensions();
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const featured = data?.components?.sectionFeatured;

    return (
        <section className={props.className}>
            <div className="bulma-container">
                <div className="py-6">
                    <Animated dataAos="fade-down">
                        <p className="is-size-3	has-text-centered has-text-link">{featured?.caption?.toUpperCase()}</p>
                    </Animated>
                    <div className={`bulma-columns ${media.isMobile ? "p-4" : "p-6"}`}>
                        <div className={`bulma-column is-clickable ${media.isMobile ? "mt-6" : ""}`}>
                            <Animated dataAos="fade-up" dataAosDelay={350}>
                                <Link to={featured?.link1}>
                                    <div className="bulma-card">
                                        <div className="bulma-card-image">
                                            <figure className="bulma-image">
                                                <CustomImage
                                                    base={GET_FEATURED_IMAGE_URL}
                                                    source={featured?.image1}
                                                    className="featured-card-image"
                                                    title="Illustration"
                                                    alt={featured?.title1}
                                                />
                                            </figure>
                                        </div>
                                        <div className="bulma-card-content py-6">
                                            <h2 className="is-size-4 has-text-weight-semibold has-text-centered">
                                                {featured?.title1}
                                            </h2>
                                            <p className="is-size-5 has-text-grey-light has-text-centered">
                                                {featured?.subtitle1}
                                            </p>
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
                                                <CustomImage
                                                    base={GET_FEATURED_IMAGE_URL}
                                                    source={featured?.image2}
                                                    className="featured-card-image"
                                                    title="Illustration"
                                                    alt={featured?.title2}
                                                />
                                            </figure>
                                        </div>
                                        <div className="bulma-card-content py-6">
                                            <h2 className="is-size-4 has-text-weight-semibold has-text-centered">
                                                {featured?.title2}
                                            </h2>
                                            <p className="is-size-5 has-text-grey-light has-text-centered">
                                                {featured?.subtitle2}
                                            </p>
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
                                                <CustomImage
                                                    base={GET_FEATURED_IMAGE_URL}
                                                    source={featured?.image3}
                                                    className="featured-card-image"
                                                    title="Illustration"
                                                    alt={featured?.title3}
                                                />
                                            </figure>
                                        </div>
                                        <div className="bulma-card-content py-6">
                                            <h2 className="is-size-4 has-text-weight-semibold has-text-centered">
                                                {featured?.title3}
                                            </h2>
                                            <p className="is-size-5 has-text-grey-light has-text-centered">
                                                {featured?.subtitle3}
                                            </p>
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
