import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import { ApplicationState } from "../../../Store/Configuration";
import { ContentPageDataAction } from "../../../Store/Actions";
import { useUnhead } from "../../../Shared/Hooks";
import { AccountActivate } from "../../../Components/Account";
import { Navigation } from "../../../Components/Layout";

const useQuery = () => {
    return new URLSearchParams(useLocation().search);
};

export const ActivationPage = (): React.ReactElement => {
    useUnhead("account activation");

    const queryParam = useQuery();
    const dispatch = useDispatch();
    const id = queryParam.get("id") ?? "";
    const type = queryParam.get("type") ?? "";

    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(
            ContentPageDataAction.request(
                ["layoutNavigation", "templates", "sectionCookiesPrompt", "accountSettings", "accountActivate"],
                "ActivationPage"
            )
        );
    }, [language?.id]);

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <main>
                <AccountActivate id={id} type={type} className="pt-120 pb-240" />
            </main>
        </>
    );
};
