import * as React from "react";
import { useSelector } from "react-redux";
import { Link } from "react-router-dom";
import { GET_ARTICLE_IMAGE_URL } from "../../../../Api";
import { ArticleFeaturesContentDto } from "../../../../Api/Models";
import { ApplicationState } from "../../../../Store/Configuration";
import { Animated, CustomImage, Skeleton } from "../../../../Shared/Components";
import { useDimensions } from "../../../../Shared/Hooks";
import Validate from "validate.js";
import "./articleFeatureView.css";

interface ArticleFeatureViewProps {
    className?: string;
}

const ActiveButton = (props: ArticleFeaturesContentDto): React.ReactElement => {
    if (Validate.isEmpty(props?.action?.href)) {
        return <button className="bulma-button bulma-is-link bulma-is-light">{props?.action?.text}</button>;
    }

    return (
        <Link to={props?.action?.href ?? ""} className="link" rel="noopener nofollow">
            <button className="bulma-button bulma-is-link bulma-is-light">{props?.action?.text}</button>
        </Link>
    );
};

export const ArticleFeatureView = (props: ArticleFeatureViewProps): React.ReactElement => {
    const media = useDimensions();
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const isLoading = data?.isLoading;
    const features = data?.components?.sectionArticle;
    const baseClass = "bulma-columns bulma-is-3 is-flex-direction-row";

    return (
        <section className={props.className}>
            <div className="bulma-container">
                <div className="py-6">
                    <Animated dataAos="fade-down">
                        <Skeleton isLoading={isLoading} mode="Text" height={40}>
                            <p className="is-size-3	has-text-centered has-text-link">
                                {features?.caption.toUpperCase()}
                            </p>
                        </Skeleton>
                    </Animated>
                    <Animated dataAos="fade-up">
                        <div className={`${baseClass} ${media.isMobile ? "p-4" : "p-6"}`}>
                            <Skeleton isLoading={isLoading} mode="Rect" width={200} height={200}>
                                <div className="bulma-column">
                                    <div
                                        className={`bulma-columns bulma-is-3 ${media.isMobile ? "do-not-display" : ""}`}
                                    >
                                        <div className="bulma-column bulma-is-three-quarters">
                                            <div className="bulma-card article-feature-card-shadow">
                                                <div className="bulma-card-image">
                                                    <figure className="bulma-image">
                                                        <CustomImage
                                                            base={GET_ARTICLE_IMAGE_URL}
                                                            source={features?.image1}
                                                            className="article-feature-image article-feature-image-large"
                                                            title="Illustration"
                                                            alt="An image illustrating listed features"
                                                        />
                                                    </figure>
                                                </div>
                                            </div>
                                        </div>
                                        <div className="bulma-column is-flex is-align-self-flex-end">
                                            <div className="bulma-card article-feature-card-shadow">
                                                <div className="bulma-card-image">
                                                    <figure className="bulma-image">
                                                        <CustomImage
                                                            base={GET_ARTICLE_IMAGE_URL}
                                                            source={features?.image2}
                                                            className="article-feature-image article-feature-image-small"
                                                            title="Illustration"
                                                            alt="An image illustrating listed features"
                                                        />
                                                    </figure>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div className="bulma-columns bulma-is-3">
                                        <div className="bulma-column is-flex is-align-self-flex-start">
                                            <div
                                                className={`bulma-card article-feature-card-shadow ${media.isMobile ? "do-not-display" : ""}`}
                                            >
                                                <div className="bulma-card-image">
                                                    <figure className="bulma-image">
                                                        <CustomImage
                                                            base={GET_ARTICLE_IMAGE_URL}
                                                            source={features?.image3}
                                                            className="article-feature-image article-feature-image-small"
                                                        />
                                                    </figure>
                                                </div>
                                            </div>
                                        </div>
                                        <div className="bulma-column bulma-is-three-quarters">
                                            <div className="bulma-card article-feature-card-shadow">
                                                <div className="bulma-card-image">
                                                    <figure className="bulma-image">
                                                        <CustomImage
                                                            base={GET_ARTICLE_IMAGE_URL}
                                                            source={features?.image4}
                                                            className="article-feature-image article-feature-image-large"
                                                            title="Illustration"
                                                            alt="An image illustrating listed features"
                                                        />
                                                    </figure>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </Skeleton>
                            <div className={`bulma-column is-align-self-center ${media.isMobile ? "p-4" : "p-6"}`}>
                                <Skeleton isLoading={isLoading} mode="Text">
                                    <h2 className="is-size-3 py-5 has-text-black">{features?.title}</h2>
                                </Skeleton>
                                <Skeleton isLoading={isLoading} mode="Text">
                                    <p className="is-size-5 py-3 has-text-grey line-height-18">
                                        {features?.description}
                                    </p>
                                </Skeleton>
                                <Skeleton isLoading={isLoading} mode="Text">
                                    <p className="is-size-5 py-3 has-text-grey line-height-18">{features?.text}</p>
                                </Skeleton>
                                <div className="has-text-left py-5">
                                    <Skeleton isLoading={isLoading} mode="Rect">
                                        <ActiveButton {...features} />
                                    </Skeleton>
                                </div>
                            </div>
                        </div>
                    </Animated>
                </div>
            </div>
        </section>
    );
};
