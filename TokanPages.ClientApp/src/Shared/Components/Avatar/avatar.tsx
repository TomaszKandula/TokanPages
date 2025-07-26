import * as React from "react";

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
        return <div className={props.className}>{props.children}</div>;
    }

    return (
        <figure className={`bulma-image ${props.className ?? ""}`}>
            <img alt={props.alt} title={props.title} src={props.src} className="bulma-is-rounded" />
        </figure>
    );
};
