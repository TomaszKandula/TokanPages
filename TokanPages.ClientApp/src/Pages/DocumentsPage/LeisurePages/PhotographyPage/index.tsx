import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../../Store/Configuration";
import { ContentPageDataAction } from "../../../../Store/Actions";
import { TryPostStateSnapshot } from "../../../../Shared/Services/SpaCaching";
import { CustomBreadcrumb, DocumentContentWrapper } from "../../../../Shared/Components";
import { Navigation, Footer } from "../../../../Components/Layout";

export const PhotographyPage = (): React.ReactElement => {
    const dispatch = useDispatch();
    const state = useSelector((state: ApplicationState) => state);
    const language = state.applicationLanguage;
    const data = state.contentPageData;
    const photography = state?.contentPageData?.components?.photography;

    React.useEffect(() => {
        dispatch(
            ContentPageDataAction.request(["navigation", "footer", "cookiesPrompt", "photography"], "PhotographyPage")
        );
    }, [language?.id]);

    const isLoading = data?.isLoading ?? false;
    const items = data?.components.photography.items ?? [];

    React.useEffect(() => {
        if (photography?.language !== "") {
            TryPostStateSnapshot(state);
        }
    }, [state]);

    return (
        <>
            <Navigation />
            <main>
                <CustomBreadcrumb isLoading={isLoading} />
                <DocumentContentWrapper isLoading={isLoading} items={items} />
            </main>
            <Footer />
        </>
    );
};
