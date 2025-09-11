import React from "react";
import { ResumeView } from "./View/resumeView";
import { useSelector } from "react-redux";
import { ApplicationState } from "Store/Configuration";

export const Resume = (): React.ReactElement => {
    const content = useSelector((state: ApplicationState) => state.contentPageData.components.pageResume);
    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const isContentLoading = data.isLoading;

    return <ResumeView isLoading={isContentLoading} content={content} />;
};
