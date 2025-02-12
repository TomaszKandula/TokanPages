import * as React from "react";
import { useSelector } from "react-redux";
import { ApplicationState } from "../../../../Store/Configuration";
import { AccessDeniedView } from "./View/accessDeniedView";

export interface AccessDeniedProps {
    background?: string;
}

export const AccessDenied = (props: AccessDeniedProps): React.ReactElement => {
    const contentPageData = useSelector((state: ApplicationState) => state.contentPageData);
    const languageId = useSelector((state: ApplicationState) => state.applicationLanguage.id);
    const accessDeniedCaption = contentPageData.components.accountSettings?.sectionAccessDenied?.accessDeniedCaption;
    const accessDeniedPrompt = contentPageData.components.accountSettings?.sectionAccessDenied?.accessDeniedPrompt;
    const homeButtonText = contentPageData.components.accountSettings?.sectionAccessDenied?.homeButtonText;

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
