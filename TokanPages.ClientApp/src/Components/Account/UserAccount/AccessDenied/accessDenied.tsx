import * as React from "react";
import { useSelector } from "react-redux";
import { ApplicationState } from "../../../../Store/Configuration";
import { AccessDeniedView } from "./View/accessDeniedView";

export interface AccessDeniedProps {
    background?: React.CSSProperties;
}

export const AccessDenied = (props: AccessDeniedProps): React.ReactElement => {
    const contentPageData = useSelector((state: ApplicationState) => state.contentPageData);
    const languageId = useSelector((state: ApplicationState) => state.applicationLanguage.id);
    const accessDeniedCaption = contentPageData.components.account?.sectionAccessDenied?.accessDeniedCaption;
    const accessDeniedPrompt = contentPageData.components.account?.sectionAccessDenied?.accessDeniedPrompt;
    const homeButtonText = contentPageData.components.account?.sectionAccessDenied?.homeButtonText;

    return (
        <AccessDeniedView
            isLoading={contentPageData.isLoading}
            languageId={languageId}
            accessDeniedCaption={accessDeniedCaption}
            accessDeniedPrompt={accessDeniedPrompt}
            homeButtonText={homeButtonText}
            background={props.background}
        />
    );
};
