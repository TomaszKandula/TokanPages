import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../../Store/Configuration";
import { ContentPageDataAction } from "../../../../Store/Actions";
import { CustomBreadcrumb, DocumentContentWrapper } from "../../../../Shared/Components";
import { useSnapshot, useUnhead } from "../../../../Shared/Hooks";
import { Navigation, Footer } from "../../../../Components/Layout";

export const GuitarPage = (): React.ReactElement => {
    useUnhead("GuitarPage");
    useSnapshot();

    const dispatch = useDispatch();
    const state = useSelector((state: ApplicationState) => state);
    const language = state.applicationLanguage;
    const data = state.contentPageData;

    React.useEffect(() => {
        dispatch(
            ContentPageDataAction.request(
                ["layoutNavigation", "layoutFooter", "sectionCookiesPrompt", "leisureGuitar"],
                "GuitarPage"
            )
        );
    }, [language?.id]);

    const isLoading = data?.isLoading ?? false;
    const items = data?.components.leisureGuitar.items ?? [];

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
