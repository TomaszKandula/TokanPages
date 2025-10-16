import * as React from "react";
import { useHistory } from "react-router-dom";
import { useSelector } from "react-redux";
import { useDimensions, useQuery } from "../../../Shared/Hooks";
import { ApplicationState } from "../../../Store/Configuration";
import { BreadcrumbView } from "./View/breadcrumbView";
import { BreadcrumbProps } from "./Types";
import Validate from "validate.js";

export const Breadcrumb = (props: BreadcrumbProps): React.ReactElement => {
    const media = useDimensions();
    const history = useHistory();
    const queryParam = useQuery();
    const navigation = useSelector((state: ApplicationState) => state.contentPageData.components.layoutNavigation);

    const param = queryParam.get(props.watchparam ?? "");
    const hasParam = !Validate.isEmpty(param);
    const paramValue = param?.replaceAll("-", " ");

    const onBackToRoot = React.useCallback(() => {
        history.push(`/${navigation.language}`);
    }, [navigation.language]);

    const onBackToPrevious = React.useCallback(() => {
        history.push(window.location.pathname);
    }, [window.location.pathname]);

    return (
        <BreadcrumbView
            isLoading={props.isLoading}
            media={media}
            navigation={navigation}
            hasParam={hasParam}
            paramValue={paramValue}
            onBackToRoot={onBackToRoot}
            onBackToPrevious={onBackToPrevious}
        />
    );
};
