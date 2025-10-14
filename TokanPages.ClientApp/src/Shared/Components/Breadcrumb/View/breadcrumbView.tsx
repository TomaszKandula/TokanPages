import * as React from "react";
import { PRERENDER_PATH_PREFIX } from "../../../Constants";
import { Icon } from "../../Icon";
import { CustomBreadcrumbViewProps, NavigationProps } from "./../Types";
import { getHomeText, pathToRootText, pathToSubitemText, toUpperCase } from "./../Utilities";
import { Skeleton } from "../../Skeleton";
import { v4 as uuidv4 } from "uuid";
import "./breadcrumbView.css";

const makeStyledBreadcrumb = (
    pathname: string,
    onClick: () => void,
    navigation: NavigationProps
): React.ReactElement[] | null => {
    const basePathWithLanguageId = `/${navigation.language}`;
    const path = pathname.replace(PRERENDER_PATH_PREFIX, "").replace(basePathWithLanguageId, "");

    const fragments = path.split("/").filter(e => String(e).trim());
    const rootName = pathToRootText({ pathname: path, navigation });
    const itemName = pathToSubitemText({ pathname: path, navigation });

    const setValue = (index: number): string => {
        if (index === 0) {
            return rootName.value;
        } else {
            return itemName;
        }
    };

    if (fragments !== undefined) {
        return fragments.map((_: string, index: number) => (
            <li className="py-1" key={uuidv4()} onClick={rootName.hasHash ? undefined : onClick}>
                <div className="mx-2">
                    <div className="custom-chip is-flex is-justify-content-flex-start is-align-items-center is-clickable">
                        <span className="p-2">{setValue(index)}</span>
                    </div>
                </div>
            </li>
        ));
    }

    return null;
};

export const BreadcrumbView = (props: CustomBreadcrumbViewProps): React.ReactElement => (
        <div className={`bulma-container bulma-is-max-tablet pt-6 ${props.media.isMobile ? "px-4" : ""}`}>
            <Skeleton isLoading={props.isLoading} mode="Text" width={200} height={18}>
                <nav className="bulma-breadcrumb bulma-has-arrow-separator">
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
                        {makeStyledBreadcrumb(window.location.pathname, props.onBackToPrevious, props.navigation)}
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
            </Skeleton>
            <hr className="my-5" />
        </div>
    );

