import * as React from "react";
import { useSelector } from "react-redux";
import { ApplicationState } from "../../../../Store/Configuration";
import { AccessDeniedView } from "./View/accessDeniedView";

export const AccessDenied = (): JSX.Element => {
    const account = useSelector((state: ApplicationState) => state.contentAccount);
    const accessDeniedCaption = account.content?.sectionAccessDenied?.accessDeniedCaption;
    const accessDeniedPrompt = account.content?.sectionAccessDenied?.accessDeniedPrompt;
    const homeButtonText = account.content?.sectionAccessDenied?.homeButtonText;

    return (
        <AccessDeniedView 
            isLoading={account.isLoading}
            accessDeniedCaption={accessDeniedCaption}
            accessDeniedPrompt={accessDeniedPrompt}
            homeButtonText={homeButtonText}
        />
    );
}
