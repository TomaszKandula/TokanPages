import * as React from "react";
import { useSelector } from "react-redux";
import Skeleton from "@material-ui/lab/Skeleton";
import { Container, Typography } from "@material-ui/core";
import { ApplicationState } from "../../../Store/Configuration";
import { GET_ICONS_URL } from "../../../Api";
import { ClientImageDto, ClientsContentDto } from "../../../Api/Models";
import { v4 as uuidv4 } from "uuid";
import Validate from "validate.js";
import "./clientsView.css";

interface ClientsViewProps {
    background?: string;
}

const RenderCaption = (props: ClientsContentDto): React.ReactElement | null => {
    if (!Validate.isEmpty(props?.caption)) {
        return (
            <div className="mt-48 mb-64">
                <Typography className="clients-caption">{props?.caption?.toUpperCase()}</Typography>
            </div>
        );
    }

    return null;
};

const RenderImages = (props: ClientsContentDto): React.ReactElement => {
    const getImagePath = (value: string): string => `${GET_ICONS_URL}/${value}`;
    return (
        <div className="clients-render-images mt-15 mb-64">
            {props?.images?.map((item: ClientImageDto, _index: number) => (
                <div className="clients-logo" key={uuidv4()}>
                    <img
                        src={getImagePath(item.path)}
                        loading="lazy"
                        alt={`An image of ${item.name}`}
                        title="Clients"
                        height={item.heigh}
                        width={item.width}
                        className="lazyloaded"
                    />
                </div>
            ))}
        </div>
    );
};

export const ClientsView = (props: ClientsViewProps): React.ReactElement => {
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const clients = data?.components.sectionClients;

    return (
        <>
            <section className={`clients-section ${props.background ?? ""}`}>
                <Container maxWidth="lg">
                    {data?.isLoading ? <Skeleton variant="text" /> : <RenderCaption {...clients} />}
                    {data?.isLoading ? <Skeleton variant="rect" height="48px" /> : <RenderImages {...clients} />}
                </Container>
            </section>
        </>
    );
};
