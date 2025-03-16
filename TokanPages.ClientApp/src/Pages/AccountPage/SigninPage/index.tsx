import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { ContentPageDataAction } from "../../../Store/Actions";
import { UserSignin } from "../../../Components/Account";
import { Navigation } from "../../../Components/Layout";
import { TryPostStateSnapshot } from "../../../Shared/Services/SpaCaching";

export const SigninPage = (): React.ReactElement => {
    const dispatch = useDispatch();
    const state = useSelector((state: ApplicationState) => state);
    const language = state.applicationLanguage;
    const accountUserSignin = state?.contentPageData?.components?.accountUserSignin;

    React.useEffect(() => {
        dispatch(
            ContentPageDataAction.request(
                ["navigation", "templates", "cookiesPrompt", "accountUserSignin"],
                "SigninPage"
            )
        );
    }, [language?.id]);

    React.useEffect(() => {
        if (accountUserSignin?.language !== "") {
            TryPostStateSnapshot(state);
        }
    }, [state]);

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <main>
                <UserSignin className="pt-120 pb-240" background="background-colour-light-grey" />
            </main>
        </>
    );
};
