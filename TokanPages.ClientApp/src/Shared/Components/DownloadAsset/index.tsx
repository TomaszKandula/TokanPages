import * as React from "react";
import { IconButton } from "../IconButton";
import { Icon } from "../Icon/icon";
import { DownloadAssetProps } from "./Types";

export const DownloadAsset = (props: DownloadAssetProps): React.ReactElement => {
    const url = `${props.url}&canDownload=true`;
    const iconButtonSize = (props.size ?? 2.0) * 1.5;

    return (
        <IconButton hasGreyBackground size={iconButtonSize}>
            <a href={url}>
                <Icon name="Download" size={props.size ?? 1.5} className={props.className} />
            </a>
        </IconButton>
    );
};
