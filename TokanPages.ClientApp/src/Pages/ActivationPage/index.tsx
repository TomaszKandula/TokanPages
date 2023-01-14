import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import { Box, Typography, Container } from "@material-ui/core";
import { IApplicationState } from "../../Store/Configuration";
import { ActivateAccount } from "../../Components/Account";
import { Navigation, Footer } from "../../Components/Layout";

import { 
    ContentActivateAccountAction, 
    ContentNavigationAction, 
    ContentFooterAction 
} from "../../Store/Actions";

const useQuery = () => 
{
    return new URLSearchParams(useLocation().search);
}

const ErrorMessage = () => 
{
    // TODO: improve UI for message
    return (
        <Box mt={10} mb={15}>
            <Typography>
                Uuuppss..., there is a missing ID...
            </Typography>
        </Box>
    );
}

export const ActivationPage = (): JSX.Element => 
{
    const queryParam = useQuery();
    const dispatch = useDispatch();
    const id = queryParam.get("id");

    const language = useSelector((state: IApplicationState) => state.applicationLanguage);
    const activation = useSelector((state: IApplicationState) => state.contentActivateAccount);
    const navigation = useSelector((state: IApplicationState) => state.contentNavigation);
    const footer = useSelector((state: IApplicationState) => state.contentFooter);

    React.useEffect(() => 
    {
        dispatch(ContentActivateAccountAction.get());
        dispatch(ContentNavigationAction.get());
        dispatch(ContentFooterAction.get());
    }, 
    [ language?.id ]);

    return (
        <>
            <Navigation content={navigation?.content} isLoading={navigation?.isLoading} />
            <Container>
                {id 
                    ? <ActivateAccount id={id} content={activation?.content} isLoading={activation?.isLoading} /> 
                    : <ErrorMessage />
                }
            </Container>
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
}
