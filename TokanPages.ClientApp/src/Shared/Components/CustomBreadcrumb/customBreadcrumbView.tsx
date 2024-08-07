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

const PathToItem = (pathname: string): Item | Subitem | undefined => {
    const navigation = useSelector((state: ApplicationState) => state.contentNavigation.content);

    let result = undefined;
    navigation.menu.items.find((value: Item) => {
        if (value.link === pathname) {
            result = value;
        } else if (value.subitems !== undefined) {
            value.subitems.find((value: Subitem) => {
                if (value.link === pathname) {
                    result = value;
                }
            });
        }

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
                    onClick={() => history.push(window.location.pathname)}
                />
                {hasParam ? <StyledBreadcrumb component="div" label={toUpper(paramValue)} /> : null}
            </Breadcrumbs>
            <Box mt={props.mtDivider} mb={props.mbDivider}>
                <Divider />
            </Box>
        </Box>
    );
};
