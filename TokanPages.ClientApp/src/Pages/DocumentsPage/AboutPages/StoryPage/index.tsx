import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../../Store/Configuration";
import { ContentPageDataAction } from "../../../../Store/Actions";
import { TryPostStateSnapshot } from "../../../../Shared/Services/SpaCaching";
import { CustomBreadcrumb, DocumentContentWrapper } from "../../../../Shared/Components";
import { Navigation, Footer } from "../../../../Components/Layout";
import { Cookies } from "../../../../Components/Cookies";

export const StoryPage = (): React.ReactElement => {
    const dispatch = useDispatch();
    const state = useSelector((state: ApplicationState) => state);
    const language = state.applicationLanguage;
    const data = state.contentPageData;
    const story = state?.contentPageData?.components?.story;

    React.useEffect(() => {
        dispatch(ContentPageDataAction.request(["navigation", "footer", "story", "cookiesPrompt"], "StoryPage"));
    }, [language?.id]);

    const isLoading = data?.isLoading ?? false;
    const items = data?.components.story.items ?? [];

    React.useEffect(() => {
        if (story?.language !== "") {
            TryPostStateSnapshot(state);
        }
    }, [state]);

    return (
        <>
            <Navigation />
            <CustomBreadcrumb isLoading={isLoading} />
            <DocumentContentWrapper isLoading={isLoading} items={items} />
            <Cookies />
            <Footer />
        </>
    );
};
