import * as React from "react";
import { Link } from "react-router-dom";
import { ViewProperties } from "../../../Shared/Abstractions";
import "./customCard.css";

type Colour = "info" | "success" | "warning" | "error";

interface OptionsProps {
    buttonLink: string;
    buttonLabel: string;
}

interface CustomCardProps extends ViewProperties {
    caption: string;
    text: string[];
    icon: React.ReactElement;
    colour: Colour;
    linkButton?: OptionsProps;
    externalButton?: React.ReactElement;
}

const RenderIcon = (props: CustomCardProps) => {
    return React.cloneElement(props.icon, {
        ...props.icon.props,
        className: `custom-card-icon vertical-centre alert-${props.colour}`,
    });
};

const RenderLinkButton = (props: CustomCardProps): React.ReactElement => (
    <div className="mt-48">
        <Link to={props.linkButton?.buttonLink ?? ""} className="link" rel="noopener nofollow">
            <button className={`bulma-button ${props.isLoading ? "bulma-is-skeleton" : ""}`} disabled={props.isLoading}>
                {props.linkButton?.buttonLabel}
            </button>
        </Link>
    </div>
);

const RenderExternalButton = (props: CustomCardProps): React.ReactElement => (
    <div className={`mt-48 ${props.isLoading ? "bulma-is-skeleton" : ""}`}>{props.externalButton}</div>
);

export const CustomCard = (props: CustomCardProps): React.ReactElement => {
    return (
        <div className="bulma-card">
            <div className="bulma-card-content">
                <div className="custom-card-icon-background my-6">
                    <div className="custom-card-icon-holder vertical-centre">
                        <RenderIcon {...props} />
                    </div>
                </div>
                <h2 className={`bulma-title has-text-centered ${props.isLoading ? "bulma-is-skeleton" : ""}`}>
                    {props.caption}
                </h2>
                <div className="my-5">
                    {props.text.map((value: string, index: number) => (
                        <p
                            className={`is-size-6 has-text-centered line-height-20 ${props.isLoading ? "bulma-is-skeleton" : ""}`}
                            key={index}
                        >
                            {value}
                        </p>
                    ))}
                </div>
                {props.linkButton ? <RenderLinkButton {...props} /> : null}
                {props.externalButton ? <RenderExternalButton {...props} /> : null}
                <div className={props.linkButton || props.externalButton ? "mb-25" : "mb-48"}></div>
            </div>
        </div>
    );
};
