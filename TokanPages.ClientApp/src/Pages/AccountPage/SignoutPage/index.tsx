import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { ApplicationState } from "../../../Store/Configuration";
import { ContentPageDataAction } from "../../../Store/Actions";
import { UserSignout } from "../../../Components/Account";
import { Navigation } from "../../../Components/Layout";

export const SignoutPage = (): React.ReactElement => {
    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(
            ContentPageDataAction.request(
                ["navigation", "templates", "cookiesPrompt", "accountUserSignout"],
                "SignoutPage"
            )
        );
    }, [language?.id]);

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <main>
                <UserSignout className="pt-120 pb-240" background="background-colour-light-grey" />
            </main>
        </>
    );
};
