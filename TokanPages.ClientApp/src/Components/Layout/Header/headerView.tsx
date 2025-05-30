import * as React from "react";
import { useSelector } from "react-redux";
import { GET_IMAGES_URL } from "../../../Api";
import { HeaderContentDto, HeaderPhotoDto } from "../../../Api/Models";
import { ApplicationState } from "../../../Store/Configuration";
import { Link } from "../../../Shared/Components";
import Validate from "validate.js";
import "./headerView.css";

interface HeaderViewProps {
    background?: string;
}

interface RenderPictureProps {
    sources: HeaderPhotoDto | undefined;
    className?: string;
}

interface ButtonProps extends HeaderContentDto {
    isLoading: boolean;
}

const TertiaryButton = (props: ButtonProps): React.ReactElement => {
    const baseClass = "bulma-button bulma-is-light header-button-tertiary";
    return (
        <Link to={props?.tertiaryButton?.href ?? ""} className="link">
            <button className={`${baseClass} ${props.isLoading ? "bulma-is-skeleton" : ""}`}>
                {props?.tertiaryButton?.text}
            </button>
        </Link>
    );
};

const SecondaryButton = (props: ButtonProps): React.ReactElement => {
    const baseClass = "bulma-button bulma-is-light header-button-secondary";
    return (
        <Link to={props?.secondaryButton?.href ?? ""} className="link">
            <button className={`${baseClass} ${props.isLoading ? "bulma-is-skeleton" : ""}`}>
                {props?.secondaryButton?.text}
            </button>
        </Link>
    );
};

const PrimaryButton = (props: ButtonProps): React.ReactElement => {
    const baseClass = "bulma-button bulma-is-link bulma-is-light header-button-primary";

    if (Validate.isEmpty(props?.primaryButton?.href)) {
        return (
            <button className={`${baseClass} ${props.isLoading ? "bulma-is-skeleton" : ""}`}>
                {props?.primaryButton?.text}
            </button>
        );
    }

    return (
        <Link to={props?.primaryButton?.href ?? ""} className="link">
            <button className={`${baseClass} ${props.isLoading ? "bulma-is-skeleton" : ""}`}>
                {props?.primaryButton?.text}
            </button>
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
                title="Illustration"
                alt="Your Software Developer"
                className={`header-image-card lazyloaded ${props.className}`}
            />
        </figure>
    );
};

export const HeaderView = (props: HeaderViewProps): React.ReactElement => {
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const header = data?.components?.layoutHeader;
    const isLoading = data?.isLoading;

    return (
        <section className={`section margin-top-60 ${props.background ?? ""}`}>
            <div className="bulma-grid">
                <div className="bulma-cell">
                    <RenderPicture sources={header?.photo} className={`${isLoading ? "bulma-is-skeleton header-image-skeleton" : ""}`} />
                </div>
                <div className="bulma-cell is-flex is-flex-direction-column is-flex-wrap-wrap">
                    <div className="bulma-content header-content-box">
                        <h1 className={`is-size-1 has-text-grey-dark ${isLoading ? "bulma-is-skeleton" : ""}`}>
                            {header?.caption}
                        </h1>
                        <h2 className={`has-text-weight-medium has-text-grey-dark is-size-4 ${isLoading ? "bulma-is-skeleton" : ""}`}>
                            {header?.subtitle}
                        </h2>
                        <h3 className={`has-text-weight-light is-size-5 ${isLoading ? "bulma-is-skeleton" : ""}`}>
                            {header?.description}
                        </h3>
                        <h3 className={`has-text-weight-light is-size-5 ${isLoading ? "bulma-is-skeleton" : ""}`}>
                            {header?.hint}
                        </h3>
                        <div className="header-button-box mt-32">
                            <PrimaryButton {...header} isLoading={isLoading} />
                            <SecondaryButton {...header} isLoading={isLoading} />
                            <TertiaryButton {...header} isLoading={isLoading} />
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};
