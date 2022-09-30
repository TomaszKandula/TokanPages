import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import Container from "@material-ui/core/Container";
import { IApplicationState } from "../../Store/Configuration";
import { Navigation, Footer } from "../../Components/Layout";
import { Unsubscribe } from "../../Components/Unsubscribe";

import { 
    GetNavigationContentAction, 
    GetFooterContentAction, 
    GetUnsubscribeContentAction
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
    
    const language = useSelector((state: IApplicationState) => state.userLanguage);
    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);
    const unsubscribe = useSelector((state: IApplicationState) => state.getUnsubscribeContent);

    React.useEffect(() => 
    {
        dispatch(GetNavigationContentAction.get());
        dispatch(GetFooterContentAction.get());
        dispatch(GetUnsubscribeContentAction.get());
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
