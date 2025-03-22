import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { ContentPageDataAction } from "../../Store/Actions";
import { TryPostStateSnapshot } from "../../Shared/Services/SpaCaching";
import { BusinessForm } from "../../Components/Business";
import { Navigation } from "../../Components/Layout";

export const BusinessPage = () => {
    const dispatch = useDispatch();
    const state = useSelector((state: ApplicationState) => state);
    const language = state.applicationLanguage;
    const businessForm = state?.contentPageData?.components?.pageBusinessForm;

    React.useEffect(() => {
        dispatch(
            ContentPageDataAction.request(
                ["layoutNavigation", "templates", "sectionCookiesPrompt", "pageBusinessForm"],
                "BusinessPage"
            )
        );
    }, [language?.id]);

    React.useEffect(() => {
        if (businessForm?.language !== "") {
            TryPostStateSnapshot(state);
        }
    }, [state]);

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <main>
                <BusinessForm hasCaption={false} hasIcon={true} hasShadow={true} className="pt-120 pb-240" />
            </main>
        </>
    );
};
