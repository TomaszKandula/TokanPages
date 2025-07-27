import * as React from "react";
import "./avatar.css";

interface AvatarProps {
    alt: string;
    title: string;
    src?: string;
    className?: string;
    children?: React.ReactElement | string;
}

export const Avatar = (props: AvatarProps): React.ReactElement => {
    const hasSrc = props.src && props.src !== "";

    if (!hasSrc && props.children) {
        const baseClass = "has-background-grey-light is-flex is-justify-content-center is-align-items-center";
        let className;
        if (props.className?.includes("96x96")) {
            className = `${baseClass} default-avatar-large`;
        } else {
            className = `${baseClass} default-avatar-small`;
        }

        return <div className={className}>{props.children}</div>;
    }

    return (
        <figure className={`bulma-image ${props.className ?? ""}`}>
            <img alt={props.alt} title={props.title} src={props.src} className="bulma-is-rounded" />
        </figure>
    );
};
