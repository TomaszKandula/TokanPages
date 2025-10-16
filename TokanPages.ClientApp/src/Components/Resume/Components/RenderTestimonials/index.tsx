import React from "react";
import { GET_IMAGES_URL } from "../../../../Api";
import { Image, Link, Skeleton } from "../../../../Shared/Components";
import { ResumeViewProps } from "../../Types";

export const RenderTestimonials = (props: ResumeViewProps) => (
    <div className="bulma-content is-size-5 has-text-grey-dark">
        <div className="mt-5 mb-6">
            <Skeleton isLoading={props.isLoading} mode="Rect" height={150}>
                <blockquote className="is-italic has-text-justified has-background-white line-height-20 m-0">
                    {props.section.text1}
                </blockquote>
            </Skeleton>
            <div className="is-flex is-justify-content-flex-end is-align-items-center is-gap-1.5">
                <div className="has-text-right my-4">
                    <Skeleton isLoading={props.isLoading} width={150} height={24}>
                        <Link to={props.section.linkedIn1} className="is-underlined">
                            <>{props.section.name1}</>
                        </Link>
                    </Skeleton>
                    <Skeleton isLoading={props.isLoading} height={24} width={150}>
                        <p className="m-0">{props.section.occupation1}</p>
                    </Skeleton>
                </div>
                <Skeleton isLoading={props.isLoading} mode="Circle" width={64} height={64}>
                    <figure className="bulma-image bulma-is-64x64">
                        <Image
                            base={GET_IMAGES_URL}
                            source={props.section.photo1}
                            title={props.section.name1}
                            alt={props.section.name1}
                            loading={props.isSnapshot ? "eager" : "lazy"}
                            className="bulma-is-rounded is-round-border-light"
                        />
                    </figure>
                </Skeleton>
            </div>
        </div>
        <div className="mb-6">
            <Skeleton isLoading={props.isLoading} mode="Rect" height={150}>
                <blockquote className="is-italic has-text-justified has-background-white line-height-20 m-0">
                    {props.section.text2}
                </blockquote>
            </Skeleton>
            <div className="is-flex is-justify-content-flex-end is-align-items-center is-gap-1.5">
                <div className="has-text-right my-4">
                    <Skeleton isLoading={props.isLoading} width={150} height={24}>
                        <Link to={props.section.linkedIn2} className="is-underlined">
                            <>{props.section.name2}</>
                        </Link>
                    </Skeleton>
                    <Skeleton isLoading={props.isLoading} width={150} height={24}>
                        <p className="m-0">{props.section.occupation2}</p>
                    </Skeleton>
                </div>
                <Skeleton isLoading={props.isLoading} mode="Circle" width={64} height={64}>
                    <figure className="bulma-image bulma-is-64x64">
                        <Image
                            base={GET_IMAGES_URL}
                            source={props.section.photo2}
                            title={props.section.name2}
                            alt={props.section.name2}
                            loading={props.isSnapshot ? "eager" : "lazy"}
                            className="bulma-is-rounded is-round-border-light"
                        />
                    </figure>
                </Skeleton>
            </div>
        </div>
        <div className="mb-6">
            <Skeleton isLoading={props.isLoading} mode="Rect" height={150}>
                <blockquote className="is-italic has-text-justified has-background-white line-height-20 m-0">
                    {props.section.text3}
                </blockquote>
            </Skeleton>
            <div className="is-flex is-justify-content-flex-end is-align-items-center is-gap-1.5">
                <div className="has-text-right my-4">
                    <Skeleton isLoading={props.isLoading} width={150} height={24}>
                        <Link to={props.section.linkedIn3} className="is-underlined">
                            <>{props.section.name3}</>
                        </Link>
                    </Skeleton>
                    <Skeleton isLoading={props.isLoading} width={150} height={24}>
                        <p className="m-0">{props.section.occupation3}</p>
                    </Skeleton>
                </div>
                <Skeleton isLoading={props.isLoading} mode="Circle" width={64} height={64}>
                    <figure className="bulma-image bulma-is-64x64">
                        <Image
                            base={GET_IMAGES_URL}
                            source={props.section.photo3}
                            title={props.section.name3}
                            alt={props.section.name3}
                            loading={props.isSnapshot ? "eager" : "lazy"}
                            className="bulma-is-rounded is-round-border-light"
                        />
                    </figure>
                </Skeleton>
            </div>
        </div>
    </div>
);
