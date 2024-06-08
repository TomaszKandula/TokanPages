import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { Navigation, Footer } from "../../../Components/Layout";
import { DocumentContentWrapper } from "../../../Shared/Components";

import {
    ContentNavigationAction,
    ContentFooterAction,
    ContentDocumentAction,
    ContentTemplatesAction,
} from "../../../Store/Actions";

export const PhotographyPage = (): JSX.Element => {
    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);
    const document = useSelector((state: ApplicationState) => state.contentDocument);

    React.useEffect(() => {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentDocumentAction.getPhotography());
        dispatch(ContentTemplatesAction.get());
    }, [language?.id]);

    const isLoading = document?.contentPhotography?.isLoading ?? false;
    const items = document?.contentPhotography?.content.items ?? [];

    return (
        <>
            <Navigation />
            <DocumentContentWrapper isLoading={isLoading} items={items} />
            <Footer />
        </>
    );
};
