import * as React from "react";
import { useSelector } from "react-redux";
import { GET_IMAGES_URL } from "../../../Api";
import { ApplicationState } from "../../../Store/Configuration";
import { Animated, Icon, Link, Image, Skeleton } from "../../../Shared/Components";
import "./socialsView.css";

interface SocialsViewProps {
    className?: string;
}

export const SocialsView = (props: SocialsViewProps): React.ReactElement => {
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const isLoading = data?.isLoading;
    const socials = data?.components?.sectionSocials;

    return (
        <section className={props.className}>
            <div className="bulma-container">
                <div className="py-6">
                    <Animated dataAos="fade-down">
                        <Skeleton isLoading={isLoading} mode="Text" height={40}>
                            <p className="is-size-3	has-text-centered has-text-link">
                                {socials?.caption?.toUpperCase()}
                            </p>
                        </Skeleton>
                    </Animated>
                    <div className="bulma-columns socials-card-columns">
                        <div className="bulma-column is-clickable socials-card-column">
                            <Animated dataAos="fade-up" dataAosDelay={350}>
                                <div className="bulma-card">
                                    <Link to={socials?.social1?.action?.href}>
                                        <div className="bulma-card-image">
                                            <figure className="bulma-image">
                                                <Skeleton isLoading={isLoading} mode="Rect" className="socials-image">
                                                    <Image
                                                        base={GET_IMAGES_URL}
                                                        source={socials?.social1?.images?.header}
                                                        className="socials-image"
                                                        title="Illustration"
                                                        alt={socials?.social1?.textTitle}
                                                    />
                                                </Skeleton>
                                            </figure>
                                        </div>
                                        <div className="socials-card-image-holder">
                                            <Skeleton isLoading={isLoading} mode="Rect" className="socials-avatar">
                                                <Image
                                                    base={GET_IMAGES_URL}
                                                    source={socials?.social1?.images?.avatar}
                                                    className="socials-avatar"
                                                    title="Socials"
                                                    alt={socials?.social1?.textTitle}
                                                />
                                            </Skeleton>
                                        </div>
                                        <div className="bulma-card-content socials-card-content">
                                            <div className="is-flex is-justify-content-center pt-6">
                                                <Skeleton isLoading={isLoading} mode="Rect" height={24}>
                                                    <figure className="bulma-image bulma-is-32x32">
                                                        <Icon name={socials?.social1?.images?.icon} size={2} />
                                                    </figure>
                                                </Skeleton>
                                            </div>
                                            <div className="is-size-5 has-text-dark has-text-weight-semibold has-text-centered py-2">
                                                <Skeleton isLoading={isLoading} mode="Text" disableMarginY>
                                                    {socials?.social1?.textTitle}
                                                </Skeleton>
                                            </div>
                                            <div className="is-align-content-center socials-text">
                                                <Skeleton isLoading={isLoading} mode="Text" disableMarginY>
                                                    <p className="is-size-6 has-text-grey has-text-centered py-2">
                                                        {socials?.social1?.textSubtitle}
                                                    </p>
                                                </Skeleton>
                                            </div>
                                            <Skeleton isLoading={isLoading} mode="Text" disableMarginY>
                                                <p className="is-size-6 has-text-dark has-text-weight-semibold has-text-centered py-2">
                                                    {socials?.social1?.textComment}
                                                </p>
                                            </Skeleton>
                                        </div>
                                    </Link>
                                </div>
                            </Animated>
                        </div>
                        <div className="bulma-column is-clickable socials-card-column">
                            <Animated dataAos="fade-up" dataAosDelay={150}>
                                <div className="bulma-card">
                                    <Link to={socials?.social2?.action?.href}>
                                        <div className="bulma-card-image">
                                            <figure className="bulma-image">
                                                <Skeleton isLoading={isLoading} mode="Rect" className="socials-image">
                                                    <Image
                                                        base={GET_IMAGES_URL}
                                                        source={socials?.social2?.images?.header}
                                                        className="socials-image"
                                                        title="Illustration"
                                                        alt={socials?.social2?.textTitle}
                                                    />
                                                </Skeleton>
                                            </figure>
                                        </div>
                                        <div className="socials-card-image-holder">
                                            <Skeleton isLoading={isLoading} mode="Rect" className="socials-avatar">
                                                <Image
                                                    base={GET_IMAGES_URL}
                                                    source={socials?.social2?.images?.avatar}
                                                    className="socials-avatar"
                                                    title="Socials"
                                                    alt={socials?.social2?.textTitle}
                                                />
                                            </Skeleton>
                                        </div>
                                        <div className="bulma-card-content socials-card-content">
                                            <div className="is-flex is-justify-content-center pt-6">
                                                <Skeleton isLoading={isLoading} mode="Rect" height={24}>
                                                    <figure className="bulma-image bulma-is-32x32">
                                                        <Icon name={socials?.social2?.images?.icon} size={2} />
                                                    </figure>
                                                </Skeleton>
                                            </div>
                                            <div className="is-size-5 has-text-dark has-text-weight-semibold has-text-centered py-2">
                                                <Skeleton isLoading={isLoading} mode="Text" disableMarginY>
                                                    {socials?.social2?.textTitle}
                                                </Skeleton>
                                            </div>
                                            <div className="is-align-content-center socials-text">
                                                <Skeleton isLoading={isLoading} mode="Text" disableMarginY>
                                                    <p className="is-size-6 has-text-grey has-text-centered py-2">
                                                        {socials?.social2?.textSubtitle}
                                                    </p>
                                                </Skeleton>
                                            </div>
                                            <Skeleton isLoading={isLoading} mode="Text" disableMarginY>
                                                <p className="is-size-6 has-text-dark has-text-weight-semibold has-text-centered py-2">
                                                    {socials?.social2?.textComment}
                                                </p>
                                            </Skeleton>
                                        </div>
                                    </Link>
                                </div>
                            </Animated>
                        </div>
                        <div className="bulma-column is-clickable socials-card-column">
                            <Animated dataAos="fade-up" dataAosDelay={250}>
                                <div className="bulma-card">
                                    <Link to={socials?.social3?.action?.href}>
                                        <div className="bulma-card-image">
                                            <figure className="bulma-image">
                                                <Skeleton isLoading={isLoading} mode="Rect" className="socials-image">
                                                    <Image
                                                        base={GET_IMAGES_URL}
                                                        source={socials?.social3?.images?.header}
                                                        className="socials-image"
                                                        title="Illustration"
                                                        alt={socials?.social3?.textTitle}
                                                    />
                                                </Skeleton>
                                            </figure>
                                        </div>
                                        <div className="socials-card-image-holder">
                                            <Skeleton isLoading={isLoading} mode="Rect" className="socials-avatar">
                                                <Image
                                                    base={GET_IMAGES_URL}
                                                    source={socials?.social3?.images?.avatar}
                                                    className="socials-avatar"
                                                    title="Socials"
                                                    alt={socials?.social3?.textTitle}
                                                />
                                            </Skeleton>
                                        </div>
                                        <div className="bulma-card-content socials-card-content">
                                            <div className="is-flex is-justify-content-center pt-6">
                                                <Skeleton isLoading={isLoading} mode="Rect" height={24}>
                                                    <figure className="bulma-image bulma-is-32x32">
                                                        <Icon name={socials?.social3?.images?.icon} size={2} />
                                                    </figure>
                                                </Skeleton>
                                            </div>
                                            <div className="is-size-5 has-text-dark has-text-weight-semibold has-text-centered py-2">
                                                <Skeleton isLoading={isLoading} mode="Text" disableMarginY>
                                                    {socials?.social3?.textTitle}
                                                </Skeleton>
                                            </div>
                                            <div className="is-align-content-center socials-text">
                                                <Skeleton isLoading={isLoading} mode="Text" disableMarginY>
                                                    <p className="is-size-6 has-text-grey has-text-centered py-2">
                                                        {socials?.social3?.textSubtitle}
                                                    </p>
                                                </Skeleton>
                                            </div>
                                            <Skeleton isLoading={isLoading} mode="Text" disableMarginY>
                                                <p className="is-size-6 has-text-dark has-text-weight-semibold has-text-centered py-2">
                                                    {socials?.social3?.textComment}
                                                </p>
                                            </Skeleton>
                                        </div>
                                    </Link>
                                </div>
                            </Animated>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};
