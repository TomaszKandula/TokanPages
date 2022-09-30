import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import { IApplicationState } from "../../Store/Configuration";
import { Navigation, Footer } from "../../Components/Layout";
import { UserSignout } from "../../Components/Account";

import { 
    GetNavigationContentAction, 
    GetFooterContentAction, 
    GetUserSignoutContentAction 
} from "../../Store/Actions";

export const SignoutPage = (): JSX.Element => 
{
    const dispatch = useDispatch();
    
    const language = useSelector((state: IApplicationState) => state.userLanguage);
    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);
    const signoutView = useSelector((state: IApplicationState) => state.getUserSignoutContent);

    React.useEffect(() => 
    {
        dispatch(GetNavigationContentAction.get());
        dispatch(GetFooterContentAction.get());
        dispatch(GetUserSignoutContentAction.get());
    }, 
    [ dispatch, language?.id ]);

    return (
        <>
            <Navigation content={navigation?.content} isLoading={navigation?.isLoading} />
            <Container>
                <UserSignout content={signoutView?.content} isLoading={signoutView?.isLoading} />
            </Container>
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
}
