import * as React from "react";
import { useSelector } from "react-redux";
import { Link } from "react-router-dom";
import { GET_SHOWCASE_IMAGE_URL } from "../../../Api";
import { FeatureShowcaseContentDto } from "../../../Api/Models";
import { ApplicationState } from "../../../Store/Configuration";
import { Animated, CustomImage, RenderHtml, Skeleton } from "../../../Shared/Components";
import Validate from "validate.js";
import "./showcaseView.css";

interface ShowcaseViewProps {
    className?: string;
}

interface ActiveButtonProps extends FeatureShowcaseContentDto {
    isLoading: boolean;
}

const ActiveButton = (props: ActiveButtonProps): React.ReactElement => {
    if (Validate.isEmpty(props?.action?.href)) {
        return <button className="bulma-button bulma-is-link bulma-is-light">{props?.action?.text}</button>;
    }

    return (
        <Link to={props?.action?.href ?? ""} className="link" rel="noopener nofollow">
            <button className="bulma-button bulma-is-link bulma-is-light">{props?.action?.text}</button>
        </Link>
    );
};

export const ShowcaseView = (props: ShowcaseViewProps): React.ReactElement => {
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const isLoading = data?.isLoading;
    const showcase = data?.components?.sectionShowcase;

    return (
        <section className={props.className}>
            <div className="bulma-container">
                <div className="py-6">
                    <Animated dataAos="fade-down">
                        <Skeleton isLoading={isLoading} mode="Text" height={40}>
                            <p className="is-size-3	has-text-centered has-text-link">
                                {showcase?.caption?.toUpperCase()}
                            </p>
                        </Skeleton>
                    </Animated>
                    <Animated dataAos="fade-up">
                        <div className="bulma-columns showcase-feature-columns">
                            <div className="bulma-column p-0 is-flex is-flex-direction-column is-align-self-center">
                                <Skeleton isLoading={isLoading} mode="Text">
                                    <h2 className="is-size-3 py-5 has-text-black">{showcase?.heading}</h2>
                                </Skeleton>
                                <Skeleton isLoading={isLoading} mode="Text">
                                    <RenderHtml
                                        value={showcase?.text}
                                        tag="p"
                                        className="is-size-5 py-3 has-text-grey line-height-18 showcase-feature-text"
                                    />
                                </Skeleton>
                                <Skeleton isLoading={isLoading} mode="Rect">
                                    <div className="has-text-left py-5">
                                        <ActiveButton isLoading={isLoading} {...showcase} />
                                    </div>
                                </Skeleton>
                            </div>
                            <div className="bulma-column p-0">
                                <div className="bulma-card">
                                    <div className="bulma-card-image">
                                        <figure className="bulma-image">
                                            <Skeleton isLoading={isLoading} mode="Rect" height={400}>
                                                <CustomImage
                                                    base={GET_SHOWCASE_IMAGE_URL}
                                                    source={showcase?.image}
                                                    className="showcase-feature-image"
                                                    title="Illustration"
                                                    alt="An image illustrating showcase section"
                                                />
                                            </Skeleton>
                                        </figure>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </Animated>
                </div>
            </div>
        </section>
    );
};
