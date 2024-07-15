import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { Navigation, Footer } from "../../../Components/Layout";
import { CustomBreadcrumb, DocumentContentWrapper } from "../../../Shared/Components";

import {
    ContentNavigationAction,
    ContentFooterAction,
    ContentDocumentAction,
    ContentTemplatesAction,
} from "../../../Store/Actions";

export const FootballPage = (): JSX.Element => {
    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);
    const document = useSelector((state: ApplicationState) => state.contentDocument);

    React.useEffect(() => {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentDocumentAction.getFootball());
        dispatch(ContentTemplatesAction.get());
    }, [language?.id]);

    const isLoading = document?.contentFootball?.isLoading ?? false;
    const items = document?.contentFootball?.content.items ?? [];

    return (
        <>
            <Navigation />
            <CustomBreadcrumb mt={12} mb={2} mr={5} ml={5} mtDivider={4} mbDivider={4} />
            <DocumentContentWrapper isLoading={isLoading} items={items} />
            <Footer />
        </>
    );
};
