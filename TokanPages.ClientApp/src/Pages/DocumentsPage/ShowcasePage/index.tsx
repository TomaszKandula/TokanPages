import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { ContentPageDataAction } from "../../../Store/Actions";
import { TryPostStateSnapshot } from "../../../Shared/Services/SpaCaching";
import { CustomBreadcrumb, DocumentContentWrapper } from "../../../Shared/Components";
import { useUnhead } from "../../../Shared/Hooks";
import { Navigation, Footer } from "../../../Components/Layout";

export const ShowcasePage = (): React.ReactElement => {
    useUnhead("showcase");

    const dispatch = useDispatch();
    const state = useSelector((state: ApplicationState) => state);
    const language = state.applicationLanguage;
    const data = state.contentPageData;
    const showcase = state?.contentPageData?.components?.pageShowcase;

    React.useEffect(() => {
        dispatch(
            ContentPageDataAction.request(
                ["layoutNavigation", "layoutFooter", "pageShowcase", "sectionCookiesPrompt"],
                "ShowcasePage"
            )
        );
    }, [language?.id]);

    const isLoading = data?.isLoading ?? false;
    const items = data?.components.pageShowcase.items ?? [];

    React.useEffect(() => {
        if (showcase?.language !== "") {
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
