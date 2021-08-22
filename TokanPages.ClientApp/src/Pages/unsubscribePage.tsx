import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import Footer from "../Components/Layout/footer";
import Unsubscribe from "../Components/Unsubscribe/unsubscribe";
import { IApplicationState } from "../Redux/applicationState";
import { ActionCreators as NavigationContent } from "../Redux/Actions/Content/getNavigationContentAction";
import { ActionCreators as FooterContent } from "../Redux/Actions/Content/getFooterContentAction";
import { ActionCreators as UnsubscribeContent } from "../Redux/Actions/Content/getUnsubscribeContentAction";

const useQuery = () => 
{
    return new URLSearchParams(useLocation().search);
}

const UnsubscribePage = (): JSX.Element =>
{
    const queryParam = useQuery();
    const dispatch = useDispatch();
    const id = queryParam.get("id") as string;
    
    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);
    const unsubscribe = useSelector((state: IApplicationState) => state.getUnsubscribeContent);

    const getContent = React.useCallback(() =>
    {
        dispatch(NavigationContent.getNavigationContent());
        dispatch(FooterContent.getFooterContent());
        dispatch(UnsubscribeContent.getUnsubscribeContent());
    }, [ dispatch ]);

    React.useEffect(() => getContent(), [ getContent ]);
    
    return(
        <>
            <Navigation content={navigation?.content} isLoading={navigation?.isLoading} />
            <Container>
                <Unsubscribe id={id} content={unsubscribe?.content} isLoading={unsubscribe?.isLoading} />
            </Container>
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
}

export default UnsubscribePage;
