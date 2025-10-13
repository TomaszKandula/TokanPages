import * as React from "react";
import { useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import { ApplicationState } from "../../../Store/Configuration";
import { PRERENDER_PATH_PREFIX } from "../../../Shared/Constants";
import { useDimensions, useQuery } from "../../../Shared/Hooks";
import { Icon } from "../Icon";
import { CustomBreadcrumbProps, NavigationProps } from "./Types";
import { getHomeText, pathToRootText, pathToSubitemText, toUpperCase } from "./Utilities";
import { v4 as uuidv4 } from "uuid";
import Validate from "validate.js";
import "./customBreadcrumbView.css";
import { Skeleton } from "../Skeleton";

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

export const CustomBreadcrumbView = (props: CustomBreadcrumbProps): React.ReactElement => {
    const media = useDimensions();
    const history = useHistory();
    const queryParam = useQuery();
    const navigation = useSelector((state: ApplicationState) => state.contentPageData.components.layoutNavigation);

    const param = queryParam.get(props.watchparam ?? "");
    const hasParam = !Validate.isEmpty(param);
    let paramValue = param?.replaceAll("-", " ");

    const onBackToRoot = React.useCallback(() => {
        history.push(`/${navigation.language}`);
    }, [navigation.language]);

    const onBackToPrevious = React.useCallback(() => {
        history.push(window.location.pathname);
    }, [window.location.pathname]);

    return (
        <div className={`bulma-container bulma-is-max-tablet pt-6 ${media.isMobile ? "px-4" : ""}`}>
            <Skeleton isLoading={props.isLoading} mode="Text" width={200} height={18}>
                <nav className="bulma-breadcrumb bulma-has-arrow-separator">
                    <ul>
                        <li className="py-1">
                            <div className="mr-2">
                                <div
                                    onClick={onBackToRoot}
                                    className="custom-chip is-flex is-justify-content-flex-start is-align-items-center is-clickable"
                                >
                                    <Icon name="Home" size={1.0} className="mx-1" />
                                    <span className="pt-2 pr-2 pb-2">{getHomeText(navigation)}</span>
                                </div>
                            </div>
                        </li>
                        {makeStyledBreadcrumb(window.location.pathname, onBackToPrevious, navigation)}
                        {hasParam ? (
                            <li className="py-1">
                                <div className="mx-2">
                                    <div className="custom-chip is-flex is-justify-content-flex-start is-align-items-center">
                                        <span className="p-2">{toUpperCase(paramValue)}</span>
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
};
