import * as React from "react";
import { useSelector } from "react-redux";
import { useHistory } from "react-router";
import { Box, Breadcrumbs, Divider } from "@material-ui/core";
import { NavigateNext, Home } from "@material-ui/icons";
import { ApplicationState } from "../../../Store/Configuration";
import { Item, Subitem } from "../RenderMenu/Models";
import { StyledBreadcrumb } from "./customBreadcrumbStyle";

interface CustomBreadcrumbProps {
    mt?: number;
    mb?: number;
    mr?: number;
    ml?: number;
    mtDivider?: number;
    mbDivider?: number;
}

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
}

export const CustomBreadcrumbView = (props: CustomBreadcrumbProps) => {
    const history = useHistory();

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
            </Breadcrumbs>
            <Box mt={props.mtDivider} mb={props.mbDivider}>
                <Divider />
            </Box>
        </Box>
    );
}
