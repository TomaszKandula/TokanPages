import * as React from "react";
import { useSelector } from "react-redux";
import { useHistory, useLocation } from "react-router";
import { Box, Breadcrumbs, Divider } from "@material-ui/core";
import { NavigateNext, Home } from "@material-ui/icons";
import { ApplicationState } from "../../../Store/Configuration";
import { Item, Subitem } from "../RenderMenu/Models";
import { StyledBreadcrumb } from "./customBreadcrumbStyle";
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

const useQuery = () => {
    return new URLSearchParams(useLocation().search);
};

//TODO: refactor this code!
const PathToItem = (pathname: string): Item | Subitem | undefined => {
    const navigation = useSelector((state: ApplicationState) => state.contentNavigation.content);

    let result = undefined;
    let itemCheck: Item | undefined = undefined;
    let subitemCheck = undefined;

    itemCheck = navigation.menu.items.find((item: Item) => {
        if (item.link === pathname) {
            result = item;
        } else if (item.subitems !== undefined) {
            subitemCheck = item.subitems.find((subitem: Subitem) => {
                if (subitem.link === pathname) {
                    result = subitem;
                }
            });

            console.debug(subitemCheck);
        }

        console.debug(itemCheck);
        return undefined;
    });

    return result;
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

export const CustomBreadcrumbView = (props: CustomBreadcrumbProps) => {
    const history = useHistory();
    const queryParam = useQuery();
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
                    label={PathToItem("/")?.value}
                    icon={<Home fontSize="small" />}
                    onClick={onBackToRoot}
                />
                <StyledBreadcrumb
                    component="div"
                    label={PathToItem(window.location.pathname)?.value}
                    onClick={onBackToPrevious}
                />
                {hasParam ? <StyledBreadcrumb component="div" label={toUpper(paramValue)} /> : null}
            </Breadcrumbs>
            <Box mt={props.mtDivider} mb={props.mbDivider}>
                <Divider />
            </Box>
        </Box>
    );
};
