import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { ContentPageDataAction } from "../../Store/Actions";
import { TryPostStateSnapshot } from "../../Shared/Services/SpaCaching";
import { BusinessForm } from "../../Components/Business";
import { Navigation } from "../../Components/Layout";
import { Cookies } from "../../Components/Cookies";

export const BusinessPage = () => {
    const dispatch = useDispatch();
    const state = useSelector((state: ApplicationState) => state);
    const language = state.applicationLanguage;
    const businessForm = state?.contentPageData?.components?.businessForm;

    React.useEffect(() => {
        dispatch(ContentPageDataAction.request([
            "navigation", 
            "templates", 
            "cookiesPrompt", 
            "businessForm"
        ], "BusinessPage"));
    }, [language?.id]);

    React.useEffect(() => {
        if (businessForm?.language !== "") {
            TryPostStateSnapshot(state);
        }
    }, [state]);

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <BusinessForm
                pt={15}
                pb={30}
                hasCaption={false}
                hasIcon={true}
                hasShadow={true}
                background={{ backgroundColor: "#FCFCFC" }}
            />
            <Cookies />
        </>
    );
};
