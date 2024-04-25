import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import { Box, Typography } from "@material-ui/core";
import { ApplicationState } from "../../Store/Configuration";
import { ActivateAccount } from "../../Components/Account";
import { Footer } from "../../Components/Layout";

import { ContentActivateAccountAction, ContentNavigationAction, ContentFooterAction } from "../../Store/Actions";

const useQuery = () => {
    return new URLSearchParams(useLocation().search);
};

const ErrorMessage = () => {
    // TODO: improve UI for message
    return (
        <Box mt={10} mb={15}>
            <Typography>Uuuppss..., there is a missing ID...</Typography>
        </Box>
    );
};

export const ActivationPage = (): JSX.Element => {
    const queryParam = useQuery();
    const dispatch = useDispatch();
    const id = queryParam.get("id");
    const type = queryParam.get("type") as string;

    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(ContentActivateAccountAction.get());
        dispatch(ContentNavigationAction.get());
        dispatch(ContentFooterAction.get());
    }, [language?.id]);

    return (
        <>
            {id ? (
                <ActivateAccount id={id} type={type} />
            ) : (
                <ErrorMessage />
            )}
            <Footer />
        </>
    );
};
