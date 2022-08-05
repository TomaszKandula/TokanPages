import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import { Box, Typography, Container } from "@material-ui/core";
import { IApplicationState } from "../../Redux/applicationState";
import { ActionCreators as ActivateAccountContent } from "../../Redux/Actions/Content/getActivateAccountContentAction";
import { ActionCreators as NavigationContent } from "../../Redux/Actions/Content/getNavigationContentAction";
import { ActionCreators as FooterContent } from "../../Redux/Actions/Content/getFooterContentAction";
import { ActivateAccount } from "../../Components/Account";
import { Navigation } from "../../Components/Layout";
import { Footer } from "../../Components/Layout";

const useQuery = () => 
{
    return new URLSearchParams(useLocation().search);
}

export const ActivationPage = (): JSX.Element => 
{
    const queryParam = useQuery();
    const dispatch = useDispatch();
    const id = queryParam.get("id");

    const language = useSelector((state: IApplicationState) => state.userLanguage);
    const activation = useSelector((state: IApplicationState) => state.getActivateAccountContent);
    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);

    React.useEffect(() => 
    {
        dispatch(ActivateAccountContent.getActivateAccountContent());
        dispatch(NavigationContent.getNavigationContent());
        dispatch(FooterContent.getFooterContent());
    }, 
    [ dispatch, language?.id ]);

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
