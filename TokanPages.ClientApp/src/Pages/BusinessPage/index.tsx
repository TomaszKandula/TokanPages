import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../Store/Configuration";
import { ContentPageDataAction } from "../../Store/Actions";
import { useSnapshot, useUnhead } from "../../Shared/Hooks";
import { BusinessForm } from "../../Components/Business";
import { Navigation } from "../../Components/Layout";

export const BusinessPage = () => {
    useUnhead("BusinessPage");
    useSnapshot();

    const dispatch = useDispatch();
    const state = useSelector((state: ApplicationState) => state);
    const language = state.applicationLanguage;

    React.useEffect(() => {
        dispatch(
            ContentPageDataAction.request(
                ["layoutNavigation", "templates", "sectionCookiesPrompt", "pageBusinessForm"],
                "BusinessPage"
            )
        );
    }, [language?.id]);

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <main>
                <BusinessForm hasCaption={false} hasIcon={true} hasShadow={true} className="pt-120 pb-240" />
            </main>
        </>
    );
};
