import * as React from "react";
import { useSelector } from "react-redux";
import { GET_IMAGES_URL } from "../../../Api";
import { ApplicationState } from "../../../Store/Configuration";
import { useDimensions } from "../../../Shared/Hooks";
import { Link, RenderHtml, Skeleton } from "../../../Shared/Components";
import { ButtonProps, HeaderViewProps, RenderPictureProps } from "./Types";
import Validate from "validate.js";
import "./headerView.css";

const TertiaryButton = (props: ButtonProps): React.ReactElement => (
    <Link to={props?.tertiaryButton?.href ?? ""} className="link">
        <button className="bulma-button bulma-is-light mr-2 is-hidden-touch">{props?.tertiaryButton?.text}</button>
        <button className="bulma-button bulma-is-light mr-2 bulma-is-fullwidth mb-2 is-hidden-desktop">
            {props?.tertiaryButton?.text}
        </button>
    </Link>
);

const SecondaryButton = (props: ButtonProps): React.ReactElement => (
    <Link to={props?.secondaryButton?.href ?? ""} className="link">
        <button className="bulma-button bulma-is-light mr-2 is-hidden-touch">{props?.secondaryButton?.text}</button>
        <button className="bulma-button bulma-is-light mr-2 bulma-is-fullwidth mb-2 is-hidden-desktop">
            {props?.secondaryButton?.text}
        </button>
    </Link>
);

const PrimaryButton = (props: ButtonProps): React.ReactElement => {
    if (Validate.isEmpty(props?.primaryButton?.href)) {
        return (
            <>
                <button className="bulma-button bulma-is-link bulma-is-light mr-2 is-hidden-touch">
                    {props?.primaryButton?.text}
                </button>
                <button className="bulma-button bulma-is-link bulma-is-light mr-2 bulma-is-fullwidth mb-2 is-hidden-desktop">
                    {props?.primaryButton?.text}
                </button>
            </>
        );
    }

    return (
        <Link to={props?.primaryButton?.href ?? ""} className="link">
            <button className="bulma-button bulma-is-link bulma-is-light mr-2 is-hidden-touch">
                {props?.primaryButton?.text}
            </button>
            <button className="bulma-button bulma-is-link bulma-is-light mr-2 bulma-is-fullwidth mb-2 is-hidden-desktop">
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
        <img
            src={photo1}
            srcSet={set}
            loading="lazy"
            title="Tom Kandula"
            alt="Your Software Developer"
            className="header-image header-figure"
        />
    );
};

export const HeaderView = (props: HeaderViewProps): React.ReactElement => {
    const media = useDimensions();
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const header = data?.components?.layoutHeader;
    const isLoading = data?.isLoading;

    let size;
    if (media.isMobile) {
        size = { width: 433, height: 360 };
    } else {
        size = { width: 865, height: 720 };
    }

    return (
        <section className={props.className ?? ""}>
            <div className="bulma-fixed-grid bulma-has-1-cols-mobile bulma-has-1-cols-tablet bulma-has-2-cols-desktop">
                <div className="bulma-grid">
                    <div className="bulma-cell">
                        <Skeleton isLoading={isLoading} mode="Rect" {...size} disableMarginY>
                            <RenderPicture sources={header?.photo} />
                        </Skeleton>
                    </div>
                    <div className="bulma-cell is-flex is-flex-direction-column">
                        <div className="bulma-content header-content-box">
                            <Skeleton isLoading={isLoading} mode="Text" height={40}>
                                <p className="is-size-1 has-text-weight-bold has-text-grey-dark m-0">
                                    {header?.caption}
                                </p>
                            </Skeleton>
                            <Skeleton isLoading={isLoading} mode="Text" height={24}>
                                <RenderHtml
                                    value={header?.subtitle}
                                    tag="h2"
                                    className="has-text-weight-medium is-size-5 has-text-grey-dark line-height-15 my-4"
                                />
                            </Skeleton>
                            <Skeleton isLoading={isLoading} mode="Text" height={24}>
                                <RenderHtml
                                    value={header?.description}
                                    tag="h3"
                                    className="has-text-weight-normal is-size-5 has-text-grey line-height-15 my-4"
                                />
                            </Skeleton>
                            <Skeleton isLoading={isLoading} mode="Text" height={24}>
                                <RenderHtml
                                    value={header?.hint}
                                    tag="p"
                                    className="has-text-weight-normal is-size-5 has-text-grey line-height-15 my-4"
                                />
                            </Skeleton>
                            <div className={`pt-4 ${isLoading ? "is-flex is-gap-1.5" : ""}`}>
                                <Skeleton isLoading={isLoading} mode="Rect" width={100}>
                                    <PrimaryButton {...header} isLoading={isLoading} />
                                </Skeleton>
                                <Skeleton isLoading={isLoading} mode="Rect" width={100}>
                                    <SecondaryButton {...header} isLoading={isLoading} />
                                </Skeleton>
                                <Skeleton isLoading={isLoading} mode="Rect" width={100}>
                                    <TertiaryButton {...header} isLoading={isLoading} />
                                </Skeleton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
};
