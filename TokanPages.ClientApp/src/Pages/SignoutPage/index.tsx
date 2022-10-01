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
    
    const language = useSelector((state: IApplicationState) => state.applicationLanguage);
    const navigation = useSelector((state: IApplicationState) => state.contentNavigation);
    const footer = useSelector((state: IApplicationState) => state.contentFooter);
    const signout = useSelector((state: IApplicationState) => state.contentUserSignout);

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
                <UserSignout content={signout?.content} isLoading={signout?.isLoading} />
            </Container>
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
}
