import React from "react";
import { GET_IMAGES_URL } from "../../../Api";
import { ImageDto } from "../../../Api/Models";
import { ProcessParagraphs } from "../RenderContent/Renderers";
import { Image } from "../Image";
import { Skeleton } from "../Skeleton";
import { Link } from "../Link";
import { Icon } from "../Icon";
import { PresentationViewProps } from "./Types";
import { v4 as uuidv4 } from "uuid";

export const PresentationView = (props: PresentationViewProps) => (
    <>
        <div className="is-flex mb-5">
            <Skeleton isLoading={props.isLoading} mode="Circle" width={128} height={128}>
                <figure className="bulma-image bulma-is-128x128">
                    <Image
                        base={GET_IMAGES_URL}
                        source={props.image.link}
                        title={props.image.title}
                        alt={props.image.alt}
                        className="bulma-is-rounded is-round-border-light"
                    />
                </figure>
            </Skeleton>
            <div className="bulma-content ml-5 is-flex is-flex-direction-column is-align-self-center is-gap-0.5">
                <Skeleton isLoading={props.isLoading} mode="Text" width={200} height={24}>
                    <div className="is-size-4 has-text-weight-bold">{props.title}</div>
                </Skeleton>
                <Skeleton isLoading={props.isLoading} mode="Text" width={200} height={24}>
                    <div className="is-size-5 has-text-weight-semibold has-text-link">{props.subtitle}</div>
                </Skeleton>
                <div className="contact-form-icon-container pt-2">
                    <Skeleton isLoading={props.isLoading} mode="Rect" width={24} height={24}>
                        <Link to={props.icon.href} key={uuidv4()} aria-label={props.icon.name}>
                            <figure className="bulma-image bulma-is-24x24">
                                <Icon name={props.icon.name} size={1.5} />
                            </figure>
                        </Link>
                    </Skeleton>
                </div>
            </div>
        </div>
        <div className="bulma-content pb-6">
            <Skeleton isLoading={props.isLoading} mode="Text" width={500} height={40}>
                <ProcessParagraphs tag="p" html={props.description} className="is-size-6 line-height-20" />
            </Skeleton>
            <Skeleton isLoading={props.isLoading} mode="Text" width={250} height={40} className="my-6">
                <h3 className="is-size-3 mt-5 mb-6">{props.logos.title}</h3>
            </Skeleton>
            <div className="bulma-fixed-grid">
                <div className="bulma-grid is-gap-7">
                    {props.logos.images.map((value: ImageDto, _index: number) => (
                        <div
                            className="bulma-cell is-flex is-justify-content-center is-align-self-center"
                            key={uuidv4()}
                        >
                            <Skeleton isLoading={props.isLoading} mode="Rect" width={value.width} height={value.heigh}>
                                <Image
                                    base={GET_IMAGES_URL}
                                    source={value.link}
                                    title={value.title}
                                    alt={value.alt}
                                    width={value.width}
                                    height={value.heigh}
                                />
                            </Skeleton>
                        </div>
                    ))}
                </div>
            </div>
        </div>
    </>
);
