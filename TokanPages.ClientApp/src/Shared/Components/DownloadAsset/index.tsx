import * as React from "react";
import { IconButton } from "../IconButton";
import { Icon } from "../Icon/icon";
import { DownloadAssetProps } from "./Types";

export const DownloadAsset = (props: DownloadAssetProps): React.ReactElement => {
    const url = `${props.url}&canDownload=true`;
    return (
        <IconButton>
            <a href={url}>
                <Icon name="Download" size={props.size ?? 1.1} className={props.className} />
            </a>
        </IconButton>
    );
};
