import * as React from "react";
import { useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { GET_IMAGES_URL } from "../../../Api";
import { ClientImageDto, ClientsContentDto } from "../../../Api/Models";
import { Skeleton } from "../../../Shared/Components";
import { v4 as uuidv4 } from "uuid";
import Validate from "validate.js";
import "./clientsView.css";

interface ClientsViewProps {
    className?: string;
}

interface ClientsViewExtendedProps extends ClientsContentDto {
    isLoading: boolean;
}

const RenderCaption = (props: ClientsViewExtendedProps): React.ReactElement | null => {
    if (!Validate.isEmpty(props?.caption)) {
        return (
            <Skeleton isLoading={props.isLoading} mode="Text" height={40}>
                <p className="is-size-3	has-text-centered has-text-link">{props?.caption?.toUpperCase()}</p>
            </Skeleton>
        );
    }

    return null;
};

const RenderImages = (props: ClientsViewExtendedProps): React.ReactElement => (
        <div className="is-flex is-flex-wrap-wrap is-justify-content-center is-align-items-center">
            {props?.images?.map((item: ClientImageDto, _index: number) => (
                <div className="client-item-margins" key={uuidv4()}>
                    <Skeleton isLoading={props.isLoading} mode="Rect" width={100} height={100}>
                        <img
                            src={`${GET_IMAGES_URL}/${item.path}`}
                            loading="lazy"
                            alt={`An image of ${item.name}`}
                            title="Clients"
                            height={item.heigh}
                            width={item.width}
                        />
                    </Skeleton>
                </div>
            ))}
        </div>
    );


export const ClientsView = (props: ClientsViewProps): React.ReactElement => {
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const clients = data?.components.sectionClients;

    return (
        <>
            <section className={props.className}>
                <div className="bulma-container">
                    <div className="client-margins">
                        <RenderCaption isLoading={data?.isLoading} {...clients} />
                        <RenderImages isLoading={data?.isLoading} {...clients} />
                    </div>
                </div>
            </section>
        </>
    );
};
