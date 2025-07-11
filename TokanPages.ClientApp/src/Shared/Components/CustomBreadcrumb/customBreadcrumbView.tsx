import * as React from "react";
import { useSelector } from "react-redux";
import { useHistory, useLocation } from "react-router-dom";
import { ItemDto, SubitemDto, UserInfoProps } from "../../../Api/Models";
import { ApplicationState } from "../../../Store/Configuration";
import { PRERENDER_PATH_PREFIX } from "../../../Shared/constants";
import { Icon } from "../Icon";
import { v4 as uuidv4 } from "uuid";
import Validate from "validate.js";
import "./customBreadcrumbView.css";

interface CustomBreadcrumbProps {
    isLoading: boolean;
    watchparam?: string;
}

interface NavigationProps {
    language: string;
    logo: string;
    userInfo: UserInfoProps;
    menu: {
        image: string;
        items: ItemDto[];
    };
}

interface PathProps {
    pathname: string;
    navigation: NavigationProps;
}

interface PathToRootTextResultProps {
    value: string;
    hasHash: boolean;
}

const useQuery = (): URLSearchParams => {
    return new URLSearchParams(useLocation().search);
};

const toUpper = (value: string | undefined): string => {
    if (value === undefined) {
        return "";
    }

    return value
        .toLowerCase()
        .split(" ")
        .map(function (word) {
            return word[0].toUpperCase() + word.substring(1);
        })
        .join(" ");
};

const getHomeText = (navigation: NavigationProps): string => {
    const text = navigation.menu.items.find((item: ItemDto) => {
        if (item.link === `/${navigation.language}`) {
            return item;
        }

        return undefined;
    });

    return text?.value ?? "";
};

const pathToRootText = (props: PathProps): PathToRootTextResultProps => {
    const array = props.pathname.split("/");
    const fragments = array.filter(e => String(e).trim());
    const rootWithHash = `#${fragments[0]}`;
    const rootWithSlash = `/${fragments[0]}`;
    let hasHash: boolean = false;

    const text = props.navigation.menu.items.find((item: ItemDto) => {
        if (item.link?.toUpperCase().includes(rootWithHash.toUpperCase())) {
            hasHash = true;
            return item;
        }

        if (item.link?.toUpperCase().includes(rootWithSlash.toUpperCase())) {
            return item;
        }

        return undefined;
    });

    return {
        value: text?.value ?? "",
        hasHash: hasHash,
    };
};

const pathToSubitemText = (props: PathProps): string => {
    const array = props.pathname.split("/");
    const fragments = array.filter(e => String(e).trim());
    const root = `#${fragments[0]}`;

    const itemWithSubitem = props.navigation.menu.items.find((item: ItemDto) => {
        if (item.link?.toUpperCase().includes(root.toUpperCase()) && item.subitems !== undefined) {
            return item;
        }

        return undefined;
    });

    if (itemWithSubitem?.subitems) {
        const text = itemWithSubitem?.subitems.find((subitem: SubitemDto) => {
            if (subitem.link?.toUpperCase().includes(props.pathname.toUpperCase())) {
                return subitem;
            }

            return undefined;
        });

        return text?.value ?? "";
    }

    return "";
};

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
            <li
                key={uuidv4()}
                onClick={rootName.hasHash ? undefined : onClick}
            >
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
        <div className="bulma-container bulma-is-max-tablet mt-6 pt-6">
            <nav className="bulma-breadcrumb bulma-has-arrow-separator">
                <ul>
                    <li>
                        <div className="mr-2">
                            <div onClick={onBackToRoot} className="custom-chip is-flex is-justify-content-flex-start is-align-items-center is-clickable">
                                <Icon name="Home" size={0.75} className="mx-1" />
                                <span className="pt-2 pr-2 pb-2">{getHomeText(navigation)}</span>
                            </div>
                        </div>
                    </li>
                    {makeStyledBreadcrumb(window.location.pathname, onBackToPrevious, navigation)}
                    {hasParam ? (
                        <li>
                            <div className="mx-2">
                                <div className="custom-chip is-flex is-justify-content-flex-start is-align-items-center">
                                    <span className="p-2">{toUpper(paramValue)}</span>
                                </div>
                            </div>
                        </li>
                    ) : null}
                </ul>
            </nav>
            <hr className="my-5" />
        </div>
    );
};
