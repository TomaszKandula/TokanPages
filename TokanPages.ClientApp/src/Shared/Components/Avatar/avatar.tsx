import * as React from "react";
import { FigoureSize } from "../../../Shared/Enums";
import { AvatarProps } from "./Types";
import "./avatar.css";

const ConvertSize = (size: FigoureSize, prefix: string = "bulma"): string => {
    switch (size) {
        case FigoureSize.extrasmall:
            return `${prefix}-is-16x16`;
        case FigoureSize.small:
            return `${prefix}-is-24x24`;
        case FigoureSize.medium:
            return `${prefix}-is-32x32`;
        case FigoureSize.large:
            return `${prefix}-is-48x48`;
        case FigoureSize.extralarge:
            return `${prefix}-is-96x96`;
        case FigoureSize.superlarge:
            return `${prefix}-is-128x128`;
    }
};

export const Avatar = (props: AvatarProps): React.ReactElement => {
    const hasSrc = props.src && props.src !== "";

    if (!hasSrc && props.children) {
        const baseFlex = "is-flex is-justify-content-center is-align-items-center";
        const baseClass = `has-background-grey-light has-text-white ${baseFlex}`;
        const className = `${baseClass} ${ConvertSize(props.size, "avatar")} ${props.className ?? ""}`;
        return <div className={className}>{props.children}</div>;
    }

    const className = `${ConvertSize(props.size)} ${props.className ?? ""}`;

    return (
        <figure className={`bulma-image ${className}`}>
            <img
                alt={props.alt}
                title={props.title}
                src={props.src}
                className={`bulma-is-rounded is-round-border-light ${ConvertSize(props.size, "avatar")}`}
            />
        </figure>
    );
};
