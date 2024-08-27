import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { BusinessForm } from "../../Components/Business";
import { Navigation } from "../../Components/Layout";

import {
    ContentNavigationAction,
    ContentFooterAction,
    ContentTemplatesAction,
} from "../../Store/Actions";

export const BusinessPage = () => {
    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentTemplatesAction.get());
    }, [language?.id]);

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <BusinessForm />
        </>
    );
};
