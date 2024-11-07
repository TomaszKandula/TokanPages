import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { ContentPageDataAction } from "../../../Store/Actions";
import { TrySnapshotState } from "../../../Shared/Services/SpaCaching";
import { Navigation, Footer } from "../../../Components/Layout";
import { CustomBreadcrumb, DocumentContentWrapper } from "../../../Shared/Components";

export const ShowcasePage = (): React.ReactElement => {
    const dispatch = useDispatch();
    const state = useSelector((state: ApplicationState) => state);
    const language = state.applicationLanguage;
    const data = state.contentPageData;
    const showcase = state?.contentPageData?.components?.showcase;

    React.useEffect(() => {
        dispatch(ContentPageDataAction.request(["navigation", "footer", "showcase"], "ShowcasePage"));
    }, [language?.id]);

    const isLoading = data?.isLoading ?? false;
    const items = data?.components.showcase.items ?? [];

    React.useEffect(() => {
        if (showcase?.language !== "") {
            TrySnapshotState(state);
        }
    }, [state]);

    return (
        <>
            <Navigation />
            <CustomBreadcrumb mt={12} mb={2} mr={5} ml={5} mtDivider={4} mbDivider={4} />
            <DocumentContentWrapper isLoading={isLoading} items={items} />
            <Footer />
        </>
    );
};
