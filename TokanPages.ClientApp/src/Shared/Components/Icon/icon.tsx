import * as React from "react";
import { Icon as MdiIcon } from "@mdi/react";
import * as Icons from "@mdi/js";
import { gitHubPath, instagramPath, linkedInPath } from "./paths";

interface IconBaseProps {
    size?: number;
    onClick?: () => void;
}

interface IconProps extends IconBaseProps {
    name: string;
    size: number;
    colour?: string;
    className?: string;
}

const LinkedinIcon = (props: IconBaseProps): React.ReactElement => (
    <svg 
        fill="#000000" 
        width={`${props.size}rem`} 
        height={`${props.size}rem`} 
        viewBox="0 0 32 32" 
        version="1.1" xmlns="http://www.w3.org/2000/svg"
        onClick={props.onClick}
    >
        <path d={linkedInPath}></path>
    </svg>
);

const GithubIcon = (props: IconBaseProps): React.ReactElement => (
    <svg 
        width={`${props.size}rem`} 
        height={`${props.size}rem`} 
        viewBox="0 0 48 48" 
        fill="none" 
        xmlns="http://www.w3.org/2000/svg"
        onClick={props.onClick}
    >
        <circle cx="24" cy="24" r="20" fill="#181717"/>
        <path d={gitHubPath} fill="white"/>
    </svg>
);

const InstgramIcon = (props: IconBaseProps): React.ReactElement => (
    <svg 
        width={`${props.size}rem`} 
        height={`${props.size}rem`} 
        viewBox="0 0 24 24" 
        fill="none" 
        xmlns="http://www.w3.org/2000/svg"
        onClick={props.onClick}
    >
        <path fillRule="evenodd" clipRule="evenodd" d={instagramPath} fill="#000000"/>
    </svg>
);

const getIconSvgPath = (item: string | undefined): string => {
    const key = `mdi${item ?? ""}`;
    // @ts-expect-error
    const svg = Icons[key];
    return svg;
};

const baseClass = "is-flex is-align-self-center";

export const Icon = (props: IconProps): React.ReactElement => { 
    if (props.name === "" || props.name === undefined) {
        return <></>;
    }

    switch (props.name.toLocaleLowerCase()) {
        case "linkedin": return <LinkedinIcon size={props.size} onClick={props.onClick} />;
        case "github": return <GithubIcon size={props.size} onClick={props.onClick} />;
        case "instagram": return <InstgramIcon size={props.size} onClick={props.onClick} />;
        default: return (
            <div onClick={props.onClick}>
                <MdiIcon
                    path={getIconSvgPath(props.name)}
                    size={props.size}
                    className={`${baseClass} ${props.className}`}
                    color={props.colour} />
            </div>
        );
    }
};
