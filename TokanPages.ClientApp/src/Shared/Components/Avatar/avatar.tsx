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
        return (
            <div className={`avatar-wrapper ${props.className}`}>
                {props.children}
            </div>
        );
    }

    return (
        <div className={`avatar-wrapper ${props.className}`}>
            <img 
                alt={props.alt} 
                title={props.title}
                src={props.src}
                className="avatar-image"
            />
        </div>
    )
};
