import * as React from "react";
import { Icon as MdiIcon } from "@mdi/react";
import * as Icons from "@mdi/js";
import { IconProps } from "./types";
import { GithubIcon, InstgramIcon, LinkedinIcon } from "./Icons";

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

    const className = `${baseClass} ${props.className ?? ""}`;

    switch (props.name.toLocaleLowerCase()) {
        case "linkedin":
            return <LinkedinIcon size={props.size} onClick={props.onClick} />;
        case "github":
            return <GithubIcon size={props.size} onClick={props.onClick} />;
        case "instagram":
            return <InstgramIcon size={props.size} onClick={props.onClick} />;

        default:
            return (
                <div onClick={props.onClick}>
                    <MdiIcon path={getIconSvgPath(props.name)} size={props.size} className={className} />
                </div>
            );
    }
};
