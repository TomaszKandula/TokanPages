import * as React from "react";
import { Link } from "react-router-dom";
import { ViewProperties } from "../../../Shared/Abstractions";
import { TColour } from "../../../Shared/types";
import { useDimensions } from "../../../Shared/Hooks";
import { Skeleton } from "../Skeleton";
import { v4 as uuidv4 } from "uuid";
import "./customCard.css";

interface OptionsProps {
    buttonLink: string;
    buttonLabel: string;
}

interface CustomCardProps extends ViewProperties {
    caption: string;
    text: string[];
    icon: React.ReactElement;
    colour: TColour;
    linkButton?: OptionsProps;
    externalButton?: React.ReactElement;
}

const RenderIcon = (props: CustomCardProps) => {
    return React.cloneElement(props.icon, {
        ...props.icon.props,
        className: `custom-card-icon ${props.colour}`,
    });
};

const RenderLinkButton = (props: CustomCardProps): React.ReactElement => (
    <div className="mt-6">
        <Skeleton isLoading={props.isLoading} mode="Rect">
            <Link to={props.linkButton?.buttonLink ?? ""} className="link" rel="noopener nofollow">
                <button className="bulma-button" disabled={props.isLoading}>
                    {props.linkButton?.buttonLabel}
                </button>
            </Link>
        </Skeleton>
    </div>
);

const RenderExternalButton = (props: CustomCardProps): React.ReactElement => (
    <div className="mt-6">
        <Skeleton isLoading={props.isLoading} mode="Rect">
            {props.externalButton}
        </Skeleton>
    </div>
);

export const CustomCard = (props: CustomCardProps): React.ReactElement => {
    const media = useDimensions();

    return (
        <div className={`bulma-card ${media.isMobile ? "mx-4" : ""}`}>
            <div className="bulma-card-content">
                <div className="custom-card-icon-background is-flex is-justify-content-center is-align-items-center my-6">
                    <div className="custom-card-icon-holder is-flex is-justify-content-center is-align-items-center">
                        <Skeleton isLoading={props.isLoading} mode="Circle" width={72} height={72}>
                            <RenderIcon {...props} />
                        </Skeleton>
                    </div>
                </div>
                <Skeleton isLoading={props.isLoading} mode="Text" height={40}>
                    <h2 className="bulma-title has-text-centered">{props.caption}</h2>
                </Skeleton>
                <div className="my-5">
                    {props.text.map((value: string, _index: number) => (
                        <Skeleton isLoading={props.isLoading} mode="Text" height={14}>
                            <p className="is-size-6 has-text-centered line-height-20" key={uuidv4()}>
                                {value}
                            </p>
                        </Skeleton>
                    ))}
                </div>
                {props.linkButton ? <RenderLinkButton {...props} /> : null}
                {props.externalButton ? <RenderExternalButton {...props} /> : null}
                <div className={props.linkButton || props.externalButton ? "mb-2" : "mb-4"}></div>
            </div>
        </div>
    );
};
