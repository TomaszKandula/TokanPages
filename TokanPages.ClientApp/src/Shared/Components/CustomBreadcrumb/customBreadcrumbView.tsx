import * as React from "react";
import { useSelector } from "react-redux";
import { useHistory, useLocation } from "react-router";
import { Box, Breadcrumbs, Divider } from "@material-ui/core";
import { NavigateNext, Home } from "@material-ui/icons";
import { UserInfoProps } from "../../../Api/Models";
import { ApplicationState } from "../../../Store/Configuration";
import { Item, Subitem } from "../RenderMenu/Models";
import { StyledBreadcrumb } from "./customBreadcrumbStyle";
import { v4 as uuidv4 } from "uuid";
import Validate from "validate.js";

interface CustomBreadcrumbProps {
    mt?: number;
    mb?: number;
    mr?: number;
    ml?: number;
    mtDivider?: number;
    mbDivider?: number;
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
        if (item.link === "/") {
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
        if (item.link?.toUpperCase() === rootWithHash.toUpperCase()) {
            hasHash = true;
            return item;
        }

        if (item.link?.toUpperCase() === rootWithSlash.toUpperCase()) {
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
        if (item.link?.toUpperCase() === root.toUpperCase() && item.subitems !== undefined) {
            return item;
        }

        return undefined;
    });

    if (itemWithSubitem?.subitems) {
        const text = itemWithSubitem?.subitems.find((subitem: Subitem) => {
            if (subitem.link?.toUpperCase() === props.pathname.toUpperCase()) {
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
    let fragments = pathname.split("/");
    fragments = fragments.filter(e => String(e).trim());

    const rootName = pathToRootText({ pathname, navigation });
    const itemName = pathToSubitemText({ pathname, navigation });

    const setValue = (index: number): string => {
        if (index === 0) {
            return rootName.value;
        } else {
            return itemName;
        }
    };

    if (fragments !== undefined) {
        return fragments.map((_: string, index: number) => (
            <StyledBreadcrumb
                key={uuidv4()}
                component="div"
                label={setValue(index)}
                onClick={rootName.hasHash ? undefined : onClick}
            />
        ));
    }

    return null;
};

export const CustomBreadcrumbView = (props: CustomBreadcrumbProps): React.ReactElement => {
    const history = useHistory();
    const queryParam = useQuery();
    const navigation = useSelector((state: ApplicationState) => state.contentPageData.components.navigation);

    const param = queryParam.get(props.watchparam ?? "");
    const hasParam = !Validate.isEmpty(param);
    let paramValue = param?.replaceAll("-", " ");

    const onBackToRoot = React.useCallback(() => {
        history.push("/");
    }, []);

    const onBackToPrevious = React.useCallback(() => {
        history.push(window.location.pathname);
    }, [window.location.pathname]);

    return (
        <Box mt={props.mt} mb={props.mb} mr={props.mr} ml={props.ml}>
            <Breadcrumbs separator={<NavigateNext fontSize="small" />} aria-label="breadcrumb">
                <StyledBreadcrumb
                    component="div"
                    label={getHomeText(navigation)}
                    icon={<Home fontSize="small" />}
                    onClick={onBackToRoot}
                />
                {makeStyledBreadcrumb(window.location.pathname, onBackToPrevious, navigation)}
                {hasParam ? <StyledBreadcrumb component="div" label={toUpper(paramValue)} /> : null}
            </Breadcrumbs>
            <Box mt={props.mtDivider} mb={props.mbDivider}>
                <Divider />
            </Box>
        </Box>
    );
};
