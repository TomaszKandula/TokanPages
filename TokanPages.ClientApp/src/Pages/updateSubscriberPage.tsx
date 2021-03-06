import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import Container from "@material-ui/core/Container";
import NavigationView from "../Components/Layout/navigationView";
import Footer from "../Components/Layout/footer";
import UpdateSubscriber from "../Components/UpdateSubscriber/updateSubscriber";
import { IApplicationState } from "../Redux/applicationState";
import { ActionCreators as NavigationContent } from "../Redux/Actions/getNavigationContentAction";
import { ActionCreators as FooterContent } from "../Redux/Actions/getFooterContentAction";
import { ActionCreators as UpdateSubscriberContent } from "../Redux/Actions/getUpdateSubscriberContentAction";

const useQuery = () => 
{
    return new URLSearchParams(useLocation().search);
}

export default function UpdateSubscriberPage()
{
    const queryParam = useQuery();
    const dispatch = useDispatch();
    const id = queryParam.get("id") as string; 

    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);
    const updateSubscriber = useSelector((state: IApplicationState) => state.getUpdateSubscriberContent);

    const getContent = React.useCallback(() =>
    {
        dispatch(NavigationContent.getNavigationContent());
        dispatch(FooterContent.getFooterContent());
        dispatch(UpdateSubscriberContent.getUpdateSubscriberContent());
    }, [ dispatch ]);

    React.useEffect(() => getContent(), [ getContent ]);
    
    return(
        <>
            <NavigationView content={navigation?.content} isLoading={navigation?.isLoading} />
            <Container>
                <UpdateSubscriber id={id} content={updateSubscriber?.content} isLoading={updateSubscriber?.isLoading} />
            </Container>
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
}
