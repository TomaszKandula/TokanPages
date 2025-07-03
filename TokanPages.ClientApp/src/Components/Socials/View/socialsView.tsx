import * as React from "react";
import { useSelector } from "react-redux";
import { GET_SOCIALS_URL } from "../../../Api";
import { ApplicationState } from "../../../Store/Configuration";
import { Animated, Icon, Link, RenderImage } from "../../../Shared/Components";
import "./socialsView.css";

interface SocialsViewProps {
    className?: string;
}

export const SocialsView = (props: SocialsViewProps): React.ReactElement => {
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const socials = data?.components?.sectionSocials;

    return (
        <section className={`section ${props.className ?? ""}`}>
            <div className="bulma-container">
                <div className="py-6">
                    <Animated dataAos="fade-down">
                        <p className="is-size-3	has-text-centered has-text-link">
                            {socials?.caption?.toUpperCase()}
                        </p>
                    </Animated>
                    <div className="p-6">
                        <div className="bulma-columns">
                            <div className="bulma-column is-clickable">
                                <Animated dataAos="fade-up" dataAosDelay={350}>
                                    <div className="bulma-card">
                                        <Link to={socials?.social1?.action?.href}>
                                            <div className="bulma-card-image">
                                                <figure className="bulma-image">
                                                    <RenderImage
                                                        base={GET_SOCIALS_URL}
                                                        source={socials?.social1?.images?.header}
                                                        className="socials-image"
                                                        title="Illustration"
                                                        alt={socials?.social1?.textTitle}
                                                    />
                                                </figure>
                                            </div>
                                            <div className="socials-card-image-holder">
                                                <RenderImage
                                                    base={GET_SOCIALS_URL}
                                                    source={socials?.social1?.images?.avatar}
                                                    className="socials-avatar"
                                                    title="Socials"
                                                    alt={socials?.social1?.textTitle}
                                                />
                                            </div>
                                            <div className="bulma-card-content">
                                                <div className="has-text-centered pt-6">
                                                    <Icon name={socials?.social1?.images?.icon} size={2} />
                                                </div>
                                                <div className="is-size-5 has-text-dark has-text-weight-semibold has-text-centered py-2">
                                                    {socials?.social1?.textTitle}
                                                </div>
                                                <h3 className="is-size-6 has-text-grey has-text-centered py-2">
                                                    {socials?.social1?.textSubtitle}
                                                </h3>
                                                <h4 className="is-size-6 has-text-dark has-text-weight-semibold has-text-centered py-2">
                                                    {socials?.social1?.textComment}
                                                </h4>
                                            </div>
                                        </Link>
                                    </div>
                                </Animated>
                            </div>
                            <div className="bulma-column is-clickable">
                                <Animated dataAos="fade-up" dataAosDelay={150}>
                                    <div className="bulma-card">
                                        <Link to={socials?.social2?.action?.href}>
                                            <div className="bulma-card-image">
                                                <figure className="bulma-image">
                                                    <RenderImage
                                                        base={GET_SOCIALS_URL}
                                                        source={socials?.social2?.images?.header}
                                                        className="socials-image"
                                                        title="Illustration"
                                                        alt={socials?.social2?.textTitle}
                                                    />
                                                </figure>
                                            </div>
                                            <div className="socials-card-image-holder">
                                                <RenderImage
                                                    base={GET_SOCIALS_URL}
                                                    source={socials?.social2?.images?.avatar}
                                                    className="socials-avatar"
                                                    title="Socials"
                                                    alt={socials?.social2?.textTitle}
                                                />
                                            </div>
                                            <div className="bulma-card-content">
                                                <div className="has-text-centered pt-6">
                                                    <Icon name={socials?.social2?.images?.icon} size={2} />
                                                </div>
                                                <div className="is-size-5 has-text-dark has-text-weight-semibold has-text-centered py-2">
                                                    {socials?.social2?.textTitle}
                                                </div>
                                                <h3 className="is-size-6 has-text-grey has-text-centered py-2">
                                                    {socials?.social2?.textSubtitle}
                                                </h3>
                                                <h4 className="is-size-6 has-text-dark has-text-weight-semibold has-text-centered py-2">
                                                    {socials?.social2?.textComment}
                                                </h4>
                                            </div>
                                        </Link>
                                    </div>
                                </Animated>
                            </div>
                            <div className="bulma-column is-clickable">
                                <Animated dataAos="fade-up" dataAosDelay={250}>
                                    <div className="bulma-card">
                                        <Link to={socials?.social3?.action?.href}>
                                            <div className="bulma-card-image">
                                                <figure className="bulma-image">
                                                    <RenderImage
                                                        base={GET_SOCIALS_URL}
                                                        source={socials?.social3?.images?.header}
                                                        className="socials-image"
                                                        title="Illustration"
                                                        alt={socials?.social3?.textTitle}
                                                    />
                                                </figure>
                                            </div>
                                            <div className="socials-card-image-holder">
                                                <RenderImage
                                                    base={GET_SOCIALS_URL}
                                                    source={socials?.social3?.images?.avatar}
                                                    className="socials-avatar"
                                                    title="Socials"
                                                    alt={socials?.social3?.textTitle}
                                                />
                                            </div>
                                            <div className="bulma-card-content">
                                                <div className="has-text-centered pt-6">
                                                    <Icon name={socials?.social3?.images?.icon} size={2} />
                                                </div>
                                                <div className="is-size-5 has-text-dark has-text-weight-semibold has-text-centered py-2">
                                                    {socials?.social3?.textTitle}
                                                </div>
                                                <h3 className="is-size-6 has-text-grey has-text-centered py-2">
                                                    {socials?.social3?.textSubtitle}
                                                </h3>
                                                <h4 className="is-size-6 has-text-dark has-text-weight-semibold has-text-centered py-2">
                                                    {socials?.social3?.textComment}
                                                </h4>
                                            </div>
                                        </Link>
                                    </div>
                                </Animated>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};
