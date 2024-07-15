import * as React from "react";
import { useSelector } from "react-redux";
import { useHistory } from "react-router";
import { Box, Breadcrumbs, Divider } from "@material-ui/core";
import { NavigateNext, Home } from "@material-ui/icons";
import { ApplicationState } from "../../../Store/Configuration";
import { Item } from "../RenderMenu/Models";
import { StyledBreadcrumb } from "./customBreadcrumbStyle";

interface CustomBreadcrumbProps {
    mt?: number;
    mb?: number;
    mr?: number;
    ml?: number;
    mtDivider?: number;
    mbDivider?: number;
}

const GetRootCaption = (items: Item[]): string => {
    let result = "";
    items.find((value) => {
        if (value.link === "/") {
            result = value.value;
        }

        return "";
    });

    return result;
}

export const CustomBreadcrumbView = (props: CustomBreadcrumbProps) => {
    const history = useHistory();
    const navigation = useSelector((state: ApplicationState) => state.contentNavigation.content);
    const navbar = useSelector((state: ApplicationState) => state.applicationNavbar);

    const onBackToRoot = React.useCallback(() => {
        history.push("/");
    }, []);

    const onBackToPrevious = React.useCallback(() => {
        if (navbar.path === undefined) {
            return;
        }

        history.push(navbar.path);

    }, [navbar.path]);

    return (
        <Box mt={props.mt} mb={props.mb} mr={props.mr} ml={props.ml}>
            <Breadcrumbs separator={<NavigateNext fontSize="small" />} aria-label="breadcrumb">
                <StyledBreadcrumb 
                    component="div"
                    label={GetRootCaption(navigation.menu.items)}
                    icon={<Home fontSize="small" />}
                    onClick={onBackToRoot}
                />
                <StyledBreadcrumb 
                    component="div"
                    label={navbar.name}
                    onClick={onBackToPrevious}
                />
            </Breadcrumbs>
            <Box mt={props.mtDivider} mb={props.mbDivider}>
                <Divider />
            </Box>
        </Box>
    );
}
