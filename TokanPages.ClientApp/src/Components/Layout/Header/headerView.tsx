import * as React from "react";
import { useSelector } from "react-redux";
import { Link } from "react-router-dom";
import Typography from "@material-ui/core/Typography";
import Skeleton from "@material-ui/lab/Skeleton";
import Button from "@material-ui/core/Button";
import Grid from "@material-ui/core/Grid/Grid";
import { CardMedia } from "@material-ui/core";
import { GET_IMAGES_URL } from "../../../Api/Request";
import { HeaderContentDto } from "../../../Api/Models";
import { ApplicationState } from "../../../Store/Configuration";
import { ReactHtmlParser } from "../../../Shared/Services/Renderers";
import Validate from "validate.js";

interface HeaderViewProps {
    background?: React.CSSProperties;
}

const OpenLinkButton = (props: HeaderContentDto): React.ReactElement => {
    return (
        <Link to={props?.resume?.href ?? ""} className="link">
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
        <Link to={props?.action?.href ?? ""} className="link">
            <Button variant="contained" className="header-button">
                {props?.action?.text}
            </Button>
        </Link>
    );
};

export const HeaderView = (props: HeaderViewProps): React.ReactElement => {
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const header = data?.components?.header;
    const imageUrl = (name: string) => {
        if (name === "") return " ";
        return `${GET_IMAGES_URL}/${name}`;
    };

    return (
        <section className="section margin-top-60" style={props.background}>
            <Grid container spacing={3}>
                <Grid item xs={12} md={7}>
                    {data?.isLoading ? (
                        <Skeleton variant="rect" className="header-image-skeleton" />
                    ) : (
                        <CardMedia
                            image={imageUrl(header?.photo)}
                            component="img"
                            className="header-image-card"
                            alt={`photo of ${header?.photo}`}
                        />
                    )}
                </Grid>
                <Grid item xs={12} md={5} className="header-section-container">
                    <div className="header-content-box">
                        <Typography component="span" className="header-content-caption">
                            {data?.isLoading ? <Skeleton variant="text" /> : <ReactHtmlParser html={header?.caption} />}
                        </Typography>
                        <Typography component="span" className="header-content-subtitle">
                            {data?.isLoading ? (
                                <Skeleton variant="text" />
                            ) : (
                                <ReactHtmlParser html={header?.subtitle} />
                            )}
                        </Typography>
                        <Typography component="span" className="header-content-description">
                            {data?.isLoading ? (
                                <Skeleton variant="text" />
                            ) : (
                                <ReactHtmlParser html={header?.description} />
                            )}
                        </Typography>
                        <div style={{ marginTop: 32 }}>
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
