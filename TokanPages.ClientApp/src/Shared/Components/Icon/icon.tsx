import * as React from "react";
import { Icon as MdiIcon } from "@mdi/react";
import * as Icons from "@mdi/js";

interface IconProps {
    name: string;
    size: number;
    colour?: string;
    className?: string;
}

const getIconSvgPath = (item: string | undefined): string => {
    const key = `mdi${item ?? ""}`;
    // @ts-expect-error
    const svg = Icons[key];
    return svg;
};

export const Icon = (props: IconProps) => (
    <MdiIcon path={getIconSvgPath(props.name)} size={props.size} className={props.className} color={props.colour} />
);
