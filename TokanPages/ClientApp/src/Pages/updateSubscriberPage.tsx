import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import Footer from "../Components/Layout/footer";
import UpdateSubscriber from "../Components/UpdateSubscription/updateSubscriber";
import { IApplicationState } from "../Redux/applicationState";
import { combinedDefaults } from "../Redux/combinedDefaults";
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

    const fetchNavigationContent = React.useCallback(() => dispatch(NavigationContent.getNavigationContent()), [ dispatch ]);
    const fetchFooterContent = React.useCallback(() => dispatch(FooterContent.getFooterContent()), [ dispatch ]);
    const fetchUpdateSubscriberContent = React.useCallback(() => dispatch(UpdateSubscriberContent.getUpdateSubscriberContent()), [ dispatch ]);

    React.useEffect(() => 
    { 
        if (navigation?.content === combinedDefaults.getNavigationContent.content) 
            fetchNavigationContent(); 
    }, [ fetchNavigationContent, navigation?.content ]);
    
    React.useEffect(() => 
    { 
        if (footer?.content === combinedDefaults.getFooterContent.content) 
            fetchFooterContent(); 
    }, [ fetchFooterContent, footer?.content ]);
    
    React.useEffect(() => 
    { 
        if (updateSubscriber?.content === combinedDefaults.getUpdateSubscriberContent.content) 
            fetchUpdateSubscriberContent(); 
    }, [ fetchUpdateSubscriberContent, updateSubscriber?.content ]);
    
    return(
        <>
            <Navigation navigation={navigation} isLoading={navigation?.isLoading} />
            <Container>
                <UpdateSubscriber id={id} updateSubscriber={updateSubscriber} isLoading={updateSubscriber?.isLoading} />
            </Container>
            <Footer footer={footer} isLoading={footer?.isLoading} />
        </>
    );
}
