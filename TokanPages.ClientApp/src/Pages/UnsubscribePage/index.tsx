import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import Container from "@material-ui/core/Container";
import { IApplicationState } from "../../Store/Configuration";
import { Navigation, Footer } from "../../Components/Layout";
import { Unsubscribe } from "../../Components/Unsubscribe";

import { 
    ContentNavigationAction, 
    ContentFooterAction, 
    ContentUnsubscribeAction
} from "../../Store/Actions";

const useQuery = () => 
{
    return new URLSearchParams(useLocation().search);
}

export const UnsubscribePage = (): JSX.Element =>
{
    const queryParam = useQuery();
    const dispatch = useDispatch();
    const id = queryParam.get("id") as string;
    
    const language = useSelector((state: IApplicationState) => state.applicationLanguage);
    const navigation = useSelector((state: IApplicationState) => state.contentNavigation);
    const footer = useSelector((state: IApplicationState) => state.contentFooter);
    const unsubscribe = useSelector((state: IApplicationState) => state.contentUnsubscribe);

    React.useEffect(() => 
    {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentUnsubscribeAction.get());
    }, 
    [ dispatch, language?.id ]);
    
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
