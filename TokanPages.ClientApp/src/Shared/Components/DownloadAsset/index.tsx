import * as React from "react";
import GetAppIcon from "@material-ui/icons/GetApp";
import { Colours } from "../../../Theme";

interface DownloadAssetProps {
    url: string;
}

export const DownloadAsset = (props: DownloadAssetProps): JSX.Element => {
    const url = `${props.url}&canDownload=true`;
    return (
        <a href={url} style={{ color: Colours.colours.black }}>
            <GetAppIcon />
        </a>
    );
} 
