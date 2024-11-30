import * as React from "react";
import { useSelector } from "react-redux";
import Skeleton from "@material-ui/lab/Skeleton";
import { Container, Typography } from "@material-ui/core";
import { ApplicationState } from "../../../Store/Configuration";
import { GET_ICONS_URL } from "../../../Api/Request";
import { ClientImageDto, ClientsContentDto } from "../../../Api/Models";
import { v4 as uuidv4 } from "uuid";
import Validate from "validate.js";

interface ClientsViewProps {
    background?: React.CSSProperties;
}

const RenderCaption = (props: ClientsContentDto): React.ReactElement | null => {
    if (!Validate.isEmpty(props?.caption)) {
        return (
            <div style={{ marginBottom: 64 }}>
                <Typography className="clients-caption">{props?.caption?.toUpperCase()}</Typography>
            </div>
        );
    }

    return null;
};

const RenderImages = (props: ClientsContentDto): React.ReactElement => {
    const getImagePath = (value: string): string => `${GET_ICONS_URL}/${value}`;
    return (
        <div style={{ display: "flex", flexWrap: "wrap", justifyContent: "center", paddingTop: 32 }}>
            {props?.images?.map((item: ClientImageDto, _index: number) => (
                <div className="clients-logo">
                    <img 
                        key={uuidv4()}
                        src={getImagePath(item.path)}
                        alt={`image of ${item.path}`}
                        height={item.heigh}
                        width={item.width}
                    />
                </div>
            ))}
        </div>
    );
};

export const ClientsView = (props: ClientsViewProps): React.ReactElement => {
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const clients = data?.components.clients;

    return (
        <>
            <section className="clients-section" style={props.background}>
                <Container maxWidth="lg">
                    {data?.isLoading ? <Skeleton variant="text" /> : <RenderCaption {...clients} />}
                    {data?.isLoading ? <Skeleton variant="rect" height="48px" /> : <RenderImages {...clients} />}
                </Container>
            </section>
        </>
    );
};
