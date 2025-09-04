import * as React from "react";
import { FigoureSize } from "../../../Shared/enums";
import "./avatar.css";

interface AvatarProps {
    alt: string;
    title: string;
    size: FigoureSize;
    src?: string;
    className?: string;
    children?: React.ReactElement | string;
}

const ConvertSize = (size: FigoureSize, prefix: string = "bulma"): string => {
    switch (size) {
        case FigoureSize.xs:
            return `${prefix}-is-16x16`;
        case FigoureSize.s:
            return `${prefix}-is-24x24`;
        case FigoureSize.m:
            return `${prefix}-is-32x32`;
        case FigoureSize.l:
            return `${prefix}-is-48x48`;
        case FigoureSize.xl:
            return `${prefix}-is-96x96`;
        case FigoureSize.xxl:
            return `${prefix}-is-128x128`;
    }
}

export const Avatar = (props: AvatarProps): React.ReactElement => {
    const hasSrc = props.src && props.src !== "";

    if (!hasSrc && props.children) {
        const baseFlex = "is-flex is-justify-content-center is-align-items-center";
        const baseClass = `has-background-grey-light has-text-white ${baseFlex}`;
        const className = `${baseClass} ${ConvertSize(props.size)} ${props.className ?? ""}`;
        return <div className={className}>{props.children}</div>;
    }

    const className = `${ConvertSize(props.size)} ${props.className ?? ""}`;

    return (
        <figure className={`bulma-image ${className}`}>
            <img alt={props.alt} title={props.title} src={props.src} className={`bulma-is-rounded ${ConvertSize(props.size, "avatar")}`} />
        </figure>
    );
};
