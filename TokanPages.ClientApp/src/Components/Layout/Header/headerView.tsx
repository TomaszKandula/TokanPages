import * as React from "react";
import { useSelector } from "react-redux";
import { GET_IMAGES_URL } from "../../../Api";
import { HeaderContentDto, HeaderPhotoDto } from "../../../Api/Models";
import { ApplicationState } from "../../../Store/Configuration";
import { useDimensions } from "../../../Shared/Hooks";
import { Link, Skeleton } from "../../../Shared/Components";
import Validate from "validate.js";
import "./headerView.css";

interface HeaderViewProps {
    className?: string;
}

interface RenderPictureProps {
    sources: HeaderPhotoDto | undefined;
    className?: string;
}

interface ButtonProps extends HeaderContentDto {
    isLoading: boolean;
    isMobile: boolean;
}

const TertiaryButton = (props: ButtonProps): React.ReactElement => {
    const baseClass = "bulma-button bulma-is-light mr-2";
    const size = props.isMobile ? "bulma-is-fullwidth mb-2" : "";

    return (
        <Link to={props?.tertiaryButton?.href ?? ""} className="link">
            <button className={`${baseClass} ${size}`}>{props?.tertiaryButton?.text}</button>
        </Link>
    );
};

const SecondaryButton = (props: ButtonProps): React.ReactElement => {
    const baseClass = "bulma-button bulma-is-light mr-2";
    const size = props.isMobile ? "bulma-is-fullwidth mb-2" : "";

    return (
        <Link to={props?.secondaryButton?.href ?? ""} className="link">
            <button className={`${baseClass} ${size}`}>{props?.secondaryButton?.text}</button>
        </Link>
    );
};

const PrimaryButton = (props: ButtonProps): React.ReactElement => {
    const baseClass = "bulma-button bulma-is-link bulma-is-light mr-2";
    const size = props.isMobile ? "bulma-is-fullwidth mb-2" : "";

    if (Validate.isEmpty(props?.primaryButton?.href)) {
        return <button className={`${baseClass} ${size}`}>{props?.primaryButton?.text}</button>;
    }

    return (
        <Link to={props?.primaryButton?.href ?? ""} className="link">
            <button className={`${baseClass} ${size}`}>{props?.primaryButton?.text}</button>
        </Link>
    );
};

const RenderPicture = (props: RenderPictureProps): React.ReactElement | null => {
    if (!props.sources) {
        return null;
    }

    if (
        props.sources.w360 === "" ||
        props.sources.w720 === "" ||
        props.sources.w1440 === "" ||
        props.sources.w2880 === ""
    ) {
        return null;
    }

    const photo1 = `${GET_IMAGES_URL}/${props.sources.w360}`;
    const photo2 = `${GET_IMAGES_URL}/${props.sources.w720}`;
    const photo3 = `${GET_IMAGES_URL}/${props.sources.w1440}`;
    const photo4 = `${GET_IMAGES_URL}/${props.sources.w2880}`;
    const set = `${photo1} 360w, ${photo2} 720w, ${photo3} 1440w, ${photo4} 2880w`;

    return (
        <figure>
            <img
                src={photo1}
                srcSet={set}
                loading="lazy"
                title="Tom Kandula"
                alt="Your Software Developer"
                className={`header-image-card lazyloaded ${props.className}`}
            />
        </figure>
    );
};

export const HeaderView = (props: HeaderViewProps): React.ReactElement => {
    const media = useDimensions();
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const header = data?.components?.layoutHeader;
    const isLoading = data?.isLoading;
    const baseClass = "bulma-content header-content-box";

    let size;
    if (media.isMobile) {
        size = { width: 360, height: 433 };
    } else {
        size = { width: 720, height: 865 };
    }

    return (
        <section className={props.className ?? ""}>
            <div className="bulma-fixed-grid bulma-has-1-cols-mobile bulma-has-1-cols-tablet bulma-has-2-cols-desktop">
                <div className="bulma-grid">
                    <div className="bulma-cell">
                        <Skeleton isLoading={isLoading} mode="Rect" {...size}>
                            <RenderPicture sources={header?.photo} />
                        </Skeleton>
                    </div>
                    <div className="bulma-cell is-flex is-flex-direction-column">
                        <div className={`${baseClass} ${media.isMobile ? "p-4" : ""} ${media.isTablet ? "p-6" : ""}`}>
                            <Skeleton isLoading={isLoading} mode="Text" height={40}>
                                <h1 className="is-size-1 has-text-grey-dark">{header?.caption}</h1>
                            </Skeleton>
                            <Skeleton isLoading={isLoading} mode="Text" height={24}>
                                <h2 className="has-text-weight-medium is-size-5 has-text-grey-dark line-height-15 my-4">
                                    {header?.subtitle}
                                </h2>
                            </Skeleton>
                            <Skeleton isLoading={isLoading} mode="Text" height={24}>
                                <h3 className="has-text-weight-normal is-size-5 has-text-grey line-height-15 my-4">
                                    {header?.description}
                                </h3>
                            </Skeleton>
                            <Skeleton isLoading={isLoading} mode="Text" height={24}>
                                <h3 className="has-text-weight-normal is-size-5 has-text-grey line-height-15 my-4">
                                    {header?.hint}
                                </h3>
                            </Skeleton>
                            <div className={`pt-4 ${isLoading ? "is-flex is-gap-1.5" : ""}`}>
                                <Skeleton isLoading={isLoading} mode="Rect" width={100}>
                                    <PrimaryButton {...header} isLoading={isLoading} isMobile={media.isMobile} />
                                </Skeleton>
                                <Skeleton isLoading={isLoading} mode="Rect" width={100}>
                                    <SecondaryButton {...header} isLoading={isLoading} isMobile={media.isMobile} />
                                </Skeleton>
                                <Skeleton isLoading={isLoading} mode="Rect" width={100}>
                                    <TertiaryButton {...header} isLoading={isLoading} isMobile={media.isMobile} />
                                </Skeleton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};
