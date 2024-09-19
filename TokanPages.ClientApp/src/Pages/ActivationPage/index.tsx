import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import { Box, Typography } from "@material-ui/core";
import { ApplicationState } from "../../Store/Configuration";
import { ActivateAccount } from "../../Components/Account";
import { Navigation } from "../../Components/Layout";
import { Colours } from "../../Theme";

import { ContentActivateAccountAction, ContentFooterAction, ContentTemplatesAction } from "../../Store/Actions";

const useQuery = () => {
    return new URLSearchParams(useLocation().search);
};

const ErrorMessage = (): React.ReactElement => {
    return (
        <Box mt={10} mb={15}>
            <Typography>Uuuppss..., there is a missing ID...</Typography>
        </Box>
    );
};

export const ActivationPage = (): React.ReactElement => {
    const queryParam = useQuery();
    const dispatch = useDispatch();
    const id = queryParam.get("id");
    const type = queryParam.get("type") as string;

    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    React.useEffect(() => {
        dispatch(ContentActivateAccountAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentTemplatesAction.get());
    }, [language?.id]);

    return (
        <>
            <Navigation backNavigationOnly={true} />
            {id ? (
                <ActivateAccount
                    id={id}
                    type={type}
                    pt={15}
                    pb={30}
                    background={{ backgroundColor: Colours.colours.lightGray3 }}
                />
            ) : (
                <ErrorMessage />
            )}
        </>
    );
};
