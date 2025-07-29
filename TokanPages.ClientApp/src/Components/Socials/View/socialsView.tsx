import * as React from "react";
import { useSelector } from "react-redux";
import { GET_SOCIALS_URL } from "../../../Api";
import { ApplicationState } from "../../../Store/Configuration";
import { Animated, Icon, Link, CustomImage, Skeleton } from "../../../Shared/Components";
import { useDimensions } from "../../../Shared/Hooks";
import "./socialsView.css";

interface SocialsViewProps {
    className?: string;
}

export const SocialsView = (props: SocialsViewProps): React.ReactElement => {
    const media = useDimensions();
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const isLoading = data?.isLoading;
    const socials = data?.components?.sectionSocials;

    let paddings;
    if (media.isDesktop || media.isMobile) {
        paddings = "pt-4 pb-6";
    }

    if (media.isTablet) {
        paddings = "pt-4 pb-4 px-2";
    }

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
                    <div className={media.isMobile ? "p-4" : "p-6"}>
                        <div className="bulma-columns">
                            <div className={`bulma-column is-clickable ${media.isMobile ? "mt-6" : ""}`}>
                                <Animated dataAos="fade-up" dataAosDelay={350}>
                                    <div className="bulma-card">
                                        <Link to={socials?.social1?.action?.href}>
                                            <div className="bulma-card-image">
                                                <figure className="bulma-image">
                                                    <Skeleton
                                                        isLoading={isLoading}
                                                        mode="Rect"
                                                        className="socials-image"
                                                    >
                                                        <CustomImage
                                                            base={GET_SOCIALS_URL}
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
                                                    <CustomImage
                                                        base={GET_SOCIALS_URL}
                                                        source={socials?.social1?.images?.avatar}
                                                        className="socials-avatar"
                                                        title="Socials"
                                                        alt={socials?.social1?.textTitle}
                                                    />
                                                </Skeleton>
                                            </div>
                                            <div className={`bulma-card-content ${paddings}`}>
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
                                                <h3 className="is-size-6 has-text-grey has-text-centered py-2 socials-text">
                                                    <Skeleton isLoading={isLoading} mode="Text" disableMarginY>
                                                        {socials?.social1?.textSubtitle}
                                                    </Skeleton>
                                                </h3>
                                                <h4 className="is-size-6 has-text-dark has-text-weight-semibold has-text-centered py-2">
                                                    <Skeleton isLoading={isLoading} mode="Text" disableMarginY>
                                                        {socials?.social1?.textComment}
                                                    </Skeleton>
                                                </h4>
                                            </div>
                                        </Link>
                                    </div>
                                </Animated>
                            </div>
                            <div className={`bulma-column is-clickable ${media.isMobile ? "mt-6" : ""}`}>
                                <Animated dataAos="fade-up" dataAosDelay={150}>
                                    <div className="bulma-card">
                                        <Link to={socials?.social2?.action?.href}>
                                            <div className="bulma-card-image">
                                                <figure className="bulma-image">
                                                    <Skeleton
                                                        isLoading={isLoading}
                                                        mode="Rect"
                                                        className="socials-image"
                                                    >
                                                        <CustomImage
                                                            base={GET_SOCIALS_URL}
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
                                                    <CustomImage
                                                        base={GET_SOCIALS_URL}
                                                        source={socials?.social2?.images?.avatar}
                                                        className="socials-avatar"
                                                        title="Socials"
                                                        alt={socials?.social2?.textTitle}
                                                    />
                                                </Skeleton>
                                            </div>
                                            <div className={`bulma-card-content ${paddings}`}>
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
                                                <h3 className="is-size-6 has-text-grey has-text-centered py-2 socials-text">
                                                    <Skeleton isLoading={isLoading} mode="Text" disableMarginY>
                                                        {socials?.social2?.textSubtitle}
                                                    </Skeleton>
                                                </h3>
                                                <h4 className="is-size-6 has-text-dark has-text-weight-semibold has-text-centered py-2">
                                                    <Skeleton isLoading={isLoading} mode="Text" disableMarginY>
                                                        {socials?.social2?.textComment}
                                                    </Skeleton>
                                                </h4>
                                            </div>
                                        </Link>
                                    </div>
                                </Animated>
                            </div>
                            <div className={`bulma-column is-clickable ${media.isMobile ? "mt-6" : ""}`}>
                                <Animated dataAos="fade-up" dataAosDelay={250}>
                                    <div className="bulma-card">
                                        <Link to={socials?.social3?.action?.href}>
                                            <div className="bulma-card-image">
                                                <figure className="bulma-image">
                                                    <Skeleton
                                                        isLoading={isLoading}
                                                        mode="Rect"
                                                        className="socials-image"
                                                    >
                                                        <CustomImage
                                                            base={GET_SOCIALS_URL}
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
                                                    <CustomImage
                                                        base={GET_SOCIALS_URL}
                                                        source={socials?.social3?.images?.avatar}
                                                        className="socials-avatar"
                                                        title="Socials"
                                                        alt={socials?.social3?.textTitle}
                                                    />
                                                </Skeleton>
                                            </div>
                                            <div className={`bulma-card-content ${paddings}`}>
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
                                                <h3 className="is-size-6 has-text-grey has-text-centered py-2 socials-text">
                                                    <Skeleton isLoading={isLoading} mode="Text" disableMarginY>
                                                        {socials?.social3?.textSubtitle}
                                                    </Skeleton>
                                                </h3>
                                                <h4 className="is-size-6 has-text-dark has-text-weight-semibold has-text-centered py-2">
                                                    <Skeleton isLoading={isLoading} mode="Text" disableMarginY>
                                                        {socials?.social3?.textComment}
                                                    </Skeleton>
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
