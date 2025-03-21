import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import { Typography } from "@material-ui/core";
import { ApplicationState } from "../../../Store/Configuration";
import { ContentPageDataAction } from "../../../Store/Actions";
import { AccountActivate } from "../../../Components/Account";
import { Navigation } from "../../../Components/Layout";

const useQuery = () => {
    return new URLSearchParams(useLocation().search);
};

const ErrorMessage = (): React.ReactElement => {
    return (
        <div className="mt-80 mb-40">
            <Typography>Uuuppss..., there is a missing ID...</Typography>
        </div>
    );
};

export const ActivationPage = (): React.ReactElement => {
    const queryParam = useQuery();
    const dispatch = useDispatch();
    const id = queryParam.get("id");
    const type = queryParam.get("type") as string;

    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(
            ContentPageDataAction.request(
                ["layoutNavigation", "templates", "sectionCookiesPrompt", "accountActivate"],
                "ActivationPage"
            )
        );
    }, [language?.id]);

    return (
        <>
            <Navigation backNavigationOnly={true} />
            <main>{id ? <AccountActivate id={id} type={type} className="pt-120 pb-240" /> : <ErrorMessage />}</main>
        </>
    );
};
