import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { ContentPageDataAction } from "../../../Store/Actions";
import { UserSignup } from "../../../Components/Account";
import { Navigation } from "../../../Components/Layout";
import { TryPostStateSnapshot } from "../../../Shared/Services/SpaCaching";

export const SignupPage = (): React.ReactElement => {
    const dispatch = useDispatch();
    const state = useSelector((state: ApplicationState) => state);
    const language = state.applicationLanguage;
    const accountUserSignup = state?.contentPageData?.components?.accountUserSignup;

    React.useEffect(() => {
        dispatch(
            ContentPageDataAction.request(
                ["layoutNavigation", "templates", "sectionCookiesPrompt", "accountUserSignup"],
                "SignupPage"
            )
        );
    }, [language?.id]);

    React.useEffect(() => {
        if (accountUserSignup?.language !== "") {
            TryPostStateSnapshot(state);
        }
    }, [state]);

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <main>
                <UserSignup className="pt-120 pb-240" />
            </main>
        </>
    );
};
