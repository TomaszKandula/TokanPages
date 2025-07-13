import * as React from "react";
import { useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { GET_ICONS_URL } from "../../../Api";
import { ClientImageDto, ClientsContentDto } from "../../../Api/Models";
import { v4 as uuidv4 } from "uuid";
import Validate from "validate.js";

interface ClientsViewProps {
    className?: string;
}

const RenderCaption = (props: ClientsContentDto): React.ReactElement | null => {
    if (!Validate.isEmpty(props?.caption)) {
        return <p className="is-size-3	has-text-centered has-text-link">{props?.caption?.toUpperCase()}</p>;
    }

    return null;
};

const RenderImages = (props: ClientsContentDto): React.ReactElement => {
    const getImagePath = (value: string): string => `${GET_ICONS_URL}/${value}`;
    return (
        <div className="is-flex is-flex-wrap-wrap is-justify-content-center is-align-items-center">
            {props?.images?.map((item: ClientImageDto, _index: number) => (
                <div className="p-6" key={uuidv4()}>
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
            <section className={`clients-section ${props.className ?? ""}`}>
                <div className="bulma-container">
                    <div className="py-6">
                        <RenderCaption {...clients} />
                        <RenderImages {...clients} />
                    </div>
                </div>
            </section>
        </>
    );
};
