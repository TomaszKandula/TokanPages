import * as React from "react";
import { useSelector } from "react-redux";
import { Link } from "react-router-dom";
import Skeleton from "@material-ui/lab/Skeleton";
import Button from "@material-ui/core/Button";
import Grid from "@material-ui/core/Grid/Grid";
import { GET_IMAGES_URL } from "../../../Api/Request";
import { HeaderContentDto, HeaderPhotoDto } from "../../../Api/Models";
import { ApplicationState } from "../../../Store/Configuration";
import Validate from "validate.js";

interface HeaderViewProps {
    background?: string;
}

interface RenderPictureProps {
    sources: HeaderPhotoDto | undefined;
}

const OpenLinkButton = (props: HeaderContentDto): React.ReactElement => {
    return (
        <Link to={props?.resume?.href ?? ""} className="link" rel="noopener nofollow">
            <Button variant="contained" className="header-button-resume">
                {props?.resume?.text}
            </Button>
        </Link>
    );
};

const ActiveButton = (props: HeaderContentDto): React.ReactElement => {
    if (Validate.isEmpty(props?.action?.href)) {
        return (
            <Button variant="contained" className="header-button">
                {props?.action?.text}
            </Button>
        );
    }

    return (
        <Link to={props?.action?.href ?? ""} className="link" rel="noopener nofollow">
            <Button variant="contained" className="header-button">
                {props?.action?.text}
            </Button>
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
            title="Illustration"
            alt="Your Software Developer"
            className="header-image-card lazyloaded"
        />
    );
};

export const HeaderView = (props: HeaderViewProps): React.ReactElement => {
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const header = data?.components?.header;
    return (
        <section className={`section margin-top-60 ${props.background ?? ""}`}>
            <Grid container spacing={3}>
                <Grid item xs={12} md={7}>
                    {data?.isLoading ? (
                        <Skeleton variant="rect" className="header-image-skeleton" />
                    ) : (
                        <RenderPicture sources={header?.photo} />
                    )}
                </Grid>
                <Grid item xs={12} md={5} className="header-section-container">
                    <div className="header-content-box">
                        {data?.isLoading ? (
                            <Skeleton variant="text" />
                        ) : (
                            <span className="header-content-caption">{header?.caption}</span>
                        )}
                        {data?.isLoading ? (
                            <Skeleton variant="text" />
                        ) : (
                            <h1 className="header-content-subtitle">{header?.subtitle}</h1>
                        )}
                        {data?.isLoading ? (
                            <Skeleton variant="text" />
                        ) : (
                            <h2 className="header-content-description">{header?.description}</h2>
                        )}
                        {data?.isLoading ? (
                            <Skeleton variant="text" />
                        ) : (
                            <h2 className="header-content-description">{header?.hint}</h2>
                        )}
                        <div className="mt-32">
                            {data?.isLoading ? (
                                <Skeleton variant="rect" height="48px" />
                            ) : (
                                <>
                                    <ActiveButton {...header} />
                                    <OpenLinkButton {...header} />
                                </>
                            )}
                        </div>
                    </div>
                </Grid>
            </Grid>
        </section>
    );
};
