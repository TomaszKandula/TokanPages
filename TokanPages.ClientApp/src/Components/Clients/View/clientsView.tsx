import * as React from "react";
import { useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { GET_ICONS_URL } from "../../../Api";
import { ClientImageDto, ClientsContentDto } from "../../../Api/Models";
import { Skeleton } from "../../../Shared/Components";
import { useDimensions } from "../../../Shared/Hooks";
import { v4 as uuidv4 } from "uuid";
import Validate from "validate.js";

interface ClientsViewProps {
    className?: string;
}

interface ClientsViewExtendedProps extends ClientsContentDto {
    isLoading: boolean;
    isDesktop: boolean;
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

const RenderImages = (props: ClientsViewExtendedProps): React.ReactElement => {
    const getImagePath = (value: string): string => `${GET_ICONS_URL}/${value}`;

    return (
        <div className="is-flex is-flex-wrap-wrap is-justify-content-center is-align-items-center">
            {props?.images?.map((item: ClientImageDto, _index: number) => (
                <div className={`${props.isDesktop ? "mx-6" : "m-6"}`} key={uuidv4()}>
                    <Skeleton isLoading={props.isLoading} mode="Rect" width={100} height={100}>
                        <img
                            src={getImagePath(item.path)}
                            loading="lazy"
                            alt={`An image of ${item.name}`}
                            title="Clients"
                            height={item.heigh}
                            width={item.width}
                            className="lazyloaded"
                        />
                    </Skeleton>
                </div>
            ))}
        </div>
    );
};

export const ClientsView = (props: ClientsViewProps): React.ReactElement => {
    const media = useDimensions();
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const clients = data?.components.sectionClients;

    return (
        <>
            <section className={props.className}>
                <div className="bulma-container">
                    <div className={`${media.isDesktop ? "py-6" : "py-4"}`}>
                        <RenderCaption isLoading={data?.isLoading} isDesktop={media.isDesktop} {...clients} />
                        <RenderImages isLoading={data?.isLoading} isDesktop={media.isDesktop} {...clients} />
                    </div>
                </div>
            </section>
        </>
    );
};
