import * as React from "react";
import { useSelector } from "react-redux";
import { useHistory, useLocation } from "react-router-dom";
import { Skeleton } from "@material-ui/lab";
import { Breadcrumbs, Divider, Chip } from "@material-ui/core";
import { NavigateNext, Home } from "@material-ui/icons";
import { UserInfoProps } from "../../../Api/Models";
import { ApplicationState } from "../../../Store/Configuration";
import { PRERENDER_PATH_PREFIX } from "../../../Shared/constants";
import { Item, Subitem } from "../RenderMenu/Models";
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
        items: Item[];
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
    const text = navigation.menu.items.find((item: Item) => {
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

    const text = props.navigation.menu.items.find((item: Item) => {
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

    const itemWithSubitem = props.navigation.menu.items.find((item: Item) => {
        if (item.link?.toUpperCase().includes(root.toUpperCase()) && item.subitems !== undefined) {
            return item;
        }

        return undefined;
    });

    if (itemWithSubitem?.subitems) {
        const text = itemWithSubitem?.subitems.find((subitem: Subitem) => {
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
            <Chip
                key={uuidv4()}
                component="div"
                label={setValue(index)}
                onClick={rootName.hasHash ? undefined : onClick}
                className="custom-chip"
            />
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
        <div className="custom-breadcrumb-container">
            {props.isLoading ? (
                <Skeleton variant="rect" width={220} height={20} />
            ) : (
                <Breadcrumbs separator={<NavigateNext fontSize="small" />} aria-label="breadcrumb">
                    <Chip
                        component="div"
                        label={getHomeText(navigation)}
                        icon={<Home fontSize="small" />}
                        onClick={onBackToRoot}
                        className="custom-chip"
                    />
                    {makeStyledBreadcrumb(window.location.pathname, onBackToPrevious, navigation)}
                    {hasParam ? <Chip component="div" label={toUpper(paramValue)} className="custom-chip" /> : null}
                </Breadcrumbs>
            )}
            <div className="custom-breadcrumb-divider">
                <Divider />
            </div>
        </div>
    );
};
