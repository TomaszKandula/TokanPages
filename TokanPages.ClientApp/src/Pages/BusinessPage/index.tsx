import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { BusinessForm } from "../../Components/Business";
import { Navigation } from "../../Components/Layout";
import { Colours } from "../../Theme";

import {
    ContentNavigationAction,
    ContentFooterAction,
    ContentTemplatesAction,
    ContentBusinessFormAction,
} from "../../Store/Actions";

export const BusinessPage = () => {
    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentBusinessFormAction.get());
        dispatch(ContentTemplatesAction.get());
    }, [language?.id]);

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <BusinessForm 
                pt={15}
                pb={30}
                hasCaption={false}
                hasIcon={true}
                hasShadow={true}
                background={{ backgroundColor: Colours.colours.lightGray3 }}
            />
        </>
    );
};
