import * as React from "react";
import { Icon } from "../../Icon";
import { Skeleton } from "../../Skeleton";
import { PRERENDER_PATH_PREFIX } from "../../../Constants";
import { BreadcrumbViewProps, MakeStyledBreadcrumbProps } from "./../Types";
import { getHomeText, pathToRootText, pathToSubitemText, toUpperCase } from "./../Utilities";
import { DownloadAsset } from "../../../../Shared/Components/DownloadAsset";
import { v4 as uuidv4 } from "uuid";
import Validate from "validate.js";
import "./breadcrumbView.css";

const MakeStyledBreadcrumb = (props: MakeStyledBreadcrumbProps): React.ReactElement | null => {
    const basePathWithLanguageId = `/${props.navigation.language}`;
    const path = props.pathname.replace(PRERENDER_PATH_PREFIX, "").replace(basePathWithLanguageId, "");

    const fragments = path.split("/").filter(e => String(e).trim());
    const rootName = pathToRootText({ pathname: path, navigation: props.navigation });
    const itemName = pathToSubitemText({ pathname: path, navigation: props.navigation });

    const setValue = (index: number): string => {
        if (index === 0) {
            return rootName.value;
        } else {
            return itemName;
        }
    };

    if (fragments !== undefined) {
        const list = fragments.map((_: string, index: number) => (
            <li className="py-1" key={uuidv4()} onClick={rootName.hasHash ? undefined : props.onClick}>
                <div className="mx-2">
                    <div className="custom-chip is-flex is-justify-content-flex-start is-align-items-center is-clickable">
                        <span className="p-2">{setValue(index)}</span>
                    </div>
                </div>
            </li>
        ));

        return <>{list}</>;
    }

    return null;
};

export const BreadcrumbView = (props: BreadcrumbViewProps): React.ReactElement => (
    <div className={`bulma-container bulma-is-max-tablet pt-6 ${props.media.isMobile ? "px-4" : ""}`}>
        <Skeleton isLoading={props.isLoading} mode="Text" width={200} height={18}>
            <div className="is-flex is-justify-content-space-between">
                <nav className="bulma-breadcrumb bulma-has-arrow-separator m-0">
                    <ul>
                        <li className="py-1">
                            <div className="mr-2">
                                <div
                                    onClick={props.onBackToRoot}
                                    className="custom-chip is-flex is-justify-content-flex-start is-align-items-center is-clickable"
                                >
                                    <Icon name="Home" size={1.0} className="mx-1" />
                                    <span className="pt-2 pr-2 pb-2">{getHomeText(props.navigation)}</span>
                                </div>
                            </div>
                        </li>
                        <MakeStyledBreadcrumb
                            pathname={window.location.pathname}
                            navigation={props.navigation}
                            onClick={props.onBackToPrevious}
                        />
                        {props.hasParam ? (
                            <li className="py-1">
                                <div className="mx-2">
                                    <div className="custom-chip is-flex is-justify-content-flex-start is-align-items-center">
                                        <span className="p-2">{toUpperCase(props.paramValue)}</span>
                                    </div>
                                </div>
                            </li>
                        ) : null}
                    </ul>
                </nav>
                {!Validate.isEmpty(props.downloadUrl) ? (
                    <DownloadAsset url={props.downloadUrl ?? ""} size={1.5} className="has-text-black" />
                ) : null}
            </div>
        </Skeleton>
        <hr className="my-5" />
    </div>
);
