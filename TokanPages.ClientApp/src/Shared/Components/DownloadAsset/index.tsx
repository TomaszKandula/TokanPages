import * as React from "react";
import GetAppIcon from "@material-ui/icons/GetApp";

interface DownloadAssetProps {
    url: string;
}

export const DownloadAsset = (props: DownloadAssetProps): React.ReactElement => {
    const url = `${props.url}&canDownload=true`;
    return (
        <a href={url} style={{ color: "black" }}>
            <GetAppIcon />
        </a>
    );
};
