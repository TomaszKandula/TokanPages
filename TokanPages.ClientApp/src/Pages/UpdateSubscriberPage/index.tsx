import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import Container from "@material-ui/core/Container";
import { ApplicationState } from "../../Store/Configuration";
import { Navigation, Footer } from "../../Components/Layout";
import { UpdateSubscriber } from "../../Components/UpdateSubscriber";

import { 
    ContentNavigationAction, 
    ContentFooterAction, 
    ContentUpdateSubscriberAction 
} from "../../Store/Actions";

const useQuery = () => 
{
    return new URLSearchParams(useLocation().search);
}

export const UpdateSubscriberPage = (): JSX.Element =>
{
    const queryParam = useQuery();
    const dispatch = useDispatch();
    const id = queryParam.get("id") as string; 

    const language = useSelector((state: ApplicationState) => state.applicationLanguage);
    const navigation = useSelector((state: ApplicationState) => state.contentNavigation);
    const footer = useSelector((state: ApplicationState) => state.contentFooter);
    const subscriber = useSelector((state: ApplicationState) => state.contentUpdateSubscriber);

    React.useEffect(() => 
    {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentUpdateSubscriberAction.get());
    }, 
    [ language?.id ]);

    return(
        <>
            <Navigation content={navigation?.content} isLoading={navigation?.isLoading} />
            <Container>
                <UpdateSubscriber id={id} content={subscriber?.content} isLoading={subscriber?.isLoading} />
            </Container>
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
}
