import * as React from "react";
import { useSelector } from "react-redux";
import { Link } from "react-router-dom";
import { GET_SHOWCASE_IMAGE_URL } from "../../../Api";
import { FeatureShowcaseContentDto } from "../../../Api/Models";
import { ApplicationState } from "../../../Store/Configuration";
import { Animated, RenderImage } from "../../../Shared/Components";
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
        return (
            <button className="bulma-button bulma-is-link bulma-is-light">
                {props?.action?.text}
            </button>
        );
    }

    return (
        <Link to={props?.action?.href ?? ""} className="link" rel="noopener nofollow">
            <button className="bulma-button bulma-is-link bulma-is-light">
                {props?.action?.text}
            </button>
        </Link>
    );
};

export const ShowcaseView = (props: ShowcaseViewProps): React.ReactElement => {
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const isLoading = data?.isLoading;
    const showcase = data?.components?.sectionShowcase;

    return (
        <section className={`section ${props.className ?? ""}`}>
            <div className="bulma-container">
                <div className="py-6">
                    <Animated dataAos="fade-down">
                        <p className="is-size-3	has-text-centered has-text-link">
                            {showcase?.caption?.toUpperCase()}
                        </p>
                    </Animated>
                    <Animated dataAos="fade-up">
                        <div className="bulma-columns p-6">
                            <div className="bulma-column is-flex is-align-items-center">
                                <div className="">
                                    <h2 className="is-size-3 py-5 has-text-black">
                                        {showcase?.heading}
                                    </h2>
                                    <p className="is-size-5 py-3 has-text-grey line-height-18">
                                        {showcase?.text}
                                    </p>
                                    <div className="has-text-left py-5">
                                        <ActiveButton isLoading={isLoading} {...showcase} />
                                    </div>
                                </div>
                            </div>
                            <div className="bulma-column">
                                <div className="bulma-card">
                                    <div className="bulma-card-image">
                                        <figure className="bulma-image">
                                            <RenderImage
                                                base={GET_SHOWCASE_IMAGE_URL}
                                                source={showcase?.image}
                                                className="showcase-feature-image"
                                            />
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
