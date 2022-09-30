import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import Container from "@material-ui/core/Container";
import { IApplicationState } from "../../Store/Configuration";
import { Navigation, Footer } from "../../Components/Layout";
import { UpdateSubscriber } from "../../Components/UpdateSubscriber";

import { 
    GetNavigationContentAction, 
    GetFooterContentAction, 
    GetUpdateSubscriberContentAction 
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

    const language = useSelector((state: IApplicationState) => state.userLanguage);
    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);
    const updateSubscriber = useSelector((state: IApplicationState) => state.getUpdateSubscriberContent);

    React.useEffect(() => 
    {
        dispatch(GetNavigationContentAction.get());
        dispatch(GetFooterContentAction.get());
        dispatch(GetUpdateSubscriberContentAction.get());
    }, 
    [ dispatch, language?.id ]);
    
    return(
        <>
            <Navigation content={navigation?.content} isLoading={navigation?.isLoading} />
            <Container>
                <UpdateSubscriber id={id} content={updateSubscriber?.content} isLoading={updateSubscriber?.isLoading} />
            </Container>
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
}
